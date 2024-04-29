namespace BlazorWasm.Shared
{
    public class OpeningHour
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeOnly? MorningOpening {  get; set; }
        public TimeOnly? MorningClosing { get; set; }
        public TimeOnly? AfternoonOpening { get; set; }
        public TimeOnly? AfternoonClosing { get; set; }
    }
}
