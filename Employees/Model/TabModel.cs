using System.Windows;

namespace Employees.Model
{
    /// <summary>
    /// Вкладка для вікна
    /// </summary>
    internal class TabModel
    {
        /// <summary>
        /// Назва вкладки
        /// </summary>
        public string TabCaption { get; set; }
        /// <summary>
        /// Контент
        /// </summary>
        public FrameworkElement TabContent { get; set; }
    }
}
