using System;
using System.Text;
using System.Windows;

namespace Employees.Helper
{
    public static class HelpUtilites
    {
        /// <summary>
        /// Повертає повне повідомлення з усіма вкладеними помилками
        /// </summary>
        /// <param name="ex">Exception instance</param>
        public static void ShowFullException(Exception ex)
        {
            var tempException = ex;
            var errorsInfo = new StringBuilder();
            errorsInfo.AppendLine(tempException.Message);
            while (tempException.InnerException != null)
            {
                tempException = tempException.InnerException;
                errorsInfo.AppendLine(tempException.Message);
            }
            errorsInfo.AppendLine();
            errorsInfo.AppendLine("ЗАВЕРШИТИ ВИКОНАННЯ ПРОГРАМИ?");
            var result = MessageBox.Show(errorsInfo.ToString(),
                "Сталася програмна помилка...", MessageBoxButton.YesNo,
                MessageBoxImage.Error);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}
