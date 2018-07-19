using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace RTH.LiturgySchedule
{
    public class DataModel : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Mass> Masses { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DataModel(DbContextOptions options) : base(options) { }

        public static DataModel Initialize(DbContextOptions options)
        {

            DataModel db;
            db = new DataModel(options);
            {
                // clear database
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

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
            AddList<Person>(db.People, list.Select(n => new Person() { Name = n }));
        }

        public static int Count<T>(DbSet<T> set) where T: class
        {
            return set.Count();
        }

        private static int AddList<T>(DbSet<T> set, IEnumerable<T> list) where T : class
        {
            var count = 0;
            foreach(T item in list)
            {
                try
                {
                    set.Add(item);
                }
                catch (ArgumentException ex)
                {
                    if (ex.Message.Contains("item with the same key has already been added"))
                    {
                    // ignore
                }
                    else throw;
                }
            }
            return count;

        }
    }

    public class Person
    {
        [Key]
        public String Name { get; set; }
        // prefs
    }
    
    public class Mass
    {
        [Key]
        
        public DateTime DateTime {get; set; }
    }
    
    public class Assignment
    {
        public int AssignmentId { set; get; }
        public Mass Mass;
        public Role Role;
        public Person Person;

    }
    
    public class Role
    {
        [Key]
        public String Name { get; set; }
    }
}
