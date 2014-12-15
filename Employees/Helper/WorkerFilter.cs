using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Employees.Helper
{
    /// <summary>
    /// Класс з набором значень фильтрив для Worker
    /// </summary>
    public class WorkerFilter : INotifyPropertyChanged
    {
        private string _fullName;
        private DateTime _dateBirth;
        private string _position;
        private string _department;
        private string _salary;
        private string _educationLevel;
        private string _speciality;
        private string _adress;

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateBirth
        {
            get { return _dateBirth; }
            set
            {
                _dateBirth = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged();
            }
        }

        public string Salary
        {
            get { return _salary; }
            set
            {
                _salary = value;
                OnPropertyChanged();
            }
        }

        public string EducationLevel
        {
            get { return _educationLevel; }
            set
            {
                _educationLevel = value;
                OnPropertyChanged();
            }
        }

        public string Speciality
        {
            get { return _speciality; }
            set
            {
                _speciality = value;
                OnPropertyChanged();
            }
        }

        public string Adress
        {
            get { return _adress; }
            set
            {
                _adress = value;
                OnPropertyChanged();
            }
        }

        public WorkerFilter()
        {
            this.FullName = string.Empty;
            this.Position = string.Empty;
            this.Department = string.Empty;
            this.Salary = string.Empty;
            this.EducationLevel = string.Empty;
            this.Speciality = string.Empty;
            this.Adress = string.Empty;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
