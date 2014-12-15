using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Threading.Tasks;
using Employees.Model;

namespace Employees.Repository
{
    internal class WorkerRepository : ReposytoryDb<Worker>
    {
        public override async Task SynchronizeAsync()
        {
            ForDelete.Clear();
            _localResourse =
                new ObservableCollection<Worker>(await
                    Context.Set<Worker>()
                        .Include(a => a.WorkerDepartment)
                        .Include(a => a.WorkerPosition)
                        .Include(a => a.WorkerSpecialty)
                        .Include(a => a.WorkerEducationLevel)
                        .ToListAsync()
                    );
            OnPropertyChanged("LocalResourse");
        }

        public override void Delete(object entity)
        {
            var worker = (Worker)entity;
            worker.DictionarySubscribe(true);
            _localResourse.Remove(worker);
            if (!worker.IsNew())
                ForDelete.Add(worker);
        }

        public WorkerRepository()
        {
            
        }
        public WorkerRepository(DataContext dataContext):base(dataContext)
        {
            
        }
    }
}
