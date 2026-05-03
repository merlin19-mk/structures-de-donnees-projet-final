# Student Task Manager

A simple C# console application for managing school tasks.

## Data Structures Used

1. `List<TaskItem>`
- Stores all tasks.
- Good for adding, looping, searching, and deleting tasks.

2. `Queue<TaskItem>`
- Stores pending tasks in FIFO order.
- Good for processing tasks one by one in the order they were queued.

## Features

1. Add task
2. View all tasks
3. Search task by ID
4. Search task by title
5. Delete task
6. Mark task as completed
7. Add task to pending queue
8. Process next pending task
9. View pending queue
10. View pending tasks
11. Exit

## Complexity Summary

### List<TaskItem>
- Add task: O(1)
- Search by ID: O(n)
- Search by title: O(n)
- Delete task: O(n)
- View all tasks: O(n)

### Queue<TaskItem>
- Enqueue: O(1)
- Dequeue: O(1)
- Check duplicates before enqueue: O(n)
- View queue: O(n)

## Run

```bash
dotnet run
```
