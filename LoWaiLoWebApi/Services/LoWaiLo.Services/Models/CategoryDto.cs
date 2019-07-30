namespace LoWaiLo.Services.Models
{
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class CategoryDto : IMapTo<Category>, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }
    }
}
