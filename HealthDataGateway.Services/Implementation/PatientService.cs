using HealthDataGateway.Data;
using HealthDataGateway.Data.Models;
using HealthDataGateway.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetPatientsByHospitalAsync(int hospitalId)
        {
            return await _context.Patients
                .Include(p => p.Diagnoses)
                .Where(p => p.HospitalId == hospitalId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int patientId)
        {
            return await _context.Patients
                .Include(p => p.Diagnoses)
                .Include(p => p.Hospital)
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            patient.CreatedAt = DateTime.Now;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            _context.ActivityLogs.Add(new ActivityLog
            {
                EntityName = "Patient",
                EntityId = patient.PatientId,
                Action = "CREATED",
                PerformedBy = "HOSPITAL_STAFF",
                LoggedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<Diagnosis> AddDiagnosisAsync(Diagnosis diagnosis)
        {
            diagnosis.DiagnosedAt = DateTime.Now;
            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();
            return diagnosis;
        }
    }
}