# WebAPI Async in C# .NET 6

This is a simple .NET Web API created as a study simulating a Blog service with requests for getting and creating new comments.

The following tools and technologies were used:

- Visual Studio 22;
- .NET 6;
- Clean Architecture;
- API REST;
- Swagger UI;
- MS SQL Server 2019 Express;
- Dapper;
- RabbitMQ;
- Docker;
- Unit tests;
- Integration tests.

## Web UI
![image](https://github.com/amandasdn/netcore-webapi-async-docker/assets/47601336/47106aeb-e825-4d6a-8acf-1410da2c7ccb)

There are 2 endpoints in this API protected by a API key.

### GET: Comment
Search for all the comments that are stored in SQL. There are some filters as query parameters such as page_size, page_number, and author. The content will be presented with pagination from the selected filter.

### POST: Comment
Send a comment content to a queue in RabbitMQ. Later, asynchronously, this content will be sent to a table in SQL.

⚠ Remind: before request any endpoint, press the button "Authorize" and set the API key value.

## Architecture

### Domain
This will contain all entities, models, interfaces and logic specific to the Domain layer.

### Application
This layer contains all application logic. It is only dependent on the Domain layer. This layer defines interfaces that are implemented by outside layers. For example, all the methods that are called on the controllers.

### Infrastructure
This layer is divided into 2 parts: Data and IoC. The Data layer is dependent on the Domain layer and contains classes for accessing external resources such as databases, web services, and message senders. The IoC (Inversion of Control) layer is dependent on both the Domain and Infrastructure Data layers and contains classes for project configuration, such as managing dependencies and middlewares. 

### WebUI
This layer contains all the endpoints that composed the web API. This layer is only dependent on the Infrastructure IoC layer. 

## Running and testing

Clone the project to any directory where you do development work.

```
git clone https://github.com/amandasdn/netcore-webapi-async-docker.git
```

On Docker, it was created three images: one for RabbitMQ, one for MS SQL Server, and one for the WebAPI.

In the repository folder `...\netcore-webapi-async-docker\Blog`, run the following command in a container with:

```
docker-compose up -d
```

The database (MS SQL Server) and the message broker (RabbitMQ) will be created. And then, you can run the web API in the browser.

```
http://localhost:8081/swagger/index.html
```

