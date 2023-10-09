using CRUD.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Airplane> Airplanes { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<Airplane>().ToTable("Airplanes");
        modelBuilder.Entity<City>().ToTable("Cities");
        modelBuilder.Entity<Person>().ToTable("Persons");

        modelBuilder.Entity<Country>().HasData(new Country
        {
            CountryId = new Guid("FC8D48A1-A7FD-4DC4-88C0-FA512FF879FF"),
            Name = "Hungary"
        });

        modelBuilder.Entity<Country>().HasData(new Country
        {
            CountryId = new Guid("0919F2B8-96C4-4284-BF69-02881447ABE1"),
            Name = "Spain"
        });

        modelBuilder.Entity<Country>().HasData(new Country
        {
            CountryId = new Guid("EA5DA711-9627-4191-BF9C-785EDA9D7C6F"),
            Name = "Germany"
        });

        modelBuilder.Entity<City>().HasData(new City { CityId = Guid.NewGuid(), Name = "Budapest", Population = 300000, PostalCode = "1058" });
        modelBuilder.Entity<Person>().HasData(new Person { PersonId = new Guid("FC8D48A1-A7FD-4DC4-88C0-FA512FF879FF"), Name = "Test Jacob", Address = "Test address", CountryId = new Guid("FC8D48A1-A7FD-4DC4-88C0-FA512FF879FF"), PersonalID = "123456" });
        modelBuilder.Entity<Airplane>().HasData(new Airplane
        {
            AirplaneId = Guid.NewGuid(),
            Manufacturer = "Boeing",
            Price = 15000000,
            Production = DateTime.Now,
            Type = "737-700",
        });

        modelBuilder.Entity<Person>().Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(50)");
        modelBuilder.Entity<Person>().Property(x => x.Gender).HasColumnName("Gender").HasColumnType("bit");
        modelBuilder.Entity<Person>().Property(x => x.Address).HasColumnName("Address").HasColumnType("nvarchar(100)");
        modelBuilder.Entity<Person>().Property(x => x.DateOfBirth).HasColumnName("Date of birth");
        modelBuilder.Entity<Person>().HasIndex(x => x.PersonalID).IsUnique();

        modelBuilder.Entity<City>().Property(x => x.PostalCode).HasColumnName("Postal Code").HasColumnType("varchar(8)");
        modelBuilder.Entity<City>().Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(50)");

        modelBuilder.Entity<Airplane>().Property(x => x.Manufacturer).HasColumnName("Manufacturer").HasColumnType("nvarchar(20)");
        modelBuilder.Entity<Airplane>().Property(x => x.Type).HasColumnName("Type").HasColumnType("nvarchar(20)");

        modelBuilder.Entity<Country>().Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(50)");


        //Table relations
        modelBuilder.Entity<City>().HasOne(x => x.Country).WithMany(x => x.Cities).HasForeignKey(x => x.CountryId);
    }
}
