using Microsoft.EntityFrameworkCore;
using NHSInterviewCodingChallenge.Data;
using NHSInterviewCodingChallenge.Models;
using NHSInterviewCodingChallenge.Services;

namespace NHSInterviewCodingChallenge.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public sealed class PatientServiceTests
{
	[Test]
	public async Task GetPatientById_ShouldBeNotNull_WhenPatientIsFound()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<NHSAppDbContext>()
			.UseInMemoryDatabase("MockedPatientsDb")
			.Options;
		NHSAppDbContext context = new(options);
		context.Populate();

		PatientService patientService = new(context);

		// Act
		Patient? patient = await patientService.GetById(1);

		// Assert
		Assert.That(patient, Is.Not.Null);
	}

	[Test]
	public async Task GetPatientById_ShouldBeNull_WhenPatientIsNotFound()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<NHSAppDbContext>()
			.UseInMemoryDatabase("MockedPatientsDb")
			.Options;
		NHSAppDbContext context = new(options);
		context.Populate();

		PatientService patientService = new(context);

		// Act
		Patient? patient = await patientService.GetById(-1);

		// Assert
		Assert.That(patient, Is.Null);
	}
}