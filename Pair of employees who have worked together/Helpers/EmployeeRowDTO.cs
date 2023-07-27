using CsvHelper.Configuration.Attributes;
using System;

namespace Pair_of_employees_who_have_worked_together.Models
{
    public class EmployeeRowDTO
    {
        [Index(0)]
        public int EmpId { get; set; }

        [Index(1)]
        public int ProjectId { get; set; }

        [Index(2)]
        public DateTime FromDate { get; set; }

        [Index(3)]
        public DateTime ToDate { get; set; }
    }
}
