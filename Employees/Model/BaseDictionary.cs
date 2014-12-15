
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace Employees.Model
{
    /// <summary>
    /// Базова модель для довідників
    /// </summary>
    public class BaseDictionary : BaseModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _referensCount = 0;

        [NotMapped]
        public bool CanDeleted
        {
            get { return _referensCount <= 0; }
            set
            {
                if (value)
                    _referensCount--;
                else
                    _referensCount++;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        public Brush DictBackground
        {
            get { return _referensCount <= 0 ? Brushes.LightGreen : Brushes.OrangeRed; }
        }

    }
}
