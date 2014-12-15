using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Employees.Helper;
using Employees.Repository;

namespace Employees.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        #region ctor

        public MainViewModel()
        {
            _repositoryes = RepositoryesDb.Instance;
            var workerView = new WorkersProfilesViewModel("Анкети працівників", _repositoryes.WorkerRepository);
            _tabViewModel.Add(workerView);
            _tabViewModel.Add(new WorkersTreeViewModel("Дерево працівників", _repositoryes.WorkerRepository, workerView));

        }

        #endregion

        #region prop and field

        private readonly IRepositoryes _repositoryes;



        private readonly ObservableCollection<BaseTabViewModel> _tabViewModel =
            new ObservableCollection<BaseTabViewModel>();

        public ObservableCollection<BaseTabViewModel> TabViewModels
        {
            get { return _tabViewModel; }
        }

        private object _selectedTab;

        public object SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                OnPropertyChanged();
            }
        }

        private bool _isRequest = false;

        public bool IsRequest
        {
            get { return _isRequest; }
            set
            {
                _isRequest = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region command

        private DelegateCommand<object> _closeApp;

        public DelegateCommand<object> CloseAppCommand
        {
            get { return _closeApp ?? (_closeApp = new DelegateCommand<object>(CloseApp)); }
        }



        private DelegateCommand<string> _addNewTab;

        public DelegateCommand<string> AddNewTabCommand
        {
            get { return _addNewTab ?? (_addNewTab = new DelegateCommand<string>(AddNewTab)); }
        }

        private DelegateCommand<string> _focusedOnTab;

        public DelegateCommand<string> FocusedOnTabCommand
        {
            get { return _focusedOnTab ?? (_focusedOnTab = new DelegateCommand<string>(FocusedOnTab)); }
        }

        private void FocusedOnTab(string tabType)
        {
            switch (tabType)
            {
                case "Workers":
                    SelectedTab = _tabViewModel.FirstOrDefault(a => a.Header == "Анкети працівників");
                    break;
                case "WorkersTree":
                    SelectedTab = _tabViewModel.FirstOrDefault(a => a.Header == "Дерево працівників");
                    break;
                case "WorkersSalary":
                    MessageBox.Show("Данний розділ програми ще не реалізований", "Інформація", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    break;
                default:
                    return;
            }
        }


        private DelegateCommandAsync _synchronize;

        public DelegateCommandAsync SynchronizeCommand
        {
            get
            {
                return _synchronize ?? (_synchronize = new DelegateCommandAsync(async () =>
                {
                    try
                    {
                        IsRequest = true;
                        await _repositoryes.SynchronizeAsync(null);
                        IsRequest = false;
                    }
                    catch (Exception ex)
                    {
                        HelpUtilites.ShowFullException(ex);
                    }

                }));
            }
        }

        private DelegateCommandAsync _save;

        public DelegateCommandAsync SaveCommand
        {
            get
            {
                return _save ?? (_save = new DelegateCommandAsync(async () =>
                {
                    try
                    {
                        IsRequest = true;
                        await _repositoryes.SaveChengesAsync(null);
                        IsRequest = false;
                    }
                    catch (Exception ex)
                    {
                        HelpUtilites.ShowFullException(ex);
                    }

                }));
            }
        }

        #endregion

        #region methods

        private void AddNewTab(string tabType)
        {
            switch (tabType)
            {
                case "Department":
                    if (_tabViewModel.All(a => a.Header != "Підрозділи"))
                        _tabViewModel.Add(new DictionaryViewModel("Підрозділи", _repositoryes.DepartmentRepository));
                    SelectedTab = _tabViewModel.FirstOrDefault(a => a.Header == "Підрозділи");
                    break;
                case "EducationLevel":
                    if (_tabViewModel.All(a => a.Header != "Ступені освіти"))
                        _tabViewModel.Add(new DictionaryViewModel("Ступені освіти",
                            _repositoryes.EducationLevelRepository));
                    SelectedTab = _tabViewModel.FirstOrDefault(a => a.Header == "Ступені освіти");
                    break;
                case "Position":
                    if (_tabViewModel.All(a => a.Header != "Посади"))
                        _tabViewModel.Add(new DictionaryViewModel("Посади", _repositoryes.PositionRepository));
                    SelectedTab = _tabViewModel.FirstOrDefault(a => a.Header == "Посади");
                    break;
                case "Specialty":
                    if (_tabViewModel.All(a => a.Header != "Спеціальності"))
                        _tabViewModel.Add(new DictionaryViewModel("Спеціальності", _repositoryes.SpecialtyRepository));
                    SelectedTab = _tabViewModel.FirstOrDefault(a => a.Header == "Спеціальності");
                    break;
                default:
                    return;

            }
        }

        private static void CloseApp(object obj)
        {
            Application.Current.MainWindow.Close();
        }

        #endregion

    }
}
