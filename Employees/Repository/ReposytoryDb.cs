using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Employees.Annotations;
using Employees.Model;

namespace Employees.Repository
{
    /// <summary>
    /// Конкретна реалізація репозиторія для БД MS SQL (Entity)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReposytoryDb<T> : IRepository<T>, IDisposable, INotifyPropertyChanged where T : BaseModel, new()
    {
        #region prop and fields

        protected readonly DataContext Context;
        protected ObservableCollection<T> _localResourse = new ObservableCollection<T>();
        protected readonly List<T> ForDelete = new List<T>();

        public IEnumerable<T> LocalResourse
        {
            get { return _localResourse; }
        }

        #endregion

        #region ctor

        public ReposytoryDb()
        {
            Context = new DataContext("EmployeesDB");
        }



        public ReposytoryDb(DataContext context)
        {
            Context = context;
        }

        #endregion
        
        #region public methods

        public void Add(object entry)
        {
            _localResourse.Add((T) entry);
        }

        public virtual void Delete(object entity)
        {
            _localResourse.Remove((T) entity);
            if (!((T) entity).IsNew())
                ForDelete.Add((T) entity);
        }

        public virtual void Synchronize()
        {
            ForDelete.Clear();
            _localResourse = new ObservableCollection<T>(Context.Set<T>());
            OnPropertyChanged("LocalResourse");
        }

        public virtual async Task SynchronizeAsync()
        {
            ForDelete.Clear();
            _localResourse = new ObservableCollection<T>(await Context.Set<T>().ToListAsync());
            OnPropertyChanged("LocalResourse");
        }


        public virtual void SaveChenges()
        {
            Context.Set<T>().RemoveRange(ForDelete);
            Context.Set<T>().AddRange(_localResourse.Where(a => a.IsNew()));
            //ToDo Update records
            Context.SaveChanges();
        }

        public virtual async Task SaveChangesAsync()
        {
            Context.Set<T>().RemoveRange(ForDelete);
            Context.Set<T>().AddRange(_localResourse.Where(a => a.IsNew()));
            Context.Set<T>().AddOrUpdate(_localResourse.Where(a => a.IsChanged()).ToArray());
            await Context.SaveChangesAsync();
        }


        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
        
        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    #endregion

}
