using Employees.Model;
using Employees.Repository;

namespace Employees.ViewModel
{
    internal class WorkersTreeViewModel : BaseTabViewModel
    {
        #region Property

        private readonly WorkersProfilesViewModel _refVvew;


        public Worker SelectedWorker
        {
            get { return _refVvew.SelectedWorker; }
            set
            {
                _refVvew.SelectedWorker = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ctor

        public WorkersTreeViewModel() : base()
        {

        }

        public WorkersTreeViewModel(string header, IRepository<BaseModel> repository, WorkersProfilesViewModel refView)
            : base(header, repository)
        {
            _refVvew = refView;
        }

        #endregion
    }
}

