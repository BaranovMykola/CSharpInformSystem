using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;

namespace TaxiCore
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model2 : DbContext
    {
        // Your context has been configured to use a 'Model2' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TaxiCore.Model2' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model2' 
        // connection string in the application configuration file.
        public Model2()
            : base("name=Model2")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxiPark>().HasMany(i => i.Taxis).WithMany();
            modelBuilder.Entity<TaxiPark>().HasMany(i => i.clientsQueue).WithMany();
            base.OnModelCreating(modelBuilder);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Taxi> Taxis { get; set; }
        public virtual DbSet<TaxiPark> TaxiParks { get; set; }
        public virtual DbSet<A> As { get; set; }
    }
}

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
