using Horarios.Enum;
using Horarios.Interfaces;
using Horarios.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horarios.Repos
{
    public class Repositories : IAssistance
    {


        public IEnumerable<AssistFrecuency> GetConcurrency(List<EmployAssist> employeesAttendance)
        {
            List<AssistFrecuency> employeesAttendanceConcurrency = new List<AssistFrecuency>();
            List<EmployAssist> emploreeAttendanceB = new List<EmployAssist>();

            emploreeAttendanceB = employeesAttendance;

            for (var i = 0; i < employeesAttendance.Count; i++)
            {
                var elementBase = employeesAttendance[i];
                var daySchedulesBase = elementBase.DaySchedules;
                int cont = 0;

                if (i + 1 < emploreeAttendanceB.Count)
                {
                    for (var j = i + 1; j <= emploreeAttendanceB.Count - 1; j++)
                    {
                        var elementB = emploreeAttendanceB[j];

                        var elementIterator = emploreeAttendanceB[j];
                        if (elementBase.EmployeeName != elementIterator.EmployeeName)
                        {
                            var daySchedulesBaseB = elementIterator.DaySchedules;

                            cont = GetScheduleConcurrency(daySchedulesBase, daySchedulesBaseB);
                            if (cont > 0)
                            {
                                var employeeAttendanceConcurrency = new AssistFrecuency();
                                employeeAttendanceConcurrency.ConcurrencyNumber = cont;
                                employeeAttendanceConcurrency.PeopleName = elementBase.EmployeeName + " - " + elementIterator.EmployeeName;
                                employeesAttendanceConcurrency.Add(employeeAttendanceConcurrency);
                            }

                        }
                    }
                }
            }

            return employeesAttendanceConcurrency;
        }

        private int GetScheduleConcurrency(List<Schedule> List1, List<Schedule> List2)
        {
            int cont = 0;

            foreach (var elementA in List1)
            {
                foreach (var elementB in List2)
                {
                    if (elementA.DayName.ToUpper() == elementB.DayName.ToUpper())
                    {
                        if (elementA.DateTimeBegin >= elementB.DateTimeBegin && elementA.DateTimeEnd <= elementB.DateTimeEnd)
                        {
                            cont++;
                            break;
                        }
                    }
                }
            }

            return cont;
        }

        public IEnumerable<EmployAssist> GetModel(string filePath, string splitOne, string splitTwo, string dateString)
        {
            var employeesAttendance = new List<EmployAssist>();
            int lineNumber = 0;

            using (StreamReader ReaderObject = new StreamReader(filePath))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    if (Line.Trim() != "")
                    {
                        string[] words = Line.Split(splitOne);
                        string[] sectionDays = words[1].Split(splitTwo);
                        var employeeAttendance = new EmployAssist();

                        employeeAttendance.EmployeeName = words[0].Trim();
                        employeeAttendance.FileLine = Line.Trim();
                        employeeAttendance.errorRegister.HasError = false;
                        bool isOk = true;

                        foreach (var element in sectionDays)
                        {
                            employeeAttendance = AnalizeLine(employeeAttendance, element, dateString);
                            if (employeeAttendance.EmployeeName == null)
                            {
                                lineNumber++;
                                employeeAttendance.errorRegister.HasError = true;
                                employeeAttendance.errorRegister.FileLine = Line.Trim();
                                employeeAttendance.errorRegister.FileLineNumber = lineNumber;
                                employeeAttendance.errorRegister.File_Path = filePath;

                                break; //there was a error with Line processing.
                            }
                        }
                        employeesAttendance.Add(employeeAttendance);
                    }
                    else
                    {
                        lineNumber++;
                    }
                }
            }


            return employeesAttendance;
        }

        private EmployAssist AnalizeLine(EmployAssist employeeAttendance, string element, string dateString)
        {
            DateTime dateTimeBegin;
            DateTime dateTimeEnd;
            bool isOk = true;
            string dayName = element.Substring(0, 2);
            string dateTimeString = element.Substring(2, 5);
            //dateName
            if (validateData("IS_DAY", dayName, "") && isOk)
            {
                if (dateString == "")
                {
                    dateTimeString = element.Substring(2, 5);
                }
                else
                {
                    dateTimeString = dateString + " " + element.Substring(2, 5);
                }
                //dateTimeBegin
                if (validateData("IS_DATETIME", dateTimeString, "") && isOk)
                {
                    dateTimeBegin = Convert.ToDateTime(dateTimeString);

                    if (dateString == "")
                    {
                        dateTimeString = element.Substring(8, 5);
                    }
                    else
                    {
                        dateTimeString = dateString + " " + element.Substring(8, 5);
                    }
                    //dateTimeEnd
                    if (validateData("IS_DATETIME", dateTimeString, "") && isOk)
                    {
                        dateTimeEnd = Convert.ToDateTime(dateTimeString);

                        Schedule schedulePerDay = new Schedule()
                        {
                            DayName = dayName,
                            DateTimeBegin = dateTimeBegin,
                            DateTimeEnd = dateTimeEnd
                        };

                        employeeAttendance.DaySchedules.Add(schedulePerDay);
                    }
                    else
                    {
                        isOk = false; //dateTimeEnd
                    }
                }
                else
                {
                    isOk = false; //dateTimeBegin
                }
            }
            else
            {
                isOk = false; //dateName
            }

            if (isOk)
            {
                return employeeAttendance;
            }

            return new EmployAssist();
        }

        private bool validateData(string option, string valor, string stringToSearch)
        {
            bool notFound = false;
            switch (option.ToUpper())
            {
                case "IS_DAY":
                    if (Days.IsDefined(typeof(Days), valor))
                    {
                        return true;
                    }
                    return notFound;

                case "IS_DATETIME":
                    DateTime temp;
                    if (DateTime.TryParse(valor, out temp))
                        return true;

                    return notFound;

                case "HAS_STRING":
                    if (valor.Contains(stringToSearch))
                        return true;

                    return notFound;
                default:
                    break;
            }
            return false;
        }
    }
}
