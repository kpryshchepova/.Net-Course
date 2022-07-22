using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class RelayCommandAsync : BasicCommandAsync
    {
        private readonly Func<Task> _callback;

        public RelayCommandAsync(Func<Task> callback, Action<Exception> onException = null) : base(onException)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }
}
