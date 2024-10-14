# Online Quiz Platform API

## Overview

The **Online Quiz Platform API** is a backend system built with **ASP.NET Core Web API** that allows users to create, manage, and participate in quizzes. The platform supports multiple user roles including **Admins**, **Instructors**, and **Students**. 

- **Admins** can manage users and moderate content across the platform.
- **Instructors** can create and manage quizzes and track student performance.
- **Students** can attempt quizzes and view their results.

## Features

- **Quiz Management**: Instructors can create, update, and delete quizzes.
- **Question Management**: Instructors can add, update, and remove questions in quizzes.
- **Answer Submission**: Students can submit answers to questions in quizzes.
- **Quiz Attempt Tracking**: Tracks student attempts for each quiz.
- **Student Profiles**: Students can view their previous quiz attempts and see their performance.
- **Admin Tools**: Admins can manage users, approve quizzes, and moderate the platform.
- **Instructor Tools**: Instructors can view student attempts and quiz results.

## Technologies

- **Backend**: ASP.NET Core Web API
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)

## Project Structure

```plaintext
OnlineQuizPlatform/
│
├── Controllers/           # API controllers (QuizController, QuestionController, AnswerController, AdminController)
├── Data/                  # Database-related files (Models, DbContext, Repositories)
├── DTOs/                  # Data Transfer Objects for API requests/responses
├── Mappings/              # Automapper profiles for DTOs and Entities
├── Repositories/          # Repositories to access data models (Quiz, Question, Answer, etc.)
├── Services/              # Business logic layer (AnswerService, QuizService, etc.)
└── Program.cs             # Entry point of the application

```
# Setup Instructions

## Prerequisites

- **.NET SDK 7.0 or higher**:  
  Make sure you have the latest .NET SDK installed. You can download it from [here](https://dotnet.microsoft.com/download). This SDK is required to build, run, and develop the Online Quiz Platform API.

- **SQL Server**:  
  The project uses **Microsoft SQL Server** as its database. If you don't have SQL Server installed, you can download it from the [official website](https://www.microsoft.com/en-us/sql-server/sql-server-downloads). Ensure that SQL Server is running on your machine, and you have an instance to connect to.

## Getting Started

### 1. Clone the repository:
Use the following commands to clone the repository to your local machine:

```bash
git clone https://github.com/yourusername/OnlineQuizPlatform.git
cd OnlineQuizPlatform
```
### 2.Set up the database:
Open the appsettings.json file located in the root of the project and configure the connection string for your SQL Server instance.
Replace YOUR_SERVER_NAME with your actual SQL Server instance name:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=OnlineQuizDb;Trusted_Connection=True;"
}
```
- **Server: The name of your SQL Server instance.**:
- **Database: The database name (you can change OnlineQuizDb if you want).**
- **Trusted_Connection: Set this to True if you're using Windows Authentication, or replace it with credentials if you're using SQL Server Authentication.**
#### Alternatively, for SQL Server authentication, you can use:
```bash
 "ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=OnlineQuizDb;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
}
```
### After configuring the connection string, run the following command to apply the migrations and create the database schema:
```bash
dotnet ef database update
```
- This command runs the migrations, creating all necessary tables in your database.
### 3. Run the project:
- To run the project, execute the following command:
```bash
dotnet run
```
### The API will be available by default at:

- **HTTPS: https://localhost:5001**
- **HTTP: http://localhost:5000**

#### Now you're all set to explore and utilize the Online Quiz Platform API! Dive in and start creating, managing, and participating in quizzes. Enjoy your journey into the world of online learning!
```bash
Feel free to adjust any part of it to better fit your style!
```

