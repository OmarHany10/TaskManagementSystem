# Task Management System API  

## Overview  

The **Task Management System API** is designed to manage tasks, users, projects, and their interactions within an organization. The API supports features such as user authentication, task management, project tracking, role-based access control (RBAC), comment management, and activity logging.  

---

## Features  

- User authentication with JWT and role-based access control (Admin, Project Manager, and User).  
- Manage tasks, including creating, updating, deleting, and assigning them to users.  
- Manage and track comments on tasks.  
- Log and view activity logs for users and tasks.  
- Manage projects, including assigning users and tracking project-specific tasks.  
- Full CRUD operations for users and projects (Admins only for some operations).  

---

## Technologies  

- **Backend**: .NET Core (C#)  
- **Authentication**: JWT with ASP.NET Core Identity  
- **Database**: SQL Server  
- **Architecture**: Layered Architecture with Services, Repositories, and DTOs  

---

## API Endpoints  

### Authentication  

- **POST /api/Authentication/Register**  
  Register a new user.  

- **POST /api/Authentication/Login**  
  Log in a user to retrieve a JWT token.  

- **POST /api/Authentication/AssignToRole**  
  Assign a role to a user (Admin only).  

### User Management  

- **GET /api/User**  
  Retrieve all users (Admin only).  

- **GET /api/User/{id}**  
  Retrieve a specific user by ID (Admin only).  

- **PUT /api/User**  
  Update user details (Admin only).  

- **DELETE /api/User**  
  Delete a user (Admin only).  

- **GET /api/User/MyActivity**  
  Retrieve the activity log of the currently authenticated user.  

- **GET /api/User/{id}/activity**  
  Retrieve the activity log for a specific user (Admin only).  

- **GET /api/User/MyTasks**  
  Retrieve tasks assigned to the currently authenticated user.  

- **GET /api/User/{userId}/tasks**  
  Retrieve tasks assigned to a specific user.  

### Project Management  

- **GET /api/Project**  
  Retrieve all projects.  

- **GET /api/Project/{id}**  
  Retrieve details of a specific project.  

- **POST /api/Project**  
  Create a new project.  

- **PUT /api/Project**  
  Update an existing project.  

- **DELETE /api/Project**  
  Delete a project.  

- **GET /api/Project/{projectId}/Users**  
  Retrieve all users assigned to a project.  

- **GET /api/Project/{projectId}/Tasks**  
  Retrieve all tasks associated with a project.  

### Task Management  

- **GET /api/Task**  
  Retrieve all tasks.  

- **GET /api/Task/{id}**  
  Retrieve details of a specific task.  

- **GET /api/Task/{taskId}/Comments**  
  Retrieve all comments for a specific task.  

- **GET /api/Task/Overdue**  
  Retrieve all overdue tasks.  

- **GET /api/Task/Upcoming**  
  Retrieve all upcoming tasks.  

- **GET /api/Task/Finished**  
  Retrieve all completed tasks.  

- **GET /api/Task/MyOverdue**  
  Retrieve overdue tasks assigned to the currently authenticated user.  

- **GET /api/Task/MyUpcoming**  
  Retrieve upcoming tasks assigned to the currently authenticated user.  

- **GET /api/Task/MyFinished**  
  Retrieve completed tasks assigned to the currently authenticated user.  

- **POST /api/Task**  
  Create a new task.  

- **PUT /api/Task**  
  Update an existing task.  

- **DELETE /api/Task**  
  Delete a task.  

### Comment Management  

- **POST /api/Task/{taskId}/Comments**  
  Add a comment to a task.  

- **PUT /api/Task/{taskId}/Comments/{commentId}**  
  Update a specific comment on a task.  

- **DELETE /api/Task/{taskId}/Comments/{commentId}**  
  Delete a specific comment on a task.  

### Activity Log  

- **GET /api/Activity/User/{userId}**  
  Retrieve the activity log for a specific user (Admin only).  

- **GET /api/Activity/Task/{taskId}**  
  Retrieve the activity log for a specific task.  
