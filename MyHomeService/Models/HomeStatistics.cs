namespace MyHomeService.Models
{
    public class HomeStatistics
    {
        public int ActiveTasksCount {  get; set; }
        public int CompletedTasksCount { get; set; }
        public int TotalTasksCount => ActiveTasksCount + CompletedTasksCount;
        public int CompletionTasksPercentage => (TotalTasksCount > 0) ? (int)Math.Round((double)CompletedTasksCount / TotalTasksCount * 100) : 0;
    }
}
