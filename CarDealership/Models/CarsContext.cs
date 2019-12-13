namespace CarDealership.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CarsContext : DbContext
    {
        public CarsContext()
            : base("name=CarsContext")
        {
        }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Engine> Engine { get; set; }
        public virtual DbSet<Kit> Kit { get; set; }
        public virtual DbSet<Kit_Option> Kit_Option { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Model_Color> Model_Color { get; set; }
        public virtual DbSet<Model_Engine> Model_Engine { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<OptionType> OptionType { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Vehicle_Option> Vehicle_Option { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Model)
                .WithRequired(e => e.Brand)
                .HasForeignKey(e => e.BrandFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Contract)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ClientFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Color>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Model_Color)
                .WithRequired(e => e.Color)
                .HasForeignKey(e => e.ColorFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Color)
                .HasForeignKey(e => e.ColorFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .Property(e => e.Total_Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Contract>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Contract)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Engine>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Engine>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Engine>()
                .Property(e => e.Power)
                .HasPrecision(4, 0);

            modelBuilder.Entity<Engine>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Engine>()
                .HasMany(e => e.Model_Engine)
                .WithRequired(e => e.Engine)
                .HasForeignKey(e => e.EngineFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Engine>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Engine)
                .HasForeignKey(e => e.EngineFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kit>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Kit>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Kit>()
                .HasMany(e => e.Kit_Option)
                .WithRequired(e => e.Kit)
                .HasForeignKey(e => e.KitFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kit>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Kit)
                .HasForeignKey(e => e.KitFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Kit)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.ModelFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Model_Color)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.ModelFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Model_Engine)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.ModelFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Option)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.ModelFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Option>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.Kit_Option)
                .WithRequired(e => e.Option)
                .HasForeignKey(e => e.OptionFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.Vehicle_Option)
                .WithRequired(e => e.Option)
                .HasForeignKey(e => e.OptionFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OptionType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OptionType>()
                .HasMany(e => e.Option)
                .WithOptional(e => e.OptionType)
                .HasForeignKey(e => e.OptionTypeFK);

            modelBuilder.Entity<Status>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.StatusFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.VIN)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Contract)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Vehicle_Option)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleFK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle_Option>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);
        }
    }
}
