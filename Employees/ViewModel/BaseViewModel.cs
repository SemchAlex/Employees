using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Employees.ViewModel
{
    /// <summary>
    /// Абстрактний базовий для ViewModel з реалізацією INotifyPropertyChanged
    /// </summary>
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
