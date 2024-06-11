$(document).ready(function () {
    // Event delegation for dynamically added elements
    $(document).on('click', '.list-link', function () {
        var listId = $(this).data('listid');
        if (listId === @Model.AllListId || listId === @Model.TodayListId || listId === @Model.TomorrowListId) {
            // Handle All, Today, and Tomorrow lists separately
            loadSpecialListTasks(listId);
        } else {
            loadListTasks(listId);
        }
        // Set the selectedListId variable
        $('#selectedListId').val(listId);
    });

    // Event handler for delete-list button
    $(document).on('click', '.delete-list', function () {
        var listId = $(this).data('listid');
        deleteList(listId);
    });
});

function loadListTasks(listId) {
    $.ajax({
        url: '/TimeManagement/ListTasks/' + listId,
        type: 'GET',
        success: function (data) {
            $('#listTasksContainer').html(data);
        }
    });
}

function loadTaskDetails(taskId) {
    $.ajax({
        url: '/TimeManagement/TaskDetails/' + taskId,
        type: 'GET',
        success: function (data) {
            $('#taskDetailsContainer').html(data);
        }
    });
}

function deleteList(listId) {
    // Send an AJAX request to delete the list
    $.ajax({
        url: '/TimeManagement/DeleteList/' + listId,
        type: 'POST',
        success: function (data) {
            // Refresh the page or update the UI as needed
            location.reload(); // For example, reload the page
        }
    });
}

function deleteTask(taskId) {
    // Send an AJAX request to delete the task
    $.ajax({
        url: '/TimeManagement/DeleteTask/' + taskId,
        type: 'POST',
        success: function (data) {
            // Refresh the page or update the UI as needed
            // For example, reload tasks for the selected list
            var listId = $('#selectedListId').val();
            loadListTasks(listId);
        }
    });
}

function loadSpecialListTasks(listId) {
    switch (listId) {
        case @Model.AllListId:
            loadAllTasks();
            break;
        case @Model.TodayListId:
            loadTodayTasks();
            break;
        case @Model.TomorrowListId:
            loadTomorrowTasks();
            break;
    }
}

function loadAllTasks() {
    $.ajax({
        url: '/TimeManagement/AllTasks',
        type: 'GET',
        success: function (data) {
            $('#listTasksContainer').html(data);
        }
    });
}

function loadTodayTasks() {
    $.ajax({
        url: '/TimeManagement/TodayTasks',
        type: 'GET',
        success: function (data) {
            $('#listTasksContainer').html(data);
        }
    });
}

function loadTomorrowTasks() {
    $.ajax({
        url: '/TimeManagement/TomorrowTasks',
        type: 'GET',
        success: function (data) {
            $('#listTasksContainer').html(data);
        }
    });
}

function showCreateListForm() {
    $('#createListForm').toggle();
}

function showCreateTaskForm() {
    $('#createTaskForm').toggle();
}

$('#createListForm form').submit(function (event) {
    event.preventDefault();
    createList();
});

$('#createTaskForm form').submit(function (event) {
    event.preventDefault();
    createTask();
});

function createList() {
    var listTitle = $('#createListForm input[name="ListTitle"]').val();
    $.ajax({
        url: '/TimeManagement/CreateList',
        type: 'POST',
        data: { ListTitle: listTitle },
        success: function (data) {
            // Refresh the page or update the list dynamically
            location.reload(); // Refresh the page
        }
    });
}

function showEditListForm(listId) {
    // Make an AJAX request to fetch the details of the list with the specified ID
    $.ajax({
        url: '/TimeManagement/GetListDetails/' + listId,
        type: 'GET',
        success: function (data) {
            // Populate the form fields with the fetched data
            $('#editListForm input[name="ListId"]').val(data.listId);
            $('#editListForm input[name="ListTitle"]').val(data.listTitle);
            $('#editListForm input[name="ListCategory"]').val(data.listCategory);

            // Display the edit list form
            $('#editListForm').show();
        },
        error: function (xhr, status, error) {
            console.error('Error fetching list details:', error);
        }
    });
}

