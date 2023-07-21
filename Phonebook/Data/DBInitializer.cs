using Phonebook.Models;
using System.Diagnostics;

namespace Phonebook.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DirectorsDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Directors.Any())
            {
                return;   
            }

            var directors = new Director[]
            {
            new Director{FirstName="Carson",LastName="Alexander", Phone="295942824"},
            new Director{FirstName="Ivan",LastName="Ivanov", Phone="297195905"},
            new Director{FirstName="Andrey",LastName="Sidorov", Phone="295901114"},
            };
            foreach (Director s in directors)
            {
                context.Directors.Add(s);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
            new Department{FirstName="Andrey",LastName="Sidorov", Phone="295901114", DirectorId=1},
            new Department{FirstName="Andrey",LastName="Sidorov", Phone="295901114", DirectorId=2},
            new Department{FirstName="Andrey",LastName="Sidorov", Phone="295901114", DirectorId=2},
            };
            foreach (Department s in departments)
            {
                context.Departments.Add(s);
            }
            context.SaveChanges();

            var workers = new Worker[]
            {
            new Worker{FirstName="Andrey",LastName="Sidorov", Phone="295901114", DepartmentId=1},
            new Worker{FirstName="Andrey",LastName="Sidorov", Phone="295901114", DepartmentId=2},
            new Worker{FirstName="Andrey",LastName="Sidorov", Phone="295901114", DepartmentId=2},
            };
            foreach (Worker e in workers)
            {
                context.Workers.Add(e);
            }
            context.SaveChanges();
        }
    }
}
