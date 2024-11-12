using System;
using System.Collections.Generic;
using System.Linq;

namespace hw
{
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }
    }

    class Department
    {
        public int Id { get; set; }
        public string Country { get; set; }   
        public string City { get; set; }
    }

    class Program
    {
        static void Main()
        {
            List<Department> departments = new List<Department>()
            {
                new Department(){ Id = 1, Country = "Ukraine", City = "Lviv" },
                new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                new Department(){ Id = 3, Country = "France", City = "Paris" },
                new Department(){ Id = 4, Country = "Ukraine", City = "Odesa" }
            };

            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
                new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
                new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
                new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
                new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
                new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
                new Employee() { Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4 }
            };

            //1
            // linq 
            var solutions_1 = from emp in employees
                         join dep in departments on emp.DepId equals dep.Id
                         where dep.Country == "Ukraine" && dep.City != "Odesa"
                         select new { emp.FirstName, emp.LastName };

            // linq method 
            var solution_1 = employees.Join(departments,
                                        emp => emp.DepId,
                                        dep => dep.Id,
                                        (emp, dep) => new { emp, dep })
                                    .Where(x => x.dep.Country == "Ukraine" && x.dep.City != "Odesa")
                                    .Select(x => new { x.emp.FirstName, x.emp.LastName });

            Console.WriteLine("Employees in Ukraine but not in Odesa:");
            foreach (var item in solutions_1 )
            {
                Console.WriteLine($"Employee name: {item.FirstName} {item.LastName}");
            }

            Console.WriteLine("\nEmployees in Ukraine but not in Odesa:");
            foreach (var item in solution_1)
            {
                Console.WriteLine($"Name of employee: {item.FirstName} {item.LastName}");
            }

            //2
            // linq
            var solutions_2 = (from dep in departments
                          select dep.Country).Distinct();

            // linq method 
            var solution_2 = departments.Select(dep => dep.Country).Distinct();

            Console.WriteLine("\nList of countries:");
            foreach (var country in solutions_2)
            {
                Console.WriteLine($"Country: {country}");
            }

            Console.WriteLine("\nList of countries:");
            foreach (var country in solution_2)
            {
                Console.WriteLine($"Country name: {country}");
            }

            //3
            // linq
            var solutions_3 = (from emp in employees
                          where emp.Age > 25
                          select emp).Take(3);

            // linq method
            var solution_3 = employees.Where(emp => emp.Age > 25).Take(3);

            Console.WriteLine("\nFirst 3 employees older than 25:");
            foreach (var emp in solutions_3)
            {
                Console.WriteLine($"Name: {emp.FirstName} {emp.LastName}, Age is {emp.Age}");
            }

            Console.WriteLine("\nFirst 3 employees older than 25:");
            foreach (var emp in solution_3)
            {
                Console.WriteLine($"Employee: {emp.FirstName} {emp.LastName} - Age: {emp.Age}");
            }

            //4
            // linq
            var solutions_4 = from emp in employees
                         join dep in departments on emp.DepId equals dep.Id
                         where dep.City == "Kyiv" && emp.Age > 23
                         select new { emp.FirstName, emp.LastName, emp.Age };

            // linq method
            var solution_4 = employees.Join(departments,
                                        emp => emp.DepId,
                                        dep => dep.Id,
                                        (emp, dep) => new { emp, dep })
                                   .Where(x => x.dep.City == "Kyiv" && x.emp.Age > 23)
                                   .Select(x => new { x.emp.FirstName, x.emp.LastName, x.emp.Age });

            Console.WriteLine("\nEmployees from Kyiv older than 23:");
            foreach (var item in solutions_4)
            {
                Console.WriteLine($"Employee: {item.FirstName} {item.LastName}, Age: {item.Age}");
            }

            Console.WriteLine("\nEmployees from Kyiv older than 23:");
            foreach (var item in solution_4)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName} (Age: {item.Age})");
            }
        }
    }
}
