
using MyCash.Domain.Commons;
using MyCash.Domain.Enums;

namespace MyCash.Domain.Entities
{
    public class Category : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryType Type { get; set; }
    }
}
