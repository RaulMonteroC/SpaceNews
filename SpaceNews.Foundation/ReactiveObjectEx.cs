using System;
using System.Reactive.Disposables;
using ReactiveUI;

namespace SpaceNews.Foundation
{
    public abstract class ReactiveObjectEx : ReactiveObject, IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewModelSubscriptions?.Dispose();
            }
        }

        protected CompositeDisposable ViewModelSubscriptions { get; } = new CompositeDisposable();
    }
}