namespace StudentTaskManager;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        var status = IsCompleted ? "Completed" : "Pending";
        return $"ID: {Id} | Title: {Title} | Due: {DueDate:yyyy-MM-dd} | Status: {status}\nDescription: {Description}";
    }
}
