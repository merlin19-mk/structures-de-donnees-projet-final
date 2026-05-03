using StudentTaskManager;

var taskManager = new TaskManager();
var keepRunning = true;

while (keepRunning)
{
	ShowMenu();
	Console.Write("Choose an option: ");
	var input = Console.ReadLine();

	Console.WriteLine();

	switch (input)
	{
		case "1":
			AddTask(taskManager);
			break;
		case "2":
			ViewAllTasks(taskManager);
			break;
		case "3":
			SearchTaskById(taskManager);
			break;
		case "4":
			SearchTaskByTitle(taskManager);
			break;
		case "5":
			DeleteTask(taskManager);
			break;
		case "6":
			MarkTaskCompleted(taskManager);
			break;
		case "7":
			AddTaskToQueue(taskManager);
			break;
		case "8":
			ProcessNextPendingTask(taskManager);
			break;
		case "9":
			ViewPendingQueue(taskManager);
			break;
		case "10":
			ViewPendingTasks(taskManager);
			break;
		case "11":
			keepRunning = false;
			Console.WriteLine("Goodbye!");
			break;
		default:
			Console.WriteLine("Invalid option. Please choose a number from the menu.");
			break;
	}

	Console.WriteLine();
}

static void ShowMenu()
{
	Console.WriteLine("=== Student Task Manager ===");
	Console.WriteLine("1. Add Task");
	Console.WriteLine("2. View All Tasks");
	Console.WriteLine("3. Search Task by ID");
	Console.WriteLine("4. Search Task by Title");
	Console.WriteLine("5. Delete Task");
	Console.WriteLine("6. Mark Task as Completed");
	Console.WriteLine("7. Add Task to Pending Queue");
	Console.WriteLine("8. Process Next Pending Task");
	Console.WriteLine("9. View Pending Queue");
	Console.WriteLine("10. View Pending Tasks");
	Console.WriteLine("11. Exit");
}

static void AddTask(TaskManager manager)
{
	Console.Write("Title: ");
	var title = Console.ReadLine() ?? string.Empty;

	Console.Write("Description: ");
	var description = Console.ReadLine() ?? string.Empty;

	var dueDate = ReadDate("Due date (yyyy-MM-dd): ");

	var task = manager.AddTask(title, description, dueDate);
	Console.WriteLine($"Task added with ID {task.Id}.");
}

static void ViewAllTasks(TaskManager manager)
{
	var tasks = manager.ViewAllTasks();
	PrintTaskList(tasks, "No tasks found.");
}

static void SearchTaskById(TaskManager manager)
{
	var id = ReadInt("Enter task ID: ");
	var task = manager.SearchTaskById(id);

	if (task is null)
	{
		Console.WriteLine("Task not found.");
		return;
	}

	Console.WriteLine(task);
}

static void SearchTaskByTitle(TaskManager manager)
{
	Console.Write("Enter title keyword: ");
	var keyword = Console.ReadLine() ?? string.Empty;

	var tasks = manager.SearchTaskByTitle(keyword);
	PrintTaskList(tasks, "No matching tasks found.");
}

static void DeleteTask(TaskManager manager)
{
	var id = ReadInt("Enter task ID to delete: ");
	var deleted = manager.DeleteTask(id);
	Console.WriteLine(deleted ? "Task deleted." : "Task not found.");
}

static void MarkTaskCompleted(TaskManager manager)
{
	var id = ReadInt("Enter task ID to mark as completed: ");
	var updated = manager.MarkTaskCompleted(id);
	Console.WriteLine(updated ? "Task marked as completed." : "Task not found.");
}

static void AddTaskToQueue(TaskManager manager)
{
	var id = ReadInt("Enter task ID to add to queue: ");
	var added = manager.AddTaskToQueue(id);

	if (added)
	{
		Console.WriteLine("Task added to pending queue.");
	}
	else
	{
		Console.WriteLine("Unable to queue task. It may not exist, may already be queued, or may be completed.");
	}
}

static void ProcessNextPendingTask(TaskManager manager)
{
	var task = manager.ProcessNextTask();

	if (task is null)
	{
		Console.WriteLine("No pending tasks in queue.");
		return;
	}

	Console.WriteLine("Processing next pending task:");
	Console.WriteLine(task);
}

static void ViewPendingQueue(TaskManager manager)
{
	var tasks = manager.ViewPendingQueue();
	PrintTaskList(tasks, "Pending queue is empty.");
}

static void ViewPendingTasks(TaskManager manager)
{
	var tasks = manager.ViewPendingTasks();
	PrintTaskList(tasks, "No pending tasks found.");
}

static int ReadInt(string prompt)
{
	while (true)
	{
		Console.Write(prompt);
		var input = Console.ReadLine();

		if (int.TryParse(input, out var value))
		{
			return value;
		}

		Console.WriteLine("Please enter a valid number.");
	}
}

static DateTime ReadDate(string prompt)
{
	while (true)
	{
		Console.Write(prompt);
		var input = Console.ReadLine();

		if (DateTime.TryParse(input, out var date))
		{
			return date.Date;
		}

		Console.WriteLine("Please enter a valid date in format yyyy-MM-dd.");
	}
}

static void PrintTaskList(IEnumerable<TaskItem> tasks, string emptyMessage)
{
	var any = false;

	foreach (var task in tasks)
	{
		Console.WriteLine(task);
		Console.WriteLine();
		any = true;
	}

	if (!any)
	{
		Console.WriteLine(emptyMessage);
	}
}
