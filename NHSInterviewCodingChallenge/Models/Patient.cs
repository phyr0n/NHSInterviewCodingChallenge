namespace NHSInterviewCodingChallenge.Models;

/// <summary>
/// Represents details about an individual NHS patient.
/// </summary>
/// <remarks>Assuming in this scenario the patient can't modify their details through this API, otherwise this should be a class rather than a record.</remarks>
public sealed record Patient
{
    /// <summary>
    /// The unique identifier representing this patient.
    /// </summary>
    /// <remarks>There's a debate to be had about whether this should be an integer or a GUID.</remarks>
    public int Id { get; set; }
    /// <summary>
    /// The patient's unique NHS number.
    /// </summary>
    public string NHSNumber { get; set; } = string.Empty;
    /// <summary>
    /// The first name of this patient.
    /// </summary>
    public string Forename { get; set; } = string.Empty;
    /// <summary>
    /// The surname of this patient.
    /// </summary>
    public string Surname { get; set; } = string.Empty;
    /// <summary>
    /// The patient's date of birth (DOB).
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    /// <summary>
    /// The GP practice that this patient is registered with.
    /// </summary>
    public string GPPractice { get; set; } = string.Empty;
}