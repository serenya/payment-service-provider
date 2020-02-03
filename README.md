![](https://github.com/serenya/payment-service-provider/workflows/ci-on-push/badge.svg)

# About Payment Service Provider

Represents API based application that allow merchants to offer a way for their shoppers to pay for products.

## Installation

Make sure that you have installed [Docker Desktop](https://www.docker.com/products/docker-desktop) on your local machine before moving further.

Also you will need to install dotnet cli and [dotnet ef](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)
to run migrations.

The easiest way to spin up the whole application without installing any runtimes and dependencies is to run it within docker container. In the root of the project you may find `docker-compose.yml` file. This file contains definition of insfrastructure in term of dockerized application. By running a command:

```
docker-compose up
```

Following services of Payment Service Provider get spinned up:

- [PaymentGateway.Api](http://localhost:5000)
- [IdentityServer](http://localhost:5555)
- [AcquiringBank.Api.Mock](http://localhost:5050)
- [PostgreSQL](https://www.postgresql.org/)
- [Adminer](https://www.adminer.org/)

After that migration command has to be executed to initiate database structure. It has to be executed in the root of the repository from command prompt:

```
 cd .\src\PaymentGateway\PaymentGateway.Data\ && dotnet ef database update
```

## Explore Payment Gateway API

In the root of the repository you may find a directory called [merchant-client](https://github.com/serenya/payment-service-provider/tree/master/merchant-client).
Within it is located [Postman](https://www.postman.com/) collection with environment setup. Collection contains three requests. One request is for
authentication reason. Others represent actual API endpoints for processing
payment requests and fetching details about done payments.

Feel free to import collection with environment and play around with Payment Gateway API.

## Improvements

As a potential improvements following tasks have to be taken into account for the nearest future:

- Improve validation errors
- Improve exception handling
- Setup secure communication between Payment Gateway and Acquiring Bank
- Implement MerchantRepository
- Read MerchantId from claims
- Add MessageQueue in case of long-running requests on Acquiring Bank side
- Add Swagger/OpenAPI support
- Use Polly for 3rd-party communications
- Add CorrelationId to trace customer requests