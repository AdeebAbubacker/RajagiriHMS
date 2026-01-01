using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using RajagiriHMS.Models;
using RajagriHMS.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RajagiriHMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ==========================
        // Master Tables
        // ==========================
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        // ==========================
        // Core Hospital Tables
        // ==========================
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSlot> DoctorSlots { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // ==========================
        // Clinical Tables
        // ==========================
        public DbSet<Vital> Vitals { get; set; }
        public DbSet<LabRequest> LabRequests { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        // ==========================
        // Operations Tables
        // ==========================
        public DbSet<PharmacyDispense> PharmacyDispenses { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }

        // ==========================
        // Audit
        // ==========================
        //public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Slot> Slots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicit table names
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<DoctorSlot>().ToTable("DoctorSlots");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Slot>().ToTable("Slots");

            // Configure Vital entity with decimal precision
            modelBuilder.Entity<Vital>(entity =>
            {
                entity.ToTable("Vitals");
                entity.Property(v => v.Temperature)
                      .HasPrecision(5, 2); // e.g., 99.99
            });

            modelBuilder.Entity<LabRequest>().ToTable("LabRequests");
            modelBuilder.Entity<Prescription>().ToTable("Prescriptions");
            modelBuilder.Entity<PharmacyDispense>().ToTable("PharmacyDispenses");

            // Configure Billing entity with decimal precision
            modelBuilder.Entity<Billing>(entity =>
            {
                entity.ToTable("Billings");
                entity.Property(b => b.ConsultationCharge)
                      .HasPrecision(10, 2); // e.g., 99999999.99
                entity.Property(b => b.LabCharge)
                      .HasPrecision(10, 2);
                entity.Property(b => b.MedicineCharge)
                      .HasPrecision(10, 2);
                entity.Property(b => b.TotalAmount)
                      .HasPrecision(10, 2);
            });

            modelBuilder.Entity<InsuranceClaim>().ToTable("InsuranceClaims");

            // ==========================
            // ADD THESE RELATIONSHIP CONFIGURATIONS
            // ==========================

            // Appointment -> Slot relationship
            modelBuilder.Entity<Appointment>()
                .HasOne<Slot>()  // Appointment has one Slot
                .WithMany()      // Slot can have many Appointments
                .HasForeignKey(a => a.SlotID)  // Foreign key is SlotID
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // If Appointment references Doctor through Slot
            modelBuilder.Entity<Slot>()
                .HasOne<Doctor>()  // Slot has one Doctor
                .WithMany()       // Doctor can have many Slots
                .HasForeignKey(s => s.DoctorID)  // Foreign key is DoctorID
                .OnDelete(DeleteBehavior.Restrict);

            // If you have DoctorSlot entity, configure it too
            modelBuilder.Entity<DoctorSlot>()
                .HasOne<Doctor>()  // DoctorSlot has one Doctor
                .WithMany()        // Doctor can have many DoctorSlots
                .HasForeignKey(ds => ds.DoctorID)  // Adjust property name as needed
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Patient relationship
            modelBuilder.Entity<Appointment>()
                .HasOne<Patient>()  // Appointment has one Patient
                .WithMany()         // Patient can have many Appointments
                .HasForeignKey(a => a.PatientID)  // Adjust property name as needed
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
