namespace EFW
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model2 : DbContext
    {
        // Your context has been configured to use a 'Model2' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EFW.Model2' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model2' 
        // connection string in the application configuration file.
        public Model2()
            : base("name=Model2")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tea>().HasMany(i => i.Components).WithMany();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Tea> Teas { get; set; }

        public virtual DbSet<Component> Comps { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}