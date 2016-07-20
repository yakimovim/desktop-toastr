using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Threading;

namespace EdlinSoftware.Toastr.Models
{
    public class ActionsExecutor : IDisposable
    {
        private readonly Dispatcher _dispatcher;
        private readonly ConcurrentQueue<Action> _actionsQueue = new ConcurrentQueue<Action>();
        private volatile bool _isDisposed;

        public ActionsExecutor(Dispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException(nameof(dispatcher));
            _dispatcher = dispatcher;

            ThreadPool.QueueUserWorkItem(DoWork);
        }

        public void AddAction(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            _actionsQueue.Enqueue(action);
        }

        private void DoWork(object state)
        {
            while (true)
            {
                Action action;
                if (_actionsQueue.TryDequeue(out action))
                {
                    _dispatcher.Invoke(action);
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(50));
                }

                if(_isDisposed) break;
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
        }
    }
}