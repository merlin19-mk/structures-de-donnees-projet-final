namespace StudentTaskManager;

public class TaskManager
{
    private readonly List<TaskItem> allTasks = new();
    private Queue<TaskItem> pendingTasks = new();
    private int nextId = 1;

    public TaskItem AddTask(string title, string description, DateTime dueDate)
    {
        var task = new TaskItem
        {
            Id = nextId++,
            Title = title,
            Description = description,
            DueDate = dueDate,
            IsCompleted = false
        };

        allTasks.Add(task);
        return task;
    }

    public List<TaskItem> ViewAllTasks()
    {
        var result = new List<TaskItem>();

        for (var i = 0; i < allTasks.Count; i++)
        {
            result.Add(allTasks[i]);
        }

        return result;
    }

    public TaskItem? SearchTaskById(int id)
    {
        for (var i = 0; i < allTasks.Count; i++)
        {
            if (allTasks[i].Id == id)
            {
                return allTasks[i];
            }
        }

        return null;
    }

    public List<TaskItem> SearchTaskByTitle(string title)
    {
        var result = new List<TaskItem>();

        for (var i = 0; i < allTasks.Count; i++)
        {
            var task = allTasks[i];
            if (task.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(task);
            }
        }

        return result;
    }

    public bool DeleteTask(int id)
    {
        var index = -1;

        for (var i = 0; i < allTasks.Count; i++)
        {
            if (allTasks[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return false;
        }

        allTasks.RemoveAt(index);

        var cleanedQueue = new Queue<TaskItem>();
        while (pendingTasks.Count > 0)
        {
            var queuedTask = pendingTasks.Dequeue();
            if (queuedTask.Id != id)
            {
                cleanedQueue.Enqueue(queuedTask);
            }
        }

        pendingTasks = cleanedQueue;
        return true;
    }

    public bool MarkTaskCompleted(int id)
    {
        TaskItem? task = null;

        for (var i = 0; i < allTasks.Count; i++)
        {
            if (allTasks[i].Id == id)
            {
                task = allTasks[i];
                break;
            }
        }

        if (task is null)
        {
            return false;
        }

        task.IsCompleted = true;

        var cleanedQueue = new Queue<TaskItem>();
        while (pendingTasks.Count > 0)
        {
            var queuedTask = pendingTasks.Dequeue();
            if (queuedTask.Id != id)
            {
                cleanedQueue.Enqueue(queuedTask);
            }
        }

        pendingTasks = cleanedQueue;
        return true;
    }

    public bool AddTaskToQueue(int id)
    {
        var task = SearchTaskById(id);
        if (task is null || task.IsCompleted)
        {
            return false;
        }

        foreach (var queuedTask in pendingTasks)
        {
            if (queuedTask.Id == id)
            {
                return false;
            }
        }

        pendingTasks.Enqueue(task);
        return true;
    }

    public TaskItem? ProcessNextTask()
    {
        while (pendingTasks.Count > 0)
        {
            var task = pendingTasks.Dequeue();
            if (!task.IsCompleted)
            {
                return task;
            }
        }

        return null;
    }

    public List<TaskItem> ViewPendingQueue()
    {
        var result = new List<TaskItem>();

        foreach (var task in pendingTasks)
        {
            if (!task.IsCompleted)
            {
                result.Add(task);
            }
        }

        return result;
    }

    public List<TaskItem> ViewPendingTasks()
    {
        var result = new List<TaskItem>();

        for (var i = 0; i < allTasks.Count; i++)
        {
            if (!allTasks[i].IsCompleted)
            {
                result.Add(allTasks[i]);
            }
        }

        return result;
    }
}
