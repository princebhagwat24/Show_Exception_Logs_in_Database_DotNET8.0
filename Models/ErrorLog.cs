namespace ExceptionLogsTask.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime LogDate { get; set; }
    }
}
