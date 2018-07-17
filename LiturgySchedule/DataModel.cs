using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTH.LiturgySchedule
{
    public class DataModel : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Mass> Masses { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public static DataModel Initialize()
        {
            DataModel db;
            using (db = new DataModel())
            {
                AddPeople(db);
                AddRoles(db);
                AddMasses(db);             

                db.SaveChanges();
            }
            return db;
        }

        private static void AddMasses(DataModel db)
        {
            db.Masses.Add(new Mass() { DateTime = new DateTime(2018, 7, 22, 8, 0, 0) });
        }

        private static void AddRoles(DataModel db)
        {
            db.Roles.Add(new Role() { Name = "Reader" });
            db.Roles.Add(new Role() { Name = "Altar Server" });
            db.Roles.Add(new Role() { Name = "Eucharist" });
        }

        private static void AddPeople(DataModel db)
        {
            var list = new List<string>()
            {
                "John", "Mary", "Bill", "Susan"

            };
            list.ForEach(n => { db.People.Add(new Person() { Name = n }); });
        }
    }

    public class Person
    {
        public String Name;
        // prefs
    }

    public class Mass
    {
        public DateTime DateTime;
    }

    public class Assignment
    {
        public Mass Mass;
        public Role Role;
        public Person Person;

    }
    public class Role
    {
        public String Name;
    }
}
