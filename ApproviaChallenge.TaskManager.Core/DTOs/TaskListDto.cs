
namespace ApproviaChallenge.TaskManager.Core.DTOs
{
    public class TaskListDto : CreateTaskDto
    {
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysOverDue { get; set; }
        public int DaysLate { get; set; }
    }
}
