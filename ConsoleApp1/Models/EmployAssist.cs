using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horarios.Models
{
    public class EmployAssist
    {

        public EmployAssist()
        {
            DaySchedules = new List<Schedule>();
            errorRegister = new ErrorRegister();
        }
        public string EmployeeName { get; set; }
        public string FileLine { get; set; }
        public List<Schedule> DaySchedules { get; set; }
        public ErrorRegister errorRegister { get; set; }



    }
}
