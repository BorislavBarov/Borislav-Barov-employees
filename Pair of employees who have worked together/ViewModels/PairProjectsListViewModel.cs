using Microsoft.Win32;
using NLog;
using Pair_of_employees_who_have_worked_together.Constants;
using Pair_of_employees_who_have_worked_together.Models;
using Pair_of_employees_who_have_worked_together.Services;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pair_of_employees_who_have_worked_together.ViewModels
{
    public class PairProjectsListViewModel
    {
        #region Declarations

        private ObservableCollection<PairProjectsViewModel> projectsByPair;


        private DelegateCommand? selectFileCommand;

        #endregion

        #region Properties

        public ObservableCollection<PairProjectsViewModel> ProjectsByPair
        {
            get
            {
                if (projectsByPair == null)
                    projectsByPair = new ObservableCollection<PairProjectsViewModel>();
                return projectsByPair;
            }
        }

        public DelegateCommand SelectFileCommand
        {
            get
            {
                if (selectFileCommand == null)
                    selectFileCommand = new DelegateCommand(() => SelectFile());
                return selectFileCommand;
            }
        }

        #endregion

        #region Methods

        public void InitializeProjectsByPair(PairByProject pairByProject)
        {
            ProjectsByPair.Clear();
            foreach (var project in pairByProject.DaysWorkedTogetherByProject)
            {
                PairProjectsViewModel pairProjectsViewModel = new PairProjectsViewModel(pairByProject.FirstEmployeeId, pairByProject.SecondEmployeeId, project.Key, project.Value);
                ProjectsByPair.Add(pairProjectsViewModel);
            }
        }

        public void SelectFile()
        {
            string selectedPath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = ApplicationConstants.CSVDialogFilter,
                Title = ApplicationConstants.CSVDialogTitle
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedPath = openFileDialog.FileName;
                CSVReaderService csvReaderService = new CSVReaderService(selectedPath);
                var employees = csvReaderService.ReadCSVData();
                EmployeesService employeeService = new EmployeesService();
                try
                {
                    if (employees != null && employees.Any())
                    {
                        employees = employees.OrderBy(emp => emp.EmpId).ThenBy(emp => emp.ProjectId).ThenBy(emp => emp.FromDate).ThenBy(emp => emp.ToDate);
                        var pairsByProject = employeeService.PopulatePairs(employees.ToList());
                        if (pairsByProject.Any())
                        {
                            var pairWithMaxWorkingDays = pairsByProject.FirstOrDefault(pp => pp.DaysWorkedTogether == pairsByProject.Max(p => p.DaysWorkedTogether));
                            InitializeProjectsByPair(pairWithMaxWorkingDays);
                        }
                        else
                        {
                            ProjectsByPair.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetCurrentClassLogger();
                    log.Error(ApplicationConstants.ErrorPopulate, ex.Message);
                    ProjectsByPair.Clear();
                }              
            }
        }

        #endregion
    }
}
