
using System;
using System.Windows.Data;
using Employees.Helper;
using Employees.Model;
using Employees.Repository;

namespace Employees.ViewModel
{
    internal class WorkersProfilesViewModel : BaseTabViewModel
    {
        #region Property


        private Worker _selectedWorker;
        private Worker _editedWorker;

        /// <summary>
        /// Вибраний працівник
        /// </summary>
        public Worker SelectedWorker
        {
            get { return _selectedWorker; }
            set
            {
                _selectedWorker = value;
                if (value != null)
                {
                    WorkerForEdit = (Worker) value.Clone();
                    WorkerForEdit.DictionarySubscribe(false);
                }
                OnPropertyChanged();
                OnPropertyChanged("IsRemoveButtonEnable");
            }
        }


        private WorkerFilter _workerFilter = new WorkerFilter();

        /// <summary>
        /// Значення фільтрів 
        /// </summary>
        public WorkerFilter WorkerFilter
        {
            get { return _workerFilter; }
            set
            {
                _workerFilter = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Доступність полів по вводу текста для редагування
        /// </summary>
        public bool IsEnabled
        {
            get { return _editedWorker != null; }
        }

        public bool IsRemoveButtonEnable
        {
            get { return _selectedWorker != null; }
        }

        /// <summary>
        /// Працівник, який редагується
        /// </summary>
        public Worker WorkerForEdit
        {
            get { return _editedWorker; }
            set
            {
                _editedWorker = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEnabled");
            }
        }

        /// <summary>
        /// Доступ до репозиторію для випадаючих списків
        /// </summary>
        public IRepositoryes Repositoryes
        {
            get { return RepositoryesDb.Instance; }
        }

        #endregion

        #region Ctor

        public WorkersProfilesViewModel()
            : base()
        {

        }

        public WorkersProfilesViewModel(string header, IRepository<BaseModel> repository)
            : base(header, repository)
        {
        }

        #endregion

        #region Command

        private DelegateCommand<object> _save;

        public DelegateCommand<object> SaveCommand
        {
            get { return _save ?? (_save = new DelegateCommand<object>(SaveItem)); }
        }

        private DelegateCommand<FilterEventArgs> _filter;

        public DelegateCommand<FilterEventArgs> FilterCommand
        {
            get { return _filter ?? (_filter = new DelegateCommand<FilterEventArgs>(FilterItem)); }
        }

        /// <summary>
        /// Фильтрація записів працівників
        /// </summary>
        /// <param name="e"></param>
        private void FilterItem(FilterEventArgs e)
        {
            var worker = e.Item as Worker;
            if (worker != null)
            {
                if (worker.FullName.Contains(this.WorkerFilter.FullName) &&
                    (worker.BirthDate == this.WorkerFilter.DateBirth || this.WorkerFilter.DateBirth == default(DateTime)) &&
                    worker.WorkerDepartment.Name.Contains(this.WorkerFilter.Department))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private DelegateCommand<object> _cancel;

        public DelegateCommand<object> CancelCommand
        {
            get { return _cancel ?? (_cancel = new DelegateCommand<object>(Cancel)); }
        }



        private DelegateCommand<object> _newItem;

        public DelegateCommand<object> NewItemCommand
        {
            get { return _newItem ?? (_newItem = new DelegateCommand<object>(NewItem)); }
        }

        private DelegateCommand<object> _removeItem;

        public DelegateCommand<object> RemoveItemCommand
        {
            get { return _removeItem ?? (_removeItem = new DelegateCommand<object>(RemveItem)); }
        }

        #endregion

        #region Method

        private void RemveItem(object obj)
        {
            Repository.Delete(SelectedWorker);
            SelectedWorker = null;
            WorkerForEdit.DictionarySubscribe(true);
            WorkerForEdit = null;
        }

        private void SaveItem(object obj)
        {
            try
            {
                if (_editedWorker == null || !string.IsNullOrEmpty(_editedWorker.Error)) return;
                if (_editedWorker.IsNew() && SelectedWorker == null)
                    Repository.Add(_editedWorker);
                else
                    SelectedWorker.Update(_editedWorker);
                WorkerForEdit.DictionarySubscribe(true);
                WorkerForEdit = null;
            }
            catch (Exception ex)
            {
                HelpUtilites.ShowFullException(ex);
            }
        }

        private void Cancel(object obj)
        {
            SelectedWorker = null;
            WorkerForEdit = null;
        }


        private void NewItem(object obj)
        {
            SelectedWorker = null;
            WorkerForEdit = new Worker();
        }

        #endregion

    }
}
