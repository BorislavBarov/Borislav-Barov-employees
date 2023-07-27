using CsvHelper.Configuration;
using Pair_of_employees_who_have_worked_together.Models;

namespace Pair_of_employees_who_have_worked_together.Helpers
{
    public class EmployeeRowDTOMapper : ClassMap<EmployeeRowDTO>
    {
        public EmployeeRowDTOMapper()
        {
            Map(p => p.EmpId).Index(0);
            Map(p => p.ProjectId).Index(1);
            Map(p => p.FromDate).TypeConverter<CustomDateTimeConverter>().Index(2);
            Map(p => p.ToDate).TypeConverter<CustomDateTimeConverter>().Index(3);
        }
    }
}
