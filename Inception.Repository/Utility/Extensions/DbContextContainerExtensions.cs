using System;
using System.Collections.Generic;
using DryIoc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Inception.Repository.Utility.Extensions
{
    internal static class DbContextContainerExtensions
    {
        public static void RegisterDbContext<TDbContext>(this IContainer container, Action<IResolver, DbContextOptionsBuilder<TDbContext>> optionsAction)
            where TDbContext : DbContext
        {
            container.RegisterDelegate(resolverContext => DbContextOptionsFactory(resolverContext, optionsAction), setup: Setup.With(useParentReuse: true));
            
            container.Register<TDbContext>(setup: Setup.With(useParentReuse: true));
        }



        private static DbContextOptions<TDbContext> DbContextOptionsFactory<TDbContext>(IResolver resolver, Action<IResolver, DbContextOptionsBuilder<TDbContext>> optionsAction)
            where TDbContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TDbContext>(
                new DbContextOptions<TDbContext>(new Dictionary<Type, IDbContextOptionsExtension>()));
            
            optionsAction?.Invoke(resolver, builder);

            return builder.Options;
        }
    }
}
