# WebAPI Async in C# .NET 6

This is a simple .NET Web API created as a study simulating a Blog service with requests for getting and creating new comments.

The following tools and technologies were used:

- Visual Studio 22;
- .NET 6;
- Clean Architecture;
- API REST;
- Swagger UI;
- MS SQL Server;
- Dapper;
- RabbitMQ;
- Docker;
- Unit tests;
- Integration tests.

![image](https://github.com/amandasdn/netcore-webapi-async-docker/assets/47601336/47106aeb-e825-4d6a-8acf-1410da2c7ccb)

## Status
![](https://img.shields.io/github/last-commit/amandasdn/netcore-webapi-async-docker)

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

And then, you can run the web API in the browser.

```
http://localhost:8081/swagger/index.html
```

