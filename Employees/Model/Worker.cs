using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Employees.Model
{

    public class Worker : BaseModel, IDataErrorInfo, ICloneable
    {
        private string _fullName;

        /// <summary>
        /// ПІБ; (обов’язково)
        /// </summary>
        [Required(ErrorMessage = "Необхідно вказати повне ім'я співробітника")]
        [MinLength(3)]
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName == value) return;
                _fullName = value;
                OnPropertyChanged();
            }
        }

        private DateTime _birthDate = new DateTime(1985, 1, 1);

        /// <summary>
        /// Дата народження; (обов’язково)
        /// </summary>
        [Required(ErrorMessage = "Необхідно вказати дату народження")]
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate == value) return;
                _birthDate = value;
                OnPropertyChanged();

            }
        }

        private Position _position;

        /// <summary>
        /// Посада (обов’язково)
        /// </summary>
        [Required(ErrorMessage = "Необхідно вказати посаду")]
        public Position WorkerPosition
        {
            get { return _position; }
            set
            {
                if (_position == value) return;
                if (value != null)
                {
                    if (_position != null)
                        _position.CanDeleted = true;
                    value.CanDeleted = false;
                }
                _position = value;
                OnPropertyChanged();
            }
        }

        private Department _department;

        /// <summary>
        /// Підрозділ організації; (необов’язково)
        /// </summary>
        public Department WorkerDepartment
        {
            get { return _department; }
            set
            {
                if (_department == value) return;
                if (value != null)
                {
                    if (_department != null)
                        _department.CanDeleted = true;
                    value.CanDeleted = false;
                }

                _department = value;
                OnPropertyChanged();
            }
        }

        private decimal _salary;

        /// <summary>
        /// Заробітна плата; (обов’язково)
        /// </summary>
        [Required(ErrorMessage = "Необхідно вказати заробітну плату")]
        public decimal Salary
        {
            get { return _salary; }
            set
            {
                if (_salary == value) return;
                _salary = value;
                OnPropertyChanged();
            }
        }


        private EducationLevel _educationLevel;

        /// <summary>
        /// Ступінь освіти (необов’язково)
        /// </summary>
        public EducationLevel WorkerEducationLevel
        {
            get { return _educationLevel; }
            set
            {
                if (_educationLevel == value) return;
                if (value != null)
                {
                    if (_educationLevel != null)
                        _educationLevel.CanDeleted = true;
                    value.CanDeleted = false;
                }
                _educationLevel = value;
                OnPropertyChanged();
            }
        }

        private Specialty _specialty;

        /// <summary>
        /// Спеціальність; (необов’язково)
        /// </summary>
        public Specialty WorkerSpecialty
        {
            get { return _specialty; }
            set
            {
                if (_specialty == value) return;
                if (value != null)
                {
                    if (_specialty != null)
                        _specialty.CanDeleted = true;
                    value.CanDeleted = false;
                }
                _specialty = value;
                OnPropertyChanged();
            }
        }


        private string _adress;

        /// <summary>
        /// Адреса; (необов’язково)
        /// </summary>
        public string Adress
        {
            get { return _adress; }
            set
            {
                if (_adress == value) return;
                _adress = value;
                OnPropertyChanged();
            }
        }


        private string _error;

        public string Error
        {
            get { return _error; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "FullName":
                        _error = ValidateProperty(this.FullName, columnName);
                        return _error;
                    case "BirthDate":
                        _error = ValidateProperty(this.FullName, columnName);
                        return _error;
                    case "WorkerPosition":
                        _error = ValidateProperty(this.FullName, columnName);
                        return _error;
                    case "Salary":
                        _error = ValidateProperty(this.FullName, columnName);
                        return _error;
                }
                return null;
            }
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }


        /// <summary>
        /// Обновити екземпляр значеннями з іншого
        /// </summary>
        /// <param name="item"></param>
        public void Update(Worker item)
        {
            this.FullName = item.FullName;
            this.Adress = item.Adress;
            this.BirthDate = item.BirthDate;
            this.Salary = item.Salary;
            this.WorkerDepartment = item.WorkerDepartment;
            this.WorkerEducationLevel = item.WorkerEducationLevel;
            this.WorkerPosition = item.WorkerPosition;
            this.WorkerSpecialty = item.WorkerSpecialty;
        }

        public void DictionarySubscribe(bool canDeleted)
        {
            setForDeleted(this.WorkerDepartment, canDeleted);
            setForDeleted(this.WorkerEducationLevel,canDeleted);
            setForDeleted(this.WorkerPosition,canDeleted);
            setForDeleted(this.WorkerSpecialty,canDeleted);
        }

        
        private void setForDeleted(BaseDictionary dict, bool canDel)
        {
            if (dict != null)
                dict.CanDeleted = canDel;
        }
    }
}
