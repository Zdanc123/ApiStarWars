using Microsoft.EntityFrameworkCore;
using StarWars.Models;
using StarWars.Repository;
using System;
using Xunit;

namespace SWTests
{
    public class UnitTest1
    {
        [Fact]
        void TestGetAll()
        {
           var context= InitialContext("TestGetAll");
            var TestRepo = new Repository<Character>(context);


            //var options = new DbContextOptionsBuilder<StarWarsContext>().
            //     UseInMemoryDatabase(databaseName: "TestGetAll").
            //     Options;
            //var context = new StarWarsContext(options);
            //var TestRepo = new Repository<Character>(context);
           
            var result = TestRepo.Count();
            Assert.Equal(6, result);

        }

        [Fact]
        void TestGetById()
        {
            var context = InitialContext("TestGetByID");
            var TestRepo = new Repository<Character>(context);
            var result = TestRepo.get(1, null);
            Assert.Equal("Gandalf", result.Name);
        }

        [Fact]
        void TestInsert()
        {
            var context = InitialContext("TestInsert");
            var TestRepo = new Repository<Character>(context);
            var result = TestRepo.insert(new Character { Id = 7, Name = "InsetTest" });
            Assert.NotNull(result);
            Assert.NotNull(TestRepo.get(result.Id, null));


        }

        [Fact]
        void TestDelete()
        {
            var context = InitialContext("TestDelete");
            var TestRepo = new Repository<Character>(context);
             TestRepo.delete(1);
            context.SaveChanges();
            var result = TestRepo.get(1, null);
            Assert.Null(result);
        }




        StarWarsContext InitialContext(string Name)
        {
            var options = new DbContextOptionsBuilder<StarWarsContext>().
                UseInMemoryDatabase(databaseName: Name).
                Options;
            var context = new StarWarsContext(options);
            
            SeedData(context);
            return context;
        }

        void SeedData(StarWarsContext context)
        {
            var character = new[]
            {

                new Character{Id=1,Name="Gandalf"},
                new Character {Id=2,Name="Aragorn"},
                new Character {Id=3,Name="Legolas"},
                new Character {Id=4,Name="Biblo Baggins"},
                new Character {Id=5,Name="Eragorn"},
                new Character {Id=6,Name="Bron"},
            };
            context.Characters.AddRange(character);
            context.SaveChanges();
        }
    }
}
