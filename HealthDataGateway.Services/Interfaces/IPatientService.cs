using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatientsByHospitalAsync(int hospitalId);
        Task<Patient?> GetPatientByIdAsync(int patientId);
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<Diagnosis> AddDiagnosisAsync(Diagnosis diagnosis);
    }
}