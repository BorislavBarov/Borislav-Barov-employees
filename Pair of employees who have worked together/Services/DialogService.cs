using Pair_of_employees_who_have_worked_together.Constants;
using System.Windows;
using System.Windows.Controls;

namespace Pair_of_employees_who_have_worked_together.Services
{
    public class DialogService
    {
        #region Methods

        public void ShowDialog(UserControl userControl)
        {
            Window dialogWindow = new Window
            {
                Content = userControl,
                Title = ApplicationConstants.DialogTitle,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };

            dialogWindow.ShowDialog();
        }

        #endregion
    }
}
