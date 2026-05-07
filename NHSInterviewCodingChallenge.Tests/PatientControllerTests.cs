using Microsoft.AspNetCore.Mvc;
using Moq;
using NHSInterviewCodingChallenge.Controllers;
using NHSInterviewCodingChallenge.Models;
using NHSInterviewCodingChallenge.Services;

namespace NHSInterviewCodingChallenge.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public sealed class PatientControllerTests
{
	[Test]
	public async Task GetPatientById_ShouldReturn200Ok_WhenPatientIsFound()
	{
		// Arrange
		// Create the mock service. For a moment, I'm going to pretend that the mock database we create in the web API is an actual database.
		Mock<IPatientService> service = new();
		Patient expectedPatient = new() { Id = 42, Forename = "Testy", Surname = "McTestFace", DateOfBirth = new DateTime(1983, 8, 23, 04, 12, 06), GPPractice = "GP Practice #1", NHSNumber = "5819328431" };
		service
			.Setup(service => service.GetById(It.IsAny<int>()))
			.ReturnsAsync(expectedPatient);

		PatientController controller = new(service.Object);

		// Act
		ActionResult<Patient> actionResult = await controller.Get(42);

		// Assert
		OkObjectResult? result = actionResult.Result as OkObjectResult;
		Patient? patient = result?.Value as Patient;

		Assert.Multiple(() =>
		{
			Assert.That(result, Is.TypeOf<OkObjectResult>());
			Assert.That(actionResult, Is.Not.Null);
			Assert.That(patient, Is.EqualTo(expectedPatient));
		});
	}

	[Test]
	public async Task GetPatientById_ShouldReturn404NotFound_WhenPatientIsNotFound()
	{
		// Arrange
		Mock<IPatientService> service = new();
		service
			.Setup(service => service.GetById(It.IsAny<int>()))
			.ReturnsAsync((Patient)null!);
		PatientController controller = new(service.Object);

		// Act
		ActionResult<Patient> actionResult = await controller.Get(42);

		// Assert
		NotFoundObjectResult? result = actionResult.Result as NotFoundObjectResult;
		Patient? patient = result?.Value as Patient;

		Assert.Multiple(() =>
		{
			Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
			Assert.That(actionResult, Is.Not.Null);
			Assert.That(patient, Is.Null);
		});
	}
}