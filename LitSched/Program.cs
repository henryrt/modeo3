using System;
using RTH.LiturgySchedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.IO;
using Newtonsoft.Json;


namespace LitSched
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize SqlLite library
            SQLitePCL.Batteries_V2.Init();
            
            var sb = new SqliteConnectionStringBuilder();
            sb.DataSource = @"C:/temp/sl.db";

            var options = new DbContextOptionsBuilder<DataModel>()
                .UseSqlite<DataModel>(sb.ConnectionString)
                .Options;

            DataModel.Initialize(options);
 
            using (var db = new DataModel(options))
            {
                var masses = db.Masses;

                Console.WriteLine(JsonConvert.SerializeObject( masses ));
                Console.WriteLine(JsonConvert.SerializeObject(db.People));
                Console.WriteLine(JsonConvert.SerializeObject(db.Roles));

            }

        }
    }
}
