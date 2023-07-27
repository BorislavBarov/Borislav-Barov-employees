using Pair_of_employees_who_have_worked_together.Services;
using Pair_of_employees_who_have_worked_together.Views;
using System.Windows;

namespace Pair_of_employees_who_have_worked_together
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            PairProjectsListView pairProject = new PairProjectsListView();
            DialogService service = new DialogService();
            service.ShowDialog(pairProject);
        }
    }
}
