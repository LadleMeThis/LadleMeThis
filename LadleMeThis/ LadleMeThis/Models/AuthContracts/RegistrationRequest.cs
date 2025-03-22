using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Models.AuthContracts
{
    public record RegistrationRequest(
        [Required] string FirstName,
        [Required] string LastName,
        [Required] string Email,
        [Required] string Username,
        [Required] string Password);
}
