using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Inception.Utility.Extensions
{
    public static class TaskExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Causes compiler to optimize the call away
        public static void NoWarning(this Task task) { /* No code goes in here */ }
    }
}
