# Payment Gateway Api
Api that allows merchants to charge their customers through credit card transactions.

## About
This is a study purpose project only.
The idea is to apply modern standards and good practices, for example, it's built using clean architecture, it uses some concepts of microservices,
it uses message queue pattern and it is hosted in the cloud for high availability and scalability.

![clean_architecture](https://user-images.githubusercontent.com/16576809/158211294-48b0d242-a61a-4d99-a33f-6976e2017681.jpg)

## Mechanism
There is a payment lambda that exposes an api that receives payment orders and writes them in an aurora postgres. This api is meant to be used by the merchants.
The payment lambda saves the payment order and returns a transaction id so that the merchants can track the status of payment orders in a future time when it is done processing.
The payment lambda publishes a message in a sns topic so that the credit analysis is decoupled and scalable.
A queue subscribes to this sns topic.
A queue processor lambda is hooked up to this queue.
The queue processor lambda process each new queue item by unwrapping the the payload from message, calling the CkoBankSimulator with the payment payload and updating the the payment order status with the CkoBankSimulator response.

## AWS Environment
Amazon Resources Created Using Terraform

- 1 Vpc.
- 2 Public subnets.
- 2 Private subnets.
- 1 Internet Gateway attached to the vpc.
- 1 Private route table and 1 public route table.
- 1 NAT Gateway with an elastic ip (according with the AWS guides, we could use one for each private subnet to increasy availability)
- 1 Payment lambda function of proxy type
- 1 Lambda role with permission to execution inside the private subnets on the vpc to access the private resources like, postgres, sns, sqs.
- 1 Http Api Gateway of proxy type and lambda integration.
- 1 Subnet group attaching the two public subnets to increase postgres availability and using the vcp security group to allow postgress to be connected exernally.
- 1 Outbound rule to allow postgres to be connected externally so that we can migrate data.
- 1 Rds Aurora Postgres
- 1 Sns topic that will receive payment orders.
- 1 Sqs that will subscribe to the payment orders topic.
- 1 Queue processor lambda attached to the Sqs to process the queue items, then call the https://get.mocklab.io/ to simulate the CkoBankSimulator, then update the 
payment status with the CkoBankSimulator response.

![aws-infra](https://user-images.githubusercontent.com/16576809/158211364-b6906090-d2ee-4551-9fcb-2ef1a96a3ccb.png)

<br/>(actually in our case it is slightly different, the 2 public subnet related to a subnet group will host postgres and the 2 private subnets will host lambda, sqs and sns)


## Environment Variables

Code:<br/>
.\src\FrameworksAndDrivers\appsettings.json<br/>
`DATABASE_CONNECTION_STRING` - Connection string with relational database. ie. Host=127.0.0.1;Port=5432;Pooling=true;Database=PaymentGateway;User Id=postgres;Password=postgrespwd;"<br/>
 `CKOBANK_SIMULATOR_ENDPOINT` - TBD <br/>
 
IaS:<br/>
.\src\terraform\terraform.tfvars<br/>

| Name                 | Value                            |
| ---------------------| -------------------------------- |
| region               | = "eu-west-2"                    |
| project              | = "payment-gateway"              |
| src_zip_artifact     | = "FrameworksAndDrivers.zip"     |
| vpc_cidr             | = "10.0.0.0/16"                  |
| public_subnets_cidr  | = ["10.0.0.0/24", "10.0.1.0/24"] |
| private_subnets_cidr | = ["10.0.2.0/24", "10.0.3.0/24"] |
| aws_account_name     | = "dev"                          |
| aws_account          |  = "999999999999"                |


### Installing

- Setup Environment Variables via `export` command or via `appsettings.json` file on the src/FrameworksAndDrivers path of the repo

- .NET 6 SDK https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- Entity Framework Core Tools for the .NET Command-Line Interface.
```
  dotnet tool install --global dotnet-ef
```
- Node.js 16.14.0+ and Npm 16.0.0+ (optional Chocolatey) https://nodejs.org/en/download/
- Amazon Lambda Tools 5.3.0
```
  dotnet tool install --global Amazon.Lambda.Tools --version 5.3.0
```
- Terraform 1.1.7 https://www.terraform.io/downloads


### Install dependencies
```
  npm install
  npm run restore
```

### Unit Tests
```
  npm test
```
You can check the results on the prompt or open the coverage folder, inside it, open index.html in any browser.

### Running locally

<h4>Without Docker:</h4>
Install postgres https://www.postgresql.org/download/

Open the solution and press F5. This will automatically open the swagger api in the browser

<h4>With Docker:</h4>
Install docker with a postgres image https://www.docker.com/products/docker-desktop

Run 
```
  cd .\Payment-Gateway
  docker-compose up pg-api
```
Open the browser, point localhost:5000/payments. This wil open the swagger api.

Now, use the Payment Transation fc782e65-0117-4c5e-b6d0-afa845effa3e in the GET method

### Deploying to Cloud

Change the "DATABASE_CONNECTION_STRING" to point to an AWS Postgres endpoint, for example: 
<br/>"Host=paymentgateway.cluster-ckpfmgmjaul2.eu-west-2.rds.amazonaws.com;Port=5432;Pooling=true;Database=PaymentGateway;User Id=postgres;Password=cJygMfXc1CbQpQ0bByPe;",


Install the Entity Framework Tools
```
  dotnet tool install --global dotnet-ef 
```

Run the ef database update command
```
  dotnet ef database update  
```

Install the Amazon Lambda Tools
```
  dotnet tool install -g Amazon.Lambda.Tools
```
Build the solution by right clicking on the file .\build.ps1 and by running it with PowerShell

Instead of the previous step it is possible to create a zipped artifact to be deployed to Amazon Cloud with a dotnet lambda cli command
```
  cd .\FrameworksAndDrivers
  dotnet lambda package 
```

Pre-Requisites to create an infrastructure on AWS using Terraform
<br/>
It is required AWS IAM API keys (access key and secret key) for creating and deleting permissions for all 
AWS resources. Terraform should be installed on the machine. If Terraform does not exist you can download and 
install it from https://www.terraform.io/downloads.

```
  cd .\terraform
  terraform init
  terraform plan
  terrform apply
```
Type yes when questioned if you agree to deploy to Amazon Cloud (there might be costs applied by Amazon AWS)

To destroy everything and stop incurring costs
```
  terraform destroy
```

# What has been made so far:
The solution with the payment lambda and its tests are building without errors. The Entity Frameworks creates automatically all the tables in postgres via migrations. When running locally, the payment lambda exposes an api with all the routes set up to alow creating payment orders (POST), checking payment order statuses (GET), finding all payments given a merchant id (GET) and updating a payment order (PUT).
<br/>
When the POST method is called, the payment lambda saves the payment order in postgres and then returns a transaction id to the merchants so that the payment order status can be checked in the future using the the same api method GET.
<br/>
The terraform is already working and deploying to an Aws account. Among other things, the configuration already allows the private subnets where the lambdas will run to make the necessary external calls to the CkoBankSimulator (https://get.mocklab.io/).

# What needs to be done:
<br/>Configuration in Terraform:
<br/>AWS Aurora Postgres Resource.
<br/>AWS SNS Topic Resource.
<br/>AWS SQS Resource.
<br/>AWS Queue Processor Lambda Resource.

<br/>Code:
<br/>Add more validation scenarios.
<br/>Add more test scenarios.
<br/>Fix integrations tests.
<br/>Create the dotnet queue processor lambda function.
<br/>Create routes https://get.mocklab.io/ to simulate the CkoBankSimulator.
<br/>Create the tests for the queue processor lambda.
 
# Improvements:
<br/>Add authentication.
<br/>Modify the payment lambda to use mediator pattern instead of simple dependency injection.
<br/>Use a rest api gateway instead of a http api gateway proxy. The idea would be to have dedicated routes and dedicated lighter and faster lambda functions.
<br/>Create a version of the lambda with a theoretically lighter ORM (Dapper, maybe) and compare it with the current EF.
<br/>Add restful pagination.
<br/>Add Concurrent Message Consumption Pattern.
<br/>Add Concurrent Message Publication Pattern.
<br/>Add Inbox Pattern.
<br/>Add Message Exchange Pattern.
<br/>Add Outbox Pattern.
<br/>Add Replay Pattern.


