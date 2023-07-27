using System.Collections.Generic;
using System.Linq;

namespace Pair_of_employees_who_have_worked_together.Models
{
    public class PairByProject
    {
        public long FirstEmployeeId { get; set; }

        public long SecondEmployeeId { get; set; }

        public long DaysWorkedTogether => DaysWorkedTogetherByProject.Values.Sum();

        /// <summary>
        /// If use database will be object with different table
        /// </summary>
        public Dictionary<long, long> DaysWorkedTogetherByProject { get; } = new Dictionary<long, long>();
    }
}
