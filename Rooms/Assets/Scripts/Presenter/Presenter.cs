using View;

namespace Presenter
{
    public abstract class Presenter<T> where T : IWindow
    {

        public virtual T Windows
        {
            get { return windows; }
        }

        protected T windows;
    }
}