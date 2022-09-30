using Horarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horarios.ViewModels
{
    public class EmployViewModels
    {
        public EmployViewModels()
        {
            ConcurrencyAttendanceProcesedList = new List<AssistFrecuency>();
        }

        public List<AssistFrecuency> ConcurrencyAttendanceProcesedList { get; set; }

        public string File_Path { get; set; }
    }
}
