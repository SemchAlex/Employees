
using Employees.Model;
using Employees.Repository;

namespace Employees.ViewModel
{
    internal abstract class BaseTabViewModel : BaseViewModel
    {

        #region ctor

        protected BaseTabViewModel()
        {

        }

        protected BaseTabViewModel(string header, IRepository<BaseModel> repository)
        {
            _repository = repository;
            _header = header;
        }

        #endregion

        #region prop and fields

        private string _header;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        private readonly IRepository<BaseModel> _repository;

        public IRepository<BaseModel> Repository
        {
            get { return _repository; }
        }

        
        #endregion
    }
}
