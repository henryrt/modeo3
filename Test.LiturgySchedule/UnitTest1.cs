using Microsoft.EntityFrameworkCore;
using RTH.LiturgySchedule;
using System;
using Xunit;

namespace Test.LiturgySchedule
{
    public class UnitTest1
    {
        
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<DataModel>()
                .UseInMemoryDatabase(databaseName: "TEST001")
                .Options;
            using (var db = new DataModel(options))
            {
                db.Roles.Add(new Role() { Name = "Reader" });
                db.Roles.Add(new Role() { Name = "Altar Server" });
                db.Roles.Add(new Role() { Name = "Eucharist" });
                db.SaveChanges();
            }
            using (var db = new DataModel(options))
            {
                Assert.Equal<int>(3, db.Roles.CountAsync().GetAwaiter().GetResult());
            }

        }
        /* 
        [Fact]
        public void Test3()
        {
            var options = new DbContextOptionsBuilder<DataModel>()
                .UseInMemoryDatabase(databaseName: "TEST002")
                .Options;

            var db = DataModel.Initialize(options);

            Assert.Equal<int>(3, db.Roles.CountAsync().GetAwaiter().GetResult());
            Assert.Equal<int>(4, db.People.CountAsync().GetAwaiter().GetResult());
            Assert.Equal<int>(1, db.Masses.CountAsync().GetAwaiter().GetResult());
            Assert.Equal<int>(0, DataModel.Count(db.Assignments));

        } */
    }
}
