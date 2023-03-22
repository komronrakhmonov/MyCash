using MyCash.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace MyCash.Domain.Entities;

public class User : Auditable
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required,MinLength(8)]
    public string Password { get; set; }
}
