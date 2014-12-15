using System.Collections.Generic;
using System.Threading.Tasks;
using Employees.Model;

namespace Employees.Repository
{

    /// <summary>
    /// Глобальний репозиторій, що включає в себе усі конкретні репозиторії
    /// </summary>
    public interface IRepositoryes
    {
        IRepository<Worker> WorkerRepository { get; }
        IRepository<Department> DepartmentRepository { get; }
        IRepository<EducationLevel> EducationLevelRepository { get; }
        IRepository<Position> PositionRepository { get; }
        IRepository<Specialty> SpecialtyRepository { get; }
        /// <summary>
        /// Завантажити дані в програму
        /// </summary>
        /// <param name="obj"></param>
        void Synchronize(object obj);
        /// <summary>
        /// Завантажити дані в програму(Асинхронна версія)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task SynchronizeAsync(object obj);
        /// <summary>
        /// Зберегти всі зміни в джерело даних
        /// </summary>
        /// <param name="obj"></param>
        void SaveChenges(object obj);
       /// <summary>
       /// Зберегти всі зміни в джерело даних(Асинхронна версія)
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        Task SaveChengesAsync(object obj);
    }

    public interface IRepository<out T>
    {
        /// <summary>
        /// Локальний ресурс записів вказаного типу
        /// </summary>
        IEnumerable<T> LocalResourse { get;  }
        void Add(object entry);
        void Delete(object entity);
        void Synchronize();
        Task SynchronizeAsync();
        void SaveChenges();
        Task SaveChangesAsync();
    }
}
