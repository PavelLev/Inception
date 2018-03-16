using System;
using System.Threading.Tasks;

namespace Inception.Utility.Parallel
{
    public interface ICluster<TEntity>
    {
        Task<TResult> DoWork<TResult>(Func<TEntity, TResult> workFunc);

        Task<TResult> DoWork<TResult>(Func<TEntity, Task<TResult>> workFunc);
    }
}
