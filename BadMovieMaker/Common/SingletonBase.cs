using System.ComponentModel;

namespace BadMovieMaker.Common
{
    public class SingletonBase<T> : INotifyPropertyChanged where T : new()
    {
        protected static T instance = default(T);
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}