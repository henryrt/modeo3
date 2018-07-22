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

        public DataModel() : base() { }
        public DataModel(DbContextOptions<DataModel> options) : base(options) { }

        public static void ResetDatabase(DbContextOptions<DataModel> options) 
        {
            using (var db = new DataModel(options)) 
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                //db.Database.
            }
        }
        public static void Initialize(DbContextOptions<DataModel> options)
        {
            
            using (var db =  new DataModel(options))
            {
                Initialize(db);
            }
        }

        public static void Initialize(DataModel db) 
        {
            AddPeople(db);
            AddRoles(db);
            AddMasses(db);             
            db.SaveChanges();
        }

        private static void AddMasses(DataModel db)
        {
            DateTime start = new DateTime(2018, 7, 22);
            int weeks = 10;
            for (int i = 0; i <weeks; i++)
            {
                start = start.AddHours(-7);
                db.Masses.Add(new Mass() { DateTime = start }); // Sat 5pm

                start = start.AddHours(15);
                db.Masses.Add(new Mass() { DateTime = start }); // Sun 8am

                start = start.AddHours(1.5);
                db.Masses.Add(new Mass() { DateTime = start }); // Sun 9:30

                start = start.AddHours(1.5);
                db.Masses.Add(new Mass() { DateTime = start }); // Sun 11am

                start = start.AddHours(6);
                db.Masses.Add(new Mass() { DateTime = start }); // Sun 5pm

                start = start.AddHours(-17).AddDays(7); // move to next week
            }
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
