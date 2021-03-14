using ReactiveUI;
using SpaceNews.Foundation;

namespace SpaceNews
{
    public abstract class BaseViewModel : ReactiveObjectEx
    {
        public bool IsBusy
        {
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }

        private bool _isBusy;
    }
}