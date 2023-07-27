namespace Pair_of_employees_who_have_worked_together.ViewModels
{
    public class PairProjectsViewModel
    {
        #region Initialize

        public PairProjectsViewModel(long firstEmpId, long secondEmpId, long projectId, long days)
        {
            this.FirstEmployeeId = firstEmpId;
            this.SecondEmployeeId = secondEmpId;
            this.ProjectId = projectId;
            this.DaysWorkedTogether = days;
        }

        #endregion

        #region Properties

        public long FirstEmployeeId { get; set; }

        public long SecondEmployeeId { get; set; }

        public long ProjectId { get; set; }

        public long DaysWorkedTogether { get; set; }

        #endregion
    }
}
