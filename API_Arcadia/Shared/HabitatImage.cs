using System.Globalization;

namespace BlazorWasm.Shared
{
    public class HabitatImage
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string MiniSlug { get; set; } = string.Empty;
        public int IdHabitat { get; set; }
    }
}
