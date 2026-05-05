using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using NHSInterviewCodingChallenge.Models;

namespace NHSInterviewCodingChallenge.Data;

/// <summary>
/// Represents a connection to the NHS database. Mocked for the purposes of this application.
/// </summary>
/// <param name="options">Options to be used for this database.</param>
public sealed class NHSAppDbContext(DbContextOptions<NHSAppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Patients in the database.
    /// </summary>
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