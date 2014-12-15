using Employees.Helper;
using Employees.Model;
using Employees.Repository;

namespace Employees.ViewModel
{
    /// <summary>
    /// Базовий  ViewModel для довідників
    /// </summary>
    internal class DictionaryViewModel : BaseTabViewModel
    {
        public DictionaryViewModel() : base()
        {

        }

        public DictionaryViewModel(string header, IRepository<BaseDictionary> repository)
            : base(header, repository)
        {

        }

        private BaseDictionary _selectedItem;

        public BaseDictionary SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private DelegateCommand<object> _delete;

        public DelegateCommand<object> DeleteCommand
        {
            get { return _delete ?? (_delete = new DelegateCommand<object>(DeleteItem)); }
        }

        private void DeleteItem(object obj)
        {
            if (_selectedItem.CanDeleted)
                Repository.Delete(_selectedItem);
        }
    }
}
