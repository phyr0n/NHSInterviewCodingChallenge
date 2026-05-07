using Microsoft.EntityFrameworkCore;
using NHSInterviewCodingChallenge.Data;
using NHSInterviewCodingChallenge.Models;

namespace NHSInterviewCodingChallenge.Services;

/// <summary>
/// Represents a set of functionality needed to implement the service to act upon NHS patient data.
/// </summary>
public interface IPatientService
{
    /// <summary>
    /// Given an ID, searches for a patient in the database.
    /// </summary>
    /// <param name="id">The patient's ID to search for.</param>
    /// <returns>An instance of <see cref="Patient"/> if a patient exists in the database; otherwise, <see langword="null"/>.</returns>
    Task<Patient?> GetById(int id);
}

/// <summary>
/// Responsible for contacting the NHS patient database.
/// </summary>
/// <param name="dbContext">A connection to the NHS database.</param>
public sealed class PatientService(NHSAppDbContext dbContext) : IPatientService
{
    private readonly NHSAppDbContext _dbContext = dbContext;

    public async Task<Patient?> GetById(int id)
    {
        return await _dbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
    }
}