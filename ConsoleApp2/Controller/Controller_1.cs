using Horarios.Interfaces;
using Horarios.Models;
using Horarios.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Controller
{
    public class Controller1
    {
        private IAssistance _attendanceRepository { get; set; }

        public Controller1(IAssistance attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        //
        // Se encarga de procesar el archivo .txt
        public void Index()
        {
            List<AssistFrecuency> employeesConcurrency = new List<AssistFrecuency>();
            IEnumerable<EmployAssist> employeesAttendance = new List<EmployAssist>();

            string filePath = @"C:\temp\Ioet.txt";

            MessagesPrint("INPUT_FILEPATH", filePath);
            string filePathAux = Console.ReadLine();
            if (filePathAux.Trim().Length > 0)
            {
                filePath = filePathAux.Trim();
            }
            if (!File.Exists(filePath))
            {
                MessagesPrint("FILE_NOEXIST");
            }
            else
            {
                employeesAttendance = _attendanceRepository.GetModel(filePath, "=", ",", "1900/01/01");

                ErrorPrint(filePath, employeesAttendance); //Error de registro

                employeesConcurrency = _attendanceRepository.GetConcurrency(employeesAttendance.ToList()).ToList();
                if (employeesConcurrency.Count > 0)
                {
                    var emploreeAttendanceViewModel = new EmployViewModels();

                    emploreeAttendanceViewModel.File_Path = filePath;
                    emploreeAttendanceViewModel.ConcurrencyAttendanceProcesedList = employeesConcurrency;

                    MessagesPrint("OUTPUT_BEGIN", filePath);
                    foreach (var element in emploreeAttendanceViewModel.ConcurrencyAttendanceProcesedList)
                    {
                        Console.WriteLine(element.PeopleName + ":" + element.ConcurrencyNumber);
                    }
                    MessagesPrint("OUTPUT_END");
                }
                else
                {
                    MessagesPrint("OUTPUT_EMPTY");
                    MessagesPrint("OUTPUT_END");
                }
            }
            Console.ReadLine();
        }

        //
        // Imprime las coincidencias en la asistencia de los empleados 
        //    Imprime la ruta y el nombre del archivo .txt
        public void MessagesPrint(string printCaso, string filePath = null)
        {
            switch (printCaso.ToUpper())
            {
                case "INPUT_FILEPATH":
                    Console.Write("Por favor ingrese la ruta del archivo (Ruta Completa)");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    break;
                case "FILE_NOEXIST":
                    Console.WriteLine("");
                    Console.WriteLine("*********************************************");
                    Console.Write("El archivo no existe o no se encontro en la ruta especificada.");
                    Console.WriteLine("");
                    Console.WriteLine("*********************************************");
                    Console.WriteLine("");
                    break;
                case "OUTPUT_BEGIN":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("OUTPUT:");
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("Archivo leido: " + filePath);
                    Console.WriteLine("");
                    break;
                case "OUTPUT_END":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.Write("Gracias por usar este programa.");
                    Console.WriteLine("");
                    break;
                case "OUTPUT_EMPTY":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("OUTPUT:");
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("No hay coincidencia entre empleados ");
                    Console.WriteLine("");
                    break;
                case "ERROR_BEGIN":
                    Console.WriteLine("");
                    Console.WriteLine("**********************");
                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("EXISTEN ERRORES EN LOS DATOS INGRESADOS:");
                    Console.ResetColor();
                    Console.WriteLine("");
                    Console.WriteLine("Archivo leido: " + filePath);
                    Console.WriteLine("");
                    break;
                case "ERROR_END":
                    Console.WriteLine("");
                    break;
                default:
                    Console.WriteLine("");
                    Console.WriteLine("Archivo no soportado.");
                    Console.WriteLine("");
                    break;
            }

        }

        //
        // Imprime errores en la consola 
        //   Sobre el input de los datos
        private void ErrorPrint(string filePath, IEnumerable<EmployAssist> employeesAttendance)
        {
            bool errorExist = false;
            foreach (var element in employeesAttendance)
            {
                if (element.errorRegister.HasError)
                {
                    errorExist = true;
                    break;
                }
            }
            if (errorExist)
            {
                MessagesPrint("ERROR_BEGIN", filePath);

                foreach (var element in employeesAttendance)
                {
                    if (element.errorRegister.HasError)
                    {
                        var errorString = "Line number: " + element.errorRegister.FileLineNumber + " ";
                        errorString = errorString + "Line content: " + element.errorRegister.FileLine;
                        Console.WriteLine(errorString);
                    }
                }
                MessagesPrint("ERROR_END", filePath);
            }
        }

    }
}

