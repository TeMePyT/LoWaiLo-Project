namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
