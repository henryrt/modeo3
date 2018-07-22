using System;
using RTH.LiturgySchedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.IO;


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
/* 
            using (var db = new DataModel(options))
            {
                MemoryStream ms = new MemoryStream();
                var serializer = new DataContractJsonSerializer(typeof(Mass));
                var masses = db.Masses.ToListAsync().GetAwaiter().GetResult();
                serializer.WriteObject(ms, masses);

                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                Console.WriteLine(sr.ReadToEnd());

            }
 */
        }
    }
}
