using CsvHelper;
using Microsoft.EntityFrameworkCore;
using NHSInterviewCodingChallenge.Models;
using System.Globalization;

namespace NHSInterviewCodingChallenge.Data;

/// <summary>
/// Represents a set of functionality needed to implement an NHS application database context.
/// </summary>
public interface INHSAppDbContext
{
    /// <summary>
    /// Patients in the database.
    /// </summary>
    DbSet<Patient> Patients { get; }
}

/// <summary>
/// Represents a connection to the NHS database.
/// </summary>
/// <param name="options">Options to be used for this database.</param>
public sealed class NHSAppDbContext(DbContextOptions<NHSAppDbContext> options) : DbContext(options), INHSAppDbContext
{
    /// <inheritdoc/>
    public DbSet<Patient> Patients => Set<Patient>();

    /// <summary>
    /// Populates the database with information. For this application, we will use some mocked data and add it to an in-memory data source.
    /// </summary>
    /// <remarks>
    /// In a real application, this class would probably not contain any population logic and would be added to a separate class.
    /// </remarks>
    public void Populate()
    {
        if (Patients.Any())
        {
            return;
        }

        using StreamReader streamReader = new("Data/MOCK_DATA.csv");
        using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);
        IEnumerable<Patient> patients = csvReader.GetRecords<Patient>();
        Patients.AddRange(patients);
        SaveChanges();
    }
}