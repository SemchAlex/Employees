using System.Threading.Tasks;
using Employees.Model;

namespace Employees.Repository
{
    public class RepositoryesDb : IRepositoryes
    {
        #region singl

        private static RepositoryesDb _instance;

        private RepositoryesDb()
        {
            var ctx = new DataContext("EmployeesDB");
            _workerReposytory = new WorkerRepository(ctx);
            _departmentRepository = new ReposytoryDb<Department>(ctx);
            _educationLevelRepository = new ReposytoryDb<EducationLevel>(ctx);
            _positionRepository = new ReposytoryDb<Position>(ctx);
            _specialtyRepository = new ReposytoryDb<Specialty>(ctx);
            ctx.Database.Initialize(true);
        }

        public static IRepositoryes Instance
        {
            get { return _instance ?? (_instance = new RepositoryesDb()); }
        }


        #endregion

        #region prop

        private readonly ReposytoryDb<Worker> _workerReposytory;

        public IRepository<Worker> WorkerRepository
        {
            get { return _workerReposytory; }
        }

        private readonly ReposytoryDb<Department> _departmentRepository;

        public IRepository<Department> DepartmentRepository
        {
            get { return _departmentRepository; }
        }

        private readonly ReposytoryDb<EducationLevel> _educationLevelRepository;

        public IRepository<EducationLevel> EducationLevelRepository
        {
            get { return _educationLevelRepository; }
        }

        private readonly ReposytoryDb<Position> _positionRepository;

        public IRepository<Position> PositionRepository
        {
            get { return _positionRepository; }
        }

        private readonly ReposytoryDb<Specialty> _specialtyRepository;

        public IRepository<Specialty> SpecialtyRepository
        {
            get { return _specialtyRepository; }
        }

        #endregion

        #region public method

        public void Synchronize(object obj)
        {
            _departmentRepository.Synchronize();
            _educationLevelRepository.Synchronize();
            _positionRepository.Synchronize();
            _specialtyRepository.Synchronize();
            _workerReposytory.Synchronize();
        }

        public async Task SynchronizeAsync(object obj)
        {
           await _departmentRepository.SynchronizeAsync();
           await _educationLevelRepository.SynchronizeAsync();
           await _positionRepository.SynchronizeAsync();
           await _specialtyRepository.SynchronizeAsync();
           await _workerReposytory.SynchronizeAsync();
        }

        public void SaveChenges(object obj)
        {
            _departmentRepository.SaveChenges();
            _educationLevelRepository.SaveChenges();
            _positionRepository.SaveChenges();
            _specialtyRepository.SaveChenges();
            _workerReposytory.SaveChenges();
        }

        public async Task SaveChengesAsync(object obj)
        {
            await _departmentRepository.SaveChangesAsync();
            await _educationLevelRepository.SaveChangesAsync();
            await _positionRepository.SaveChangesAsync();
            await _specialtyRepository.SaveChangesAsync();
            await _workerReposytory.SaveChangesAsync();
        }

        #endregion

    }
}
