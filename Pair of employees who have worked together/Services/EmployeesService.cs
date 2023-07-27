using Pair_of_employees_who_have_worked_together.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pair_of_employees_who_have_worked_together.Services
{
    public class EmployeesService
    {
        #region Methods

        public IList<PairByProject> PopulatePairs(IList<EmployeeRowDTO> employeeRowDTOs)
        {
            ///Remove rows when employee overlap by itself
            IList<EmployeeRowDTO> finalEmployeesResult = DistinctOverlapRows(employeeRowDTOs);

            ///Build pairs
            List<PairByProject> pairsByProjects = new List<PairByProject>();

            for (int i = 0; i <= finalEmployeesResult.Count() - 1; i++)
            {
                for (int j = i + 1; j <= finalEmployeesResult.Count() - 1; j++)
                {
                    EmployeeRowDTO firstEmpl = finalEmployeesResult[i];
                    EmployeeRowDTO secondEmpl = finalEmployeesResult[j];

                    if (firstEmpl.EmpId == secondEmpl.EmpId)
                        continue;

                    if (firstEmpl.ProjectId == secondEmpl.ProjectId && HasDaysTogether(firstEmpl, secondEmpl))
                    {
                        int overlapDays = CalculateDaysTogether(firstEmpl, secondEmpl);
                        int projectId = firstEmpl.ProjectId;

                        if (overlapDays > 0)
                        {
                            if (pairsByProjects.Any(pbp => pbp.FirstEmployeeId == firstEmpl.EmpId && pbp.SecondEmployeeId == secondEmpl.EmpId))
                            {
                                PairByProject contextPairProject = 
                                            pairsByProjects.FirstOrDefault(pbp => pbp.FirstEmployeeId == firstEmpl.EmpId && pbp.SecondEmployeeId == secondEmpl.EmpId);

                                if (contextPairProject.DaysWorkedTogetherByProject.ContainsKey(projectId))
                                {
                                    contextPairProject.DaysWorkedTogetherByProject[projectId] += overlapDays;
                                }
                                else
                                {
                                    contextPairProject.DaysWorkedTogetherByProject[projectId] = overlapDays;
                                }
                            }
                            else
                            {
                                PairByProject pairByProject = new PairByProject();
                                pairByProject.FirstEmployeeId = firstEmpl.EmpId;
                                pairByProject.SecondEmployeeId = secondEmpl.EmpId;
                                pairByProject.DaysWorkedTogetherByProject.Add(firstEmpl.ProjectId, overlapDays);
                                pairsByProjects.Add(pairByProject);
                            }
                        }
                    }
                }
            }

            return pairsByProjects;
        }

        private IList<EmployeeRowDTO> DistinctOverlapRows(IList<EmployeeRowDTO> employeeRowDTOs)
        {
            IList<EmployeeRowDTO> finalEmployeesResult = new List<EmployeeRowDTO>();

            for (int i = 0; i <= employeeRowDTOs.Count() - 1; i++)
            {
                if (i >= employeeRowDTOs.Count() - 1)
                {
                    finalEmployeesResult.Add(employeeRowDTOs[i]);
                    break;
                }

                EmployeeRowDTO firstEmpl = employeeRowDTOs[i];
                EmployeeRowDTO secondEmpl = employeeRowDTOs[i + 1];

                EmployeeRowDTO lastEmployeeForFinalResult = finalEmployeesResult.LastOrDefault();
                if (lastEmployeeForFinalResult != null && 
                    lastEmployeeForFinalResult.EmpId == firstEmpl.EmpId && 
                    firstEmpl.ProjectId == lastEmployeeForFinalResult.ProjectId && 
                    HasDaysTogether(lastEmployeeForFinalResult, firstEmpl))
                {
                    lastEmployeeForFinalResult.ToDate = firstEmpl.ToDate;
                    continue;
                }
                else if (firstEmpl.EmpId == secondEmpl.EmpId && firstEmpl.ProjectId == secondEmpl.ProjectId && HasDaysTogether(firstEmpl, secondEmpl))
                {
                    EmployeeRowDTO newEmployee = new EmployeeRowDTO 
                    { 
                      EmpId = firstEmpl.EmpId, 
                      ProjectId = firstEmpl.ProjectId, 
                      FromDate = firstEmpl.FromDate, 
                      ToDate = secondEmpl.ToDate 
                    };
                    finalEmployeesResult.Add(newEmployee);
                    i++;
                }
                else
                {
                    finalEmployeesResult.Add(firstEmpl);
                }
            }

            return finalEmployeesResult;
        }

        private int CalculateDaysTogether(EmployeeRowDTO firstEmpl, EmployeeRowDTO secondEmpl)
        {
            DateTime periodStartDate =
                firstEmpl.FromDate.Date < secondEmpl.FromDate.Date ? secondEmpl.FromDate.Date : firstEmpl.FromDate.Date;

            DateTime periodEndDate =
                firstEmpl.ToDate.Date < secondEmpl.ToDate.Date ? firstEmpl.ToDate.Date : secondEmpl.ToDate.Date;

            return Math.Abs((periodEndDate - periodStartDate).Days);
        }

        private bool HasDaysTogether(EmployeeRowDTO firstEmpl, EmployeeRowDTO secondEmpl)
        {
            return firstEmpl.FromDate.Date <= secondEmpl.ToDate.Date && firstEmpl.ToDate.Date >= secondEmpl.FromDate.Date;
        }

        #endregion
    }
}
