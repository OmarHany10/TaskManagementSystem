# Task Management System API  

## Overview  

The **Task Management System API** is designed to manage tasks, users, projects, and their interactions within an organization. The API supports features such as user authentication, task management, project tracking, role-based access control (RBAC), comment management, and activity logging.  

---

## Features  

- **User Authentication**: Secure authentication using JWT, with role-based access control for Admins, Project Managers, and Users.  
- **Task Management**: Comprehensive management of tasks, including creating, updating, deleting, assigning, and tracking.  
- **Comment Management**: Manage and track comments on tasks for effective collaboration.  
- **Activity Logging**: Track and log user and task activities for audit and accountability.  
- **Project Management**: Handle projects, including assigning users, tracking tasks, and managing project details.  
- **Role Management**: Admins can assign specific roles to users to control access.  
- **CRUD Operations**:  
  - Full support for **Create**, **Read**, **Update**, and **Delete** operations for tasks, users, projects, and comments.  
  - Simplifies resource management and promotes a standardized API design.  
- **Overdue Task Tracking**: Identify overdue tasks for better time management.  
- **Layered Architecture**: Implements a clear separation of concerns using Services, Repositories, and DTOs.  
- **Repository Pattern**: Simplifies database operations by abstracting query logic into reusable, maintainable classes.  
- **Unit of Work**: Ensures consistency by grouping multiple database operations into a single transaction.  
- **Scalability**: Designed with scalability in mind, enabling easy addition of features and expansion of functionality.  

---

## Technologies  

- **Backend**: .NET Core (C#)  
- **Authentication**: JWT with ASP.NET Core Identity  
- **Database**: SQL Server  
- **Architecture**: Layered Architecture using Repository Pattern and Unit of Work  

---

## Benefits of CRUD Operations  

- **Create**: Allows adding new resources (tasks, users, projects, etc.) with simple and intuitive endpoints.  
- **Read**: Provides efficient retrieval of data, including filtering, pagination, and searching.  
- **Update**: Enables modification of existing resources, ensuring seamless updates to tasks, users, or projects.  
- **Delete**: Supports secure and efficient removal of unnecessary or outdated resources.  
- **Consistency**: CRUD operations maintain uniformity across the API, simplifying client-side development.  

---

## Benefits of Repository Pattern  

- Encapsulates database logic, making the code cleaner and easier to maintain.  
- Promotes testability by allowing mocking of database operations during unit testing.  
- Provides a single, unified interface for database queries, reducing code duplication.  

---

## Benefits of Unit of Work  

- Groups multiple database operations within a single transaction to maintain data consistency.  
- Reduces the risk of partial updates when performing complex operations.  
- Improves performance by reducing the number of database calls.  

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
