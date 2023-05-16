using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.DAL;

public partial class S25646Context : DbContext
{
    public S25646Context()
    {
    }

    public S25646Context(DbContextOptions<S25646Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<FireTruck> FireTrucks { get; set; }

    public virtual DbSet<FireTruckAction> FireTruckActions { get; set; }

    public virtual DbSet<Firefighter> Firefighters { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Medicament> Medicaments { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderMenuItem> OrderMenuItems { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PersonalDatum> PersonalData { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TypeOfOrder> TypeOfOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog =s25646;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.IdAction).HasName("Action_pk");

            entity.ToTable("Action");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Address_pk");

            entity.ToTable("Address");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Client_pk");

            entity.ToTable("Client");

            entity.Property(e => e.IdClient).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.Property(e => e.Pesel).HasMaxLength(120);
            entity.Property(e => e.Telephone).HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("Client_Trip_pk");

            entity.ToTable("Client_Trip");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Client");

            entity.HasOne(d => d.IdTripNavigation).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Trip");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("Country_pk");

            entity.ToTable("Country");

            entity.Property(e => e.IdCountry).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.IdTrips).WithMany(p => p.IdCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryTrip",
                    r => r.HasOne<Trip>().WithMany()
                        .HasForeignKey("IdTrip")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Trip"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Country_Trip_Country"),
                    j =>
                    {
                        j.HasKey("IdCountry", "IdTrip").HasName("Country_Trip_pk");
                        j.ToTable("Country_Trip");
                    });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_pk");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddressId).HasColumnName("Address_Id");
            entity.Property(e => e.DataId).HasColumnName("Data_Id");
            entity.Property(e => e.Passsword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Customer_Address");

            entity.HasOne(d => d.Data).WithMany(p => p.Customers)
                .HasForeignKey(d => d.DataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Customer_Data");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("Doctor_pk");

            entity.ToTable("Doctor");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Employee_pk");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DataId).HasColumnName("Data_Id");
            entity.Property(e => e.JobId).HasColumnName("Job_Id");

            entity.HasOne(d => d.Data).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_Data");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_Job");
        });

        modelBuilder.Entity<FireTruck>(entity =>
        {
            entity.HasKey(e => e.IdFiretruck).HasName("FireTruck_pk");

            entity.ToTable("FireTruck");

            entity.Property(e => e.OperationNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<FireTruckAction>(entity =>
        {
            entity.HasKey(e => new { e.IdFiretruck, e.IdAction }).HasName("FireTruck_Action_pk");

            entity.ToTable("FireTruck_Action");

            entity.Property(e => e.AssignmentDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdActionNavigation).WithMany(p => p.FireTruckActions)
                .HasForeignKey(d => d.IdAction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_4_Action");

            entity.HasOne(d => d.IdFiretruckNavigation).WithMany(p => p.FireTruckActions)
                .HasForeignKey(d => d.IdFiretruck)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_4_FireTruck");
        });

        modelBuilder.Entity<Firefighter>(entity =>
        {
            entity.HasKey(e => e.IdFirefighter).HasName("Firefighter_pk");

            entity.ToTable("Firefighter");

            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasMany(d => d.IdActions).WithMany(p => p.IdFirefighters)
                .UsingEntity<Dictionary<string, object>>(
                    "FirefighterAction",
                    r => r.HasOne<Action>().WithMany()
                        .HasForeignKey("IdAction")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Firefighter_Action_Action"),
                    l => l.HasOne<Firefighter>().WithMany()
                        .HasForeignKey("IdFirefighter")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Firefighter_Action_Firefighter"),
                    j =>
                    {
                        j.HasKey("IdFirefighter", "IdAction").HasName("Firefighter_Action_pk");
                        j.ToTable("Firefighter_Action");
                    });
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Job_pk");

            entity.ToTable("Job");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.HasKey(e => e.IdMedicament).HasName("Medicament_pk");

            entity.ToTable("Medicament");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Menu_pk");

            entity.ToTable("Menu");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MenuItem_pk");

            entity.ToTable("MenuItem");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MenuId).HasColumnName("Menu_Id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MenuItem_Menu");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pk");

            entity.ToTable("Order", tb => tb.HasTrigger("PricingInfo"));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.HourAway).HasColumnType("datetime");
            entity.Property(e => e.HourOrder).HasColumnType("datetime");
            entity.Property(e => e.TypeOfOrderId).HasColumnName("TypeOfOrder_Id");

            entity.HasOne(d => d.TypeOfOrder).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TypeOfOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_TypeOfOrder");

            entity.HasMany(d => d.Customers).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderCustomer",
                    r => r.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Order_Customer_Customer"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Order_Customer_Order"),
                    j =>
                    {
                        j.HasKey("OrderId", "CustomerId").HasName("Order_Customer_pk");
                        j.ToTable("Order_Customer");
                        j.IndexerProperty<int>("OrderId").HasColumnName("Order_Id");
                        j.IndexerProperty<int>("CustomerId").HasColumnName("Customer_Id");
                    });

            entity.HasMany(d => d.Employees).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderEmployee",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Order_Employee_Employee"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Order_Employee_Order"),
                    j =>
                    {
                        j.HasKey("OrderId", "EmployeeId").HasName("Order_Employee_pk");
                        j.ToTable("Order_Employee");
                        j.IndexerProperty<int>("OrderId").HasColumnName("Order_Id");
                        j.IndexerProperty<int>("EmployeeId").HasColumnName("Employee_Id");
                    });
        });

        modelBuilder.Entity<OrderMenuItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.MenuItemId }).HasName("Order_MenuItem_pk");

            entity.ToTable("Order_MenuItem", tb => tb.HasTrigger("BlockAddingMenuItemsToReadyOrder"));

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.MenuItemId).HasColumnName("MenuItem_id");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderMenuItems)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_MenuItem_MenuItem");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderMenuItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_MenuItem_Order");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.IdPatient).HasName("Patient_pk");

            entity.ToTable("Patient");

            entity.Property(e => e.Birthdate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<PersonalDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PersonalData_pk");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.IdPrescription).HasName("Prescription_pk");

            entity.ToTable("Prescription");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DueDate).HasColumnType("date");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Patient");
        });

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("Prescription_Medicament_pk");

            entity.ToTable("Prescription_Medicament");

            entity.Property(e => e.Details).HasMaxLength(100);

            entity.HasOne(d => d.IdMedicamentNavigation).WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(d => d.IdMedicament)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Medicament_Medicament");

            entity.HasOne(d => d.IdPrescriptionNavigation).WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(d => d.IdPrescription)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Medicament_Prescription");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("Trip_pk");

            entity.ToTable("Trip");

            entity.Property(e => e.IdTrip).ValueGeneratedNever();
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<TypeOfOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TypeOfOrder_pk");

            entity.ToTable("TypeOfOrder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