function showEditTaskForm(taskId) {
    // Make an AJAX request to fetch the details of the task with the specified ID
    $.ajax({
        url: '/TimeManagement/GetTaskDetails/' + taskId,
        type: 'GET',
        success: function (data) {
            // Populate the form fields with the fetched data
            $('#editTaskForm input[name="TaskId"]').val(data.taskId);
            $('#editTaskForm input[name="TaskTitle"]').val(data.taskTitle);
            $('#editTaskForm select[name="TaskPriority"]').val(data.taskPriority);
            // Populate other fields similarly

            // Display the edit task form
            $('#editTaskForm').show();
        },
        error: function (xhr, status, error) {
            console.error('Error fetching task details:', error);
        }
    });
}

function editList() {
    var listId = $('#editListForm input[name="ListId"]').val();
    var updatedList = {
        ListId: listId,
        ListTitle: $('#editListForm input[name="ListTitle"]').val(),
        ListCategory: $('#editListForm input[name="ListCategory"]').val()
        // Add other properties as needed
    };

    $.ajax({
        url: '/TimeManagement/EditList',
        type: 'POST',
        data: updatedList,
        success: function (data) {
            // Hide the edit form and perform any necessary actions
            $('#editListForm').hide();
            // For example, you may want to refresh the page or update the UI
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error editing list:', error);
        }
    });
}

function editTask() {
    var taskId = $('#editTaskForm input[name="TaskId"]').val();
    var updatedTask = {
        TaskId: taskId,
        TaskTitle: $('#editTaskForm input[name="TaskTitle"]').val(),
        TaskPriority: $('#editTaskForm select[name="TaskPriority"]').val()
        // Add other properties as needed
    };

    $.ajax({
        url: '/TimeManagement/EditTask',
        type: 'POST',
        data: updatedTask,
        success: function (data) {
            // Hide the edit form and perform any necessary actions
            $('#editTaskForm').hide();
            // For example, you may want to refresh the page or update the UI
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error editing task:', error);
        }
    });
}

function createTask() {
    var taskTitle = $('#createTaskForm input[name="TaskTitle"]').val();
    var taskPriority = $('#createTaskForm select[name="TaskPriority"]').val();
    var taskTag = $('#createTaskForm input[name="TaskTag"]').val();
    var taskDate = $('#createTaskForm input[name="TaskDate"]').val();
    var taskStartTime = $('#createTaskForm input[name="TaskStartTime"]').val();
    var taskEndTime = $('#createTaskForm input[name="TaskEndTime"]').val();
    var taskDifficulty = $('#createTaskForm select[name="TaskDifficulty"]').val();

    var listId = $('#selectedListId').val();

    $.ajax({
        url: '/TimeManagement/CreateTask',
        type: 'POST',
        data: {
            ListId: listId,
            TaskTitle: taskTitle,
            TaskPriority: taskPriority,
            TaskTag: taskTag,
            TaskDate: taskDate,
            TaskStartTime: taskStartTime,
            TaskEndTime: taskEndTime,
            TaskDifficulty: taskDifficulty
        },
        success: function (data) {
            // Refresh the page or update the tasks dynamically
            loadListTasks(listId); // Reload tasks for the selected list
        }
    });
}

function updateTaskStatus(taskId, isChecked) {
    $.ajax({
        url: '/TimeManagement/UpdateTaskStatus/' + taskId,
        type: 'POST',
        data: { id: taskId, isChecked: isChecked }, // Ensure that the checkbox value is correctly passed
        success: function (data) {
            // Optionally, update the UI to reflect the new task status
            console.log('Task status updated successfully');
        },
        error: function (xhr, status, error) {
            console.error('Error updating task status:', error);
        }
    });
}
