# Naggaro.AccountStatement Web API

This repository contains a .NET 6 Web API project for managing account statements. Below, you'll find instructions on how to run the code and execute the tests.

## Prerequisites

Before running the application or tests, ensure that you have the following installed on your machine:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/) (optional, if you prefer using the IDE)

## Getting Started

1. Clone the repository to your local machine:
```bash
    git clone https://github.com/yourusername/Naggaro.AccountStatement.git 
```
2.  Navigate to the project root:

```bash

`cd Naggaro.AccountStatement` 
```
3.  Make sure the startup project is set to `Naggaro.AccountStatment.WebApi` in your preferred IDE (Visual Studio) or editor.

## Running the Web API

### Visual Studio:

1.  Open the `Naggaro.AccountStatment.WebApi` project in Visual Studio.
2.  Ensure that the startup project is set to `Naggaro.AccountStatment.WebApi`.
3.  Press F5 to start the application.

### Dotnet CLI:

1.  Open a terminal or command prompt.
2.  Navigate to the `Naggaro.AccountStatment.WebApi` project directory.
3.  Run the following command:

```bash

`dotnet run` 
```
The Web API should now be running at `https://localhost:5001` or `http://localhost:5000`.

## Running Tests

To execute the tests for the Web API, you can use either Visual Studio Test Explorer or the Dotnet CLI.

### Visual Studio Test Explorer:

1.  Open the `Naggaro.AccountStatment.WebApi.Tests` project in Visual Studio.
2.  Build the solution to ensure that the test project is up to date.
3.  Open the Test Explorer from the top menu: Test > Windows > Test Explorer.
4.  Click on the "Run All Tests" button to execute all the tests.

### Dotnet CLI:

1.  Open a terminal or command prompt.
2.  Navigate to the `Naggaro.AccountStatment.WebApi.Tests` project directory.
3.  Run the following command:

```bash

`dotnet test` 
```
This will execute all the tests in the test project.

1.  **Login API Call**

```bash

`curl --location 'https://localhost:7153/api/auth/login' \
--header 'Content-Type: application/json' \
--data '{
    "userName": "admin",
    "password": "admin"
}'
```

This API call sends a POST request to the login endpoint (`/api/auth/login`) with the username and password provided in the JSON payload.

2.  **Get Account Statement Data API Call**

```bash

`curl --location 'https://localhost:7153/api/accountstatement?AccountId=8&FromAmount=3000&ToAmount=3000' \
--header 'Cookie: AccountAppCookie=CfDJ8KAS7mcpOMpLn4g6nl4tDBKmvO9oQQTjmZC15nNrkaRBFmE21HGDx0ylWaO6ZBsFEZyAq2icqxPG3w4w9uM7loP_rVM-MP7Nteyg0F6ycj0IvzG3-_3qmnna1SvCwNU7vh5Xc8NoPNO6MaY-ikgM2N__P_BxYLahSY2cJv_NPRt5xdZeO_mdmLvPZq15ZgCZzOjJvBzp_0PHB24Wah7bUKIghseC7A4NxXo76E0Ox8qcPPyTb8ol2EyySw3CFFLQ6lVL2O8JWXSL0_t9uJeYV_pDry0XJ52LpEqIHCbnylYCdMvsX7gZCsayZYfGjq41YAVAG9lym_mfYU3GLOIowvx23twdJDwL6buhVbzdTH_nC92WSDPe4oXb6pYncISaTD1Azeg69PTMPnH99Z_ogs3ppY2D_0MX9T-WaRySUBSc44baZnm1stDhWd7DtTGsqsAOristAEpGuLTkQ_L9vHAxlVBobao-WUGRmQ-pBFDb_rJjOZ_012L9HWEmzjy2S6oajL6yuOrFpheNYM6NrATcJfV00m8QnZKqxFJMO67g2PyCDhTqq8v721hCm0AxpoA-wl25uYxn-JpJrQbn3lKrshvp0D5W1-Xujv38MKUasmBrwxI-OwET3gGT3-rks0-Qx6LfD4FyGFTR_PHyE0ayIzbV4_2i4FbqKhtQUdmESNANpghulK67EzCLGnyX2cvSzkRvp0hGecbuIr1ADBbBQNtTlJqUIR6oZTCa1e1OhX9dopfMLsGhKDIrJdpG9NUZl7xxoBCMoi3WBn5N0nXIeFbBkdIFjaL5Q27MODMvkS4CFbYd_4R1TNhT5TYZo_bo2u_EF9WEF9cCq0z0Jf4'
```

This API call sends a GET request to the `accountstatement` endpoint with query parameters `AccountId`, `FromAmount`, and `ToAmount`,`FromDate`, and `ToDate`. The request also includes a `Cookie` header with the `AccountAppCookie` value, which is  obtained after a successful login. Please note that you should replace the cookie value in the `Cookie` header with the actual value you received after logging in.

Make sure your Web API is running and that the provided URLs and credentials are correct. Additionally, adjust the base URL (`https://localhost:7153`) if your Web API runs on a different port.


## Integrating the Account Statement API into a microservices architecture
Integrating the Account Statement API into a microservices  involves designing it to interact with other microservices efficiently while ensuring reusability and scalability. Below are some key considerations for integrating the API:

1.  **API Gateway:** In a microservices architecture, an API Gateway acts as a central entry point for external clients. The Account Statement API can be exposed through the API Gateway, which will handle requests, route them to the appropriate microservice, and aggregate responses if needed.
    
2.  **Asynchronous Communication:** To avoid tight coupling and improve scalability, microservices should communicate asynchronously. The Account Statement API can use message queues (e.g., RabbitMQ, AzureServiceBus) to publish events and notify other microservices about changes or updates.
    
3.  **Shared Data Contracts:** To ensure reusability, define shared data contracts with other microservices. The Account Statement API can use standard data models that align with the organization's data standards.
    
4.  **Security and Authentication:** Implement a security strategy, including authentication and authorization mechanisms. APIs should enforce access control to prevent unauthorized access to sensitive data. OAuth 2.0 or JWT tokens can be used for secure communication between microservices.
    
5.  **Caching:** To improve performance, using caching mechanisms for frequently accessed data. Caching can be employed at various levels, such as the API Gateway or individual microservices. The Account Statement API can cache common queries or results.
    
6.  **Polyglot Persistence:** Different microservices might require different databases based on their needs. The Account Statement API can use the most appropriate data storage technology (e.g., SQL, NoSQL) to handle its data while integrating with other microservices that might use different databases.
    
7.  **External API Integration:** Microservices often interact with external APIs. The Account Statement API can be designed to communicate with external services, aggregating data or exposing specific functionalities to external clients.
    
8.  **Event-Driven Architecture:**  adopting an event-driven architecture to enable loose coupling and scalability. The Account Statement API can publish events when specific actions occur, and other microservices can subscribe to these events and take actions accordingly.
    
9.  **Resilience and Fault Tolerance:** Microservices should be resilient to failures. Implement retry mechanisms, circuit breakers, and timeouts to handle potential failures when interacting with other microservices. The Account Statement API can use patterns like Circuit Breaker and Retry.
