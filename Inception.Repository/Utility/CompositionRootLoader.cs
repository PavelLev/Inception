using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using DryIoc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Logging;

namespace Inception.Repository.Utility
{
    public class CompositionRootLoader : ICompositionRootLoader
    {
        private readonly IContainer _container;
        private readonly ILogger _logger;
        private readonly MetadataReference[] _commonReferences;
        private readonly string _fileFormat = "{0}.CompositionRoot.cs";



        public CompositionRootLoader(IContainer container, ILoggerFactory loggerFactory)
        {
            _container = container;
            _logger = loggerFactory.CreateLogger(GetType());

            _commonReferences = GetCommonReferences();
        }



        private MetadataReference[] GetCommonReferences()
        {
            var objectAssemblyLocation = typeof(object).Assembly.Location;

            var commonAssemblyLocation = Directory.GetParent
                (
                objectAssemblyLocation
                ).FullName;


            var netStandardAssembly = Assembly.LoadFrom(commonAssemblyLocation + Path.DirectorySeparatorChar + "netstandard.dll");

            var portableExecutableReferences = new[]
            {
                MetadataReference.CreateFromFile(objectAssemblyLocation),
                MetadataReference.CreateFromFile(netStandardAssembly.Location)
            }
                .Concat
                (
                GetMetadataForReferences(netStandardAssembly)
                )
                .ToArray();

            return portableExecutableReferences;
        }



        public void Load<T>()
        {
            var (filePath, compilation) = SetupCompilation<T>();


            var memoryStream = new MemoryStream();

            var emitResult = compilation.Emit(memoryStream);

            AddCoreReferences(ref compilation, ref emitResult, memoryStream);

            EnsureEmitResultIsSuccessful(emitResult, filePath);


            memoryStream.Seek(0, SeekOrigin.Begin);

            var createdAssembly = AssemblyLoadContext.Default.LoadFromStream(memoryStream);


            var compositionRootType = createdAssembly.ExportedTypes.First();

            _container.Register(compositionRootType, Reuse.Singleton);

            _container.Resolve(compositionRootType);
        }



        private void AddCoreReferences(ref CSharpCompilation compilation, ref EmitResult emitResult, Stream peStream)
        {
            if (emitResult.Success)
            {
                return;
            }

            var errors = emitResult.Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
                .ToArray();

            if (errors.Any(diagnostic => diagnostic.Id != "CS0012"))
            {
                return;
            }


            compilation = compilation.AddReferences
                (
                errors.Select
                    (
                    error => MetadataReference.CreateFromFile
                        (
                        Assembly.Load
                            (
                            GetAssemblyStringFromError(error)
                            )
                            .Location
                        )
                    )
                );


            emitResult = compilation.Emit(peStream);
        }



        private string GetAssemblyStringFromError(Diagnostic error)
        {
            var assemblyString = error.ToString()
                .Split('\'')[3];

            return assemblyString;
        }



        private (string filePath, CSharpCompilation compilation) SetupCompilation<T>()
        {
            var assembly = typeof(T).Assembly;

            var filePath = string.Format
                (
                _fileFormat,
                Path.ChangeExtension(assembly.Location, null)
                );


            var fileName = string.Format
                (
                _fileFormat,
                assembly.GetName()
                    .Name
                );


            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"File {filePath} doesn't exist");
            }

            var compilation = CSharpCompilation.Create(fileName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var syntaxTree = CSharpSyntaxTree.ParseText
                (
                File.ReadAllText(filePath)
                );

            compilation = compilation.AddReferences
                    (
                    _commonReferences
                    )
                .AddReferences
                    (
                    MetadataReference.CreateFromFile
                        (
                        assembly.Location
                        )
                    )
                .AddReferences
                    (
                    GetMetadataForReferences(assembly)
                    );

            compilation = compilation.AddSyntaxTrees
                (
                syntaxTree
                );

            return (filePath, compilation);
        }



        private IEnumerable<MetadataReference> GetMetadataForReferences(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies()
                .Select
                    (
                    referencedAssemblyName => MetadataReference.CreateFromFile
                        (
                        Assembly.Load(referencedAssemblyName)
                            .Location
                        )
                    );
        }



        private void EnsureEmitResultIsSuccessful(EmitResult emitResult, string filePath)
        {
            if (!emitResult.Success)
            {
                var stringBuilder = new StringBuilder();

                var errorMessage = $"Failed to compile {filePath}";

                stringBuilder.AppendLine(errorMessage);

                foreach (var diagnostic in emitResult.Diagnostics)
                {
                    stringBuilder.AppendLine(diagnostic.ToString());
                }


                _logger.LogError(stringBuilder.ToString());

                throw new ArgumentException(errorMessage);
            }
        }
    }
}