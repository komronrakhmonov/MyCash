
using MyCash.Domain.Enums;

namespace MyCash.Servise.DTOs;

public class CategoryCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryType Type { get; set; }
}
