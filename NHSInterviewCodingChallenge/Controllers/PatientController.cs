using Microsoft.AspNetCore.Mvc;
using NHSInterviewCodingChallenge.Models;
using NHSInterviewCodingChallenge.Services;

namespace NHSInterviewCodingChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class PatientController(IPatientService patientService) : ControllerBase
{
    private readonly IPatientService _patientService = patientService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Description = "Patient found.", StatusCode = 200, Type = typeof(Patient))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "Patient not found.")]
    public async Task<ActionResult<Patient>> Get(int id)
    {
        Patient? patient = await _patientService.GetById(id);

        if (patient is null)
        {
            return Problem($"No patient could be found in the database with ID = {id}. Please try again.", HttpContext.Request.Path, StatusCodes.Status404NotFound, "Patient Not Found", "errors/PatientNotFound");
        }

        return Ok(patient);
    }
}