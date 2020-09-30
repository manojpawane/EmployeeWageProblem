using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWageV2
{
    public class EmpWageBuilder : IComputeWage, IComputeWageV2
    {
        public const int IS_FULL_TIME = 1;
        public const int IS_PART_TIME = 2;

        private LinkedList<CompanyEmpWage> companyEmpWagesList;
        private Dictionary<string, CompanyEmpWage> companyToEmpWageMap;


        public EmpWageBuilder()
        {
            this.companyEmpWagesList = new LinkedList<CompanyEmpWage>();
            this.companyToEmpWageMap = new Dictionary<string, CompanyEmpWage>();
        }

        public void AddCompanyEmpWage(string company, int empRatePerHour, int numOfWorkingDays, int maxHoursPerMonth)
        {
            CompanyEmpWage companyEmpWage = new CompanyEmpWage(company, empRatePerHour, numOfWorkingDays, maxHoursPerMonth);
            this.companyEmpWagesList.AddLast(companyEmpWage);
            this.companyToEmpWageMap.Add(company, companyEmpWage);
        }

        public void ComputeEmpWage()
        {
            foreach (CompanyEmpWage companyEmpWage in this.companyEmpWagesList)
            {
                int totalEmpWageTemp = this.ComputeEmpWage(companyEmpWage);
                companyEmpWage.SettotalEmpWage(totalEmpWageTemp);
                Console.WriteLine(companyEmpWage.toString());
            }
        }

        public int ComputeEmpWage(CompanyEmpWage companyEmpWage)
        {
            int totalEmpHours = 0;
            int workingDays = 0;
            int empHours = 0;
            int totalWagePerDay = 0;
            int totalWagePerMonth = 0;
            while (totalEmpHours < companyEmpWage.maxHoursPerMonth && workingDays < companyEmpWage.numOfWorkingDays)
            {
                EmpWageBuilder empWageBuilder = new EmpWageBuilder();
                empHours = empWageBuilder.GetWorkingHours();

                if (totalEmpHours == 96)
                {
                    empHours = 4;
                }
                if (empHours != 0)
                {
                    workingDays++;
                    totalEmpHours = empHours + totalEmpHours;
                    totalWagePerDay = empHours * companyEmpWage.ratePerHours;
                    totalWagePerMonth += totalWagePerDay;
                    Console.WriteLine("Total Wage per Day.." + totalWagePerDay);
                }
            }
            return totalWagePerMonth;

        }

        public int GetWorkingHours()
        {
            const int FULL_TIME = 1;
            const int PART_TIME = 2;
            int empHours = 0;
            Random random = new Random();

            int empCheck = random.Next(0, 3);
            switch (empCheck)
            {
                case FULL_TIME:
                    empHours = 8;
                    break;
                case PART_TIME:
                    empHours = 4;
                    break;
                default:
                    empHours = 0;
                    break;
            }
            return empHours;
        }

        public int GetTotalWage(string company)
        {
            return this.companyToEmpWageMap[company].totalEmpWage;
        }
    }
}
