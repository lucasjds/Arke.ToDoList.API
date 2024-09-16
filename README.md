# Introduction 
The **Arke ToDoList** Challenge is designed to run autonomously, in a container, or as a standalone program, and wait for input from its corresponding API. 

The Arke ToDoList Challenge is:
* **Autonomous**: The program runs separately from any other system.
* **Testable**: The program will give a detailed report with the code coverage. This report is inside Tests project.
* **Scalable**: The program can be a microservice and the scructure of the program is well-designed. This facilitates scalability, maitinaning a modular and decoupled architecture.

# Getting Started
Before starting, you require to have installed:
1. Microsoft .NET 8.0
2. Docker Desktop
3. Postman
4. Visual Studio (was used Visual Studio 2022 Community)

# Build and Test

For setting up the environment is necessary to:

1. Install Docker Desktop
2. Open the Command Prompt or Powershell on the `/Arke.ToDoList.API` folder and execute the following command:
    1. `docker-compose up --build -d`
    2. That will generate a container in the port 6500
    3. Then the whole test can be done through Postman. There is a collection inside `Arke.ToDoList.API\Arke.ToDoList.API` folder

# Debug locally
For debugging the application locally, stop the component you want to debug in Docker Desktop and run the respective project directly in your IDE.

# The Architecture
All the train of thought can be followed through the commits in the github. Initially, I started doing the repositories and then moved to the services where it is found all the business logic. Last but not least, I completed the API with the controller. 

After finishing the logic, unit tests, and etc; I moved to change the whole structure creating libraries and separating the code into class libraries, therefore, by doing that, the code is decoupled, more scalable and easy to maintain. 
This is explained in the next section.

## DDD
The reason DDD was used and because it gives many advantages such as:

#### Flexibility and Scalability
Modularity: DDD encourages dividing the system into bounded contexts, which helps maintain a modular and decoupled architecture. This facilitates scalability and maintainability.
Change Isolation: Changes within one bounded context do not affect other contexts, allowing parts of the system to evolve independently.
### Easy Maintenance
Testability: With a well-defined domain model, it is easier to write unit and integration tests since business logic is encapsulated and isolated.
High-Quality Code: Domain-oriented design promotes cleaner, more understandable code, which makes the system easier to maintain and evolve.
### Foundation for architecture and design
Domain-Based Architecture: DDD provides a solid foundation for system architecture, which can include architectures like Microservices.
### Improved Documentation and Understanding
Domain Documentation: The domain model serves as a living documentation that describes the system’s structure and behavior in detail and is easy to understand.
Training and Onboarding: Eases the training of new developers and team members, as the domain and system logic are clearly represented in the code.

## Database
The Database used is in-memory Database.
## ORM
The ORM utilized is Entity Framework Core.
## Unit Test
For the unit test, it was used xUnit, FluentAssertions, Bogus, and Moq. 
FluentAssertions provides us an easy way to assert the result comparing entire objects. Bogus provides us a way to fake the models and the entities.
# Code Coverage
Inside the `Arke.ToDoList.API\Arke.ToDoList.Tests`, it is found a report on the Code Coverage. To keep it simple, only the Business Logic was covered.

# Things to Improve
1. I could have used FluentValidations to have a more scalable application. By doing that, I would have separated the validation from the TaskService, for example.
2. To document, I could have used stylecop. By Doing that I could have documennted the Interfaces and all the files with a brief explanation
3. I could have created a nuget package of Arke.ToDoList.Shared. By doing that, other applications - such as the front end - could use the classes in this project. A good example is TaskModel that can be shared with the front end.
4. I could have created unit tests for the controllers.
   


