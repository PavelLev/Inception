using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DryIoc;
using ImTools;

namespace Inception.Utility.Parallel
{
    public class Cluster<TEntity> : ICluster<TEntity>
    {
        private readonly Task<(TEntity entity, object result)>[] _entityTasks;
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(true);



        public Cluster(Func<TEntity> factoryFunc, ClusterOptions clusterOptions)
        {
            _entityTasks = Enumerable.Range(0, clusterOptions.DegreeOfParallelism)
                .Select(number => Task.FromResult((factoryFunc(), (object)null)))
                .ToArray();
        }



        public Task<TResult> DoWork<TResult>(Func<TEntity, TResult> workFunc)
        {
            return DoWork(entity => Task.Run(() => workFunc(entity)));
        }



        public async Task<TResult> DoWork<TResult>(Func<TEntity, Task<TResult>> workFunc)
        {
            _autoResetEvent.WaitOne();


            var task = await Task.WhenAny(_entityTasks);

            var index = _entityTasks.IndexOf(task);


            var newTask = Task.Run(async () =>
            {
                var entity = (await task).entity;

                object result = await workFunc(entity);

                return (entity, result);
            });

            _entityTasks[index] = newTask;


            _autoResetEvent.Set();

            return (TResult)(await newTask).result;
        }
    }
}
