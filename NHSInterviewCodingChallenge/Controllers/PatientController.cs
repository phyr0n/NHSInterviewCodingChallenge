using Microsoft.AspNetCore.Mvc;
using NHSInterviewCodingChallenge.Models;
using NHSInterviewCodingChallenge.Services;

namespace NHSInterviewCodingChallenge.Controllers;

/// <summary>
/// Responsible for handling incoming HTTP requests for services related to patients.
/// </summary>
/// <param name="patientService">The service responsible for handling business logic for patients.</param>
[ApiController]
[Route("[controller]")]
public sealed class PatientController(IPatientService patientService) : ControllerBase
{

	/// <summary>
	/// Given an ID, gets the patient.
	/// </summary>
	/// <param name="id">The requested ID.</param>
	/// <returns>If a patient was found in the database, this method will return a <c>200 OK</c> response; otherwise, a <c>404 NOT FOUND</c> will be returned.</returns>
	[HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Description = "Patient found.", StatusCode = 200, Type = typeof(Patient))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "Patient not found.")]
    public async Task<ActionResult<Patient>> Get(int id)
    {
        Patient? patient = await patientService.GetById(id);

        if (patient is null)
        {
            return NotFound(patient);
        }

        return Ok(patient);
    }
}