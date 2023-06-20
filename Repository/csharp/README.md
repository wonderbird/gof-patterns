# Repository in C#

## Run MongoDB in Docker

Although not required by the code in this solution, the [docker-compose.yml](./docker-compose.yml) stack shows how
to configure a MongoDB instance.

Start MongoDB and the admin UI mongodb-express:

```shell
docker-compose up
```

You can connect to the database using `mongosh` as follows:

```shell
docker run --rm -it --network repository_default mongo:latest mongosh --host mongo -u root -p example --authenticationDatabase admin
```

## References

- [Microsoft Learn: Create a web API with ASP.NET Core and MongoDB](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app)
- [Docker Hub: mongo](https://hub.docker.com/_/mongo)
- [MongoDB Manual](https://www.mongodb.com/docs/manual/)
