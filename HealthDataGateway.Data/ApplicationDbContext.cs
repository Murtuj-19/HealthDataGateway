using System;
using HealthDataGateway.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HealthDataGateway.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<TransferRequest> TransferRequests { get; set; }
        public DbSet<ConnectorRequest> ConnectorRequests { get; set; }
        public DbSet<IncomingTransferRequest> IncomingTransferRequests { get; set; }
        public DbSet<Acknowledgement> Acknowledgements { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        // Read-only view mapping
        public DbSet<GatewayDashboard> GatewayDashboard { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hospital
            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.HasKey(e => e.HospitalId);
                entity.Property(e => e.HospitalName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
                entity.ToTable("Hospital");
            });

            // Patient
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);
                entity.Property(e => e.LocalPatientId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Gender).HasMaxLength(1);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIME()");

                entity.HasIndex(e => new { e.HospitalId, e.LocalPatientId }).IsUnique();

                entity.HasOne(e => e.Hospital)
                    .WithMany(h => h.Patients)
                    .HasForeignKey(e => e.HospitalId);

                entity.ToTable("Patient");
            });

            // Diagnosis
            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.HasKey(e => e.DiagnosisId);
                entity.Property(e => e.DiagnosisText).IsRequired().HasMaxLength(500);
                entity.Property(e => e.DiagnosedAt).HasDefaultValueSql("SYSDATETIME()");

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId);

                entity.ToTable("Diagnosis");
            });

            // TransferRequest
            modelBuilder.Entity<TransferRequest>(entity =>
            {
                entity.HasKey(e => e.TransferRequestId);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIME()");

                entity.HasOne(e => e.SourceHospital)
                    .WithMany(h => h.SourceTransferRequests)
                    .HasForeignKey(e => e.SourceHospitalId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.TargetHospital)
                    .WithMany(h => h.TargetTransferRequests)
                    .HasForeignKey(e => e.TargetHospitalId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.TransferRequests)
                    .HasForeignKey(e => e.PatientId);

                entity.HasIndex(e => e.Status);

                entity.ToTable("TransferRequest");
            });

            // ConnectorRequest
            modelBuilder.Entity<ConnectorRequest>(entity =>
            {
                entity.HasKey(e => e.ConnectorRequestId);
                entity.Property(e => e.EncryptedPayload).IsRequired();
                entity.Property(e => e.EncryptionType).HasMaxLength(50).HasDefaultValue("AES-256");
                entity.Property(e => e.AuthType).HasMaxLength(50).HasDefaultValue("OAuth2");
                entity.Property(e => e.IsFHIRCompliant).HasDefaultValue(true);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIME()");

                entity.HasOne(e => e.TransferRequest)
                    .WithMany(t => t.ConnectorRequests)
                    .HasForeignKey(e => e.TransferRequestId);

                entity.HasIndex(e => e.Status);

                entity.ToTable("ConnectorRequest");
            });

            // IncomingTransferRequest
            modelBuilder.Entity<IncomingTransferRequest>(entity =>
            {
                entity.HasKey(e => e.IncomingRequestId);
                entity.Property(e => e.IncomingStatus).IsRequired().HasMaxLength(20);
                entity.Property(e => e.ReceivedAt).HasDefaultValueSql("SYSDATETIME()");

                entity.HasOne(e => e.ConnectorRequest)
                    .WithMany(c => c.IncomingTransferRequests)
                    .HasForeignKey(e => e.ConnectorRequestId);

                entity.HasOne(e => e.SourceHospital)
                    .WithMany()
                    .HasForeignKey(e => e.SourceHospitalId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.TargetHospital)
                    .WithMany()
                    .HasForeignKey(e => e.TargetHospitalId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(e => e.IncomingStatus);

                entity.ToTable("IncomingTransferRequest");
            });

            // Acknowledgement
            modelBuilder.Entity<Acknowledgement>(entity =>
            {
                entity.HasKey(e => e.AcknowledgementId);
                entity.Property(e => e.Decision).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Remarks).HasMaxLength(300);
                entity.Property(e => e.RespondedAt).HasDefaultValueSql("SYSDATETIME()");

                entity.HasOne(e => e.IncomingTransferRequest)
                    .WithMany(i => i.Acknowledgements)
                    .HasForeignKey(e => e.IncomingRequestId);

                entity.ToTable("Acknowledgement");
            });

            // ActivityLog
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasKey(e => e.LogId);
                entity.Property(e => e.EntityName).HasMaxLength(50);
                entity.Property(e => e.Action).HasMaxLength(50);
                entity.Property(e => e.PerformedBy).HasMaxLength(100);
                entity.Property(e => e.LoggedAt).HasDefaultValueSql("SYSDATETIME()");

                entity.ToTable("ActivityLog");
            });

            // Map view
            modelBuilder.Entity<GatewayDashboard>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_GatewayDashboard");
            });

            // Seed Data
            modelBuilder.Entity<Hospital>().HasData(
                new Hospital { HospitalId = 1, HospitalName = "City General Hospital", IsActive = true, CreatedAt = DateTime.Now },
                new Hospital { HospitalId = 2, HospitalName = "St. Mary's Medical Center", IsActive = true, CreatedAt = DateTime.Now },
                new Hospital { HospitalId = 3, HospitalName = "Regional Trauma Center", IsActive = true, CreatedAt = DateTime.Now }
            );
        }

        // Stored procedure wrappers
        public void CreateTransferRequestSp(int sourceHospitalId, int patientId, int targetHospitalId)
        {
            Database.ExecuteSqlRaw("EXEC sp_CreateTransferRequest @p0, @p1, @p2", sourceHospitalId, patientId, targetHospitalId);
        }

        public void PushToConnectorSp(int transferRequestId, byte[] encryptedPayload)
        {
            Database.ExecuteSqlRaw("EXEC sp_PushToConnector @p0, @p1", transferRequestId, encryptedPayload);
        }

        public void TargetDecisionSp(int incomingRequestId, string decision, string remarks)
        {
            Database.ExecuteSqlRaw("EXEC sp_TargetDecision @p0, @p1, @p2", incomingRequestId, decision, remarks);
        }
    }
}