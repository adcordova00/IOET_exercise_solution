using Horarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horarios.Interfaces
{
    public interface IAssistance
    {

        public IEnumerable<AssistFrecuency> GetConcurrency(List<EmployAssist> concurrencyAttendances);
        public IEnumerable<EmployAssist> GetModel(string filePath, string splitOne, string splitTwo, string dateString);


    }
}