# IOET_exercise_solution

# Overview.

In my procees aplication IOET send to me a technical exam to resolve: 

**IOET EXERCISE:**

The company ACME offers their employees the flexibility to work the hours they want. But due to some external circumstances they need to know what employees have been at the office within the same time frame

The goal of this exercise is to output a table containing pairs of employees and how often they have coincided in the office.

Input: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data. You can include the data from our examples below:

Example 1:

INPUT
RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
ANDRES=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00


OUTPUT:
ASTRID-RENE: 2
ASTRID-ANDRES: 3
RENE-ANDRES: 2

# Architecture.

*A Visual Studio Console Aplication

*Use .netappcore 3.1

*Use MVC architecture, divide the solution in two projects. 

# To Use this aplication. 

This is an example of how you may give instructions on setting up your project locally. To get a local copy up and running follow these simple example steps.

**Step 1: Clone or download this repository**

From your shell or command line:

```git clone https://github.com/adcordova00/IOET_exercise_solution```

or download and extract the repository .zip file.

**Step 2: Build solution and run**

If you prefer to use your shell or command line:

Inside IOET_exercise_solution directory, from your shell or command line:

```dotnet build```

```dotnet run -p ./ConsoleApp2```

If you prefer to use Visual Studio 2019:

- Select Build Solution from Build menu.
- Press F5 to run it.

# How to use this sample

- Run solution (Build solution and run)
- Enter full path of file to be analyzed and press enter.
- Into the solution you can find a .txt archive to test the program.
-On Linux and Mac the slash are ./ on Windows are .\

```
Por favor ingrese la ruta del archivo (Ruta Completa)
```
- Wait the results
- This is an example of the Output: 

```
OUTPUT:

Archivo leido: C:\Users\Anthony\Desktop\Ioet_Solution_C#\employees.txt

ANTHONY - ADRIANA:2
ANTHONY - EMILY:2
ANTHONY - JUAN:4
ANTHONY - LUIS:1
ADRIANA - EMILY:3
ADRIANA - JUAN:2
ADRIANA - LUIS:2
EMILY - JUAN:2
EMILY - LUIS:2
JUAN - LUIS:1
```
