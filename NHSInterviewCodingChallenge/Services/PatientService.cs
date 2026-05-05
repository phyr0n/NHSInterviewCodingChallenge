using Microsoft.EntityFrameworkCore;
using NHSInterviewCodingChallenge.Data;
using NHSInterviewCodingChallenge.Models;

namespace NHSInterviewCodingChallenge.Services;

public interface IPatientService
{
    Task<Patient?> GetById(int id);
}

public sealed class PatientService(NHSAppDbContext dbContext) : IPatientService
{
    private readonly NHSAppDbContext _dbContext = dbContext;

    public async Task<Patient?> GetById(int id)
    {
        return await _dbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
    }
}