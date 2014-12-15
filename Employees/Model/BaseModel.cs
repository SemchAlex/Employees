using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Employees.Annotations;

namespace Employees.Model
{
    public abstract class BaseModel : INotifyPropertyChanged
    {

        #region prop and field

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value;  }
        }

       

        private bool _isChanged;

        #endregion



        
        public bool IsChanged()
        {
            return _isChanged;
        }

        public bool IsNew()
        {
            return _id == -1;
        }

        #region ctor

        protected BaseModel()
        {
            _id = -1;
            _isChanged = false;
        }


        #endregion

        #region InotifyProp

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _isChanged = true;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        protected string ValidateProperty(object value, string propertyName)
        {
            var info = this.GetType().GetProperty(propertyName);
            IEnumerable<string> errorInfos =
                (from va in info.GetCustomAttributes(true).OfType<ValidationAttribute>()
                 where !va.IsValid(value)
                 select va.FormatErrorMessage(string.Empty)).ToList();

            return errorInfos.Any() ? errorInfos.FirstOrDefault<string>() : null;
        }

    }
}
