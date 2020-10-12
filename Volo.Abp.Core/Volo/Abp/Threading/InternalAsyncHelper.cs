using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Threading
{
    public static class InternalAsyncHelper
    {
        public static async Task AwaitTaskWithFinally(Task actualReturnValue, Action<Exception> finalAction)
        {
            Exception exception = null;
            try
            {
                await actualReturnValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Func<Task> postAction,
            Action<Exception> finalAction)
        {
            Exception exception = null;
            try
            {
                await actualReturnValue;
                await postAction();
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        public static async Task AwaitTaskWithPreActionAndPostActionAndFinally(Func<Task> actualReturnValue,
            Func<Task> preAction = null,
            Func<Task> postAction = null, Action<Exception> finalAction = null)
        {
            Exception exception = null;

        }
    }
}
