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
- 1 NAT Gateway.
- 1 Payment lambda function of proxy type
- 1 Lambda role with permission to execution inside the vpc to increase lambda availability.
- 1 Http Api Gateway of proxy type and lambda integration.
- 1 Subnet group attaching the two public subnets to increase postgres availability and using the vcp security group to allow postgress to be connected exernally.
- 1 Outbound rule to allow postgres to be connected externally so that we can migrate data.
- 1 Rds postgres
- 1 Sns topic that will receive payment orders.
- 1 Sqs that will subscribe to the payment orders topic.
- 1 Queue processor lambda attached to the Sqs to process the queue items, then call the https://get.mocklab.io/ to simulate the CkoBankSimulator, then update the 
payment status with the CkoBankSimulator response.

![aws-infra](https://user-images.githubusercontent.com/16576809/158211364-b6906090-d2ee-4551-9fcb-2ef1a96a3ccb.png)

<br/>(actually in our case it is slightly different, the 2 public subnet related to a subnet group will host postgres and the 2 private subnets will host lambda)


## Environment Variables

* `DATABASE_CONNECTION_STRING` - Connection string with relational database. ie. Host=127.0.0.1;Port=5432;Pooling=true;Database=PaymentGateway;User Id=postgres;Password=postgrespwd;"

* `CKOBANK_SIMULATOR_ENDPOINT` - TBD

### Installing

Download Terraform and Intall. => https://www.terraform.io/downloads

- Setup Environment Variables via `export` command or via `appsettings.json` file on the src/InterfaceAdapters path of the repo
- DotNet 6 Cli (Core SDK)
- DotNet 6
- EF Core Tools
- Node.js (maybe with chocolatey, depending on you)
- Npm
- Yarn
- Amazon Lambda Tools

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

First install postgres https://www.postgresql.org/download/ or docker with a postgres image https://www.docker.com/products/docker-desktop

Open the solution and press F5

Use the Payment Transation fc782e65-0117-4c5e-b6d0-afa845effa3e in the GET method

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
Create a zipped artifact to be deployed to Amazon Cloud
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

# What is done:

The solution with the payment lambda and its tests are building without errors. The Entity Frameworks creates automatically all the tables in postgres via migrations. When running locally, the payment lambda exposes an api with all the routes set up to alow creating payment orders (POST), checking payment order statuses (GET), finding all payments given a merchant id (GET) and updating a payment order (PUT).
<br/>
When the POST method is called, the payment lambda saves the payment order in postgres and then returns a transaction id to the merchants so that the payment order status can be checked in the future using the the same api method GET.

<br/>
The terraform is already working and deploying to an Aws account.

# What needs to be done:
<br/> Add more validation scenarios. That can be done by just adding rules in the Fluent Validation structure.
<br/> Add more test scenarios. That can be done by just adding more method tests.
<br/> Fix integrations tests and verify how to test the lambdas.
<br/>
Create a new dotnet project for the queue processor lambda.
Make this queue processor lambda call the CkoBankSimulator (exernal url https://get.mocklab.io/) and update a payment order with the response.
Create the tests for the queue processor lambda.
Create more routes in the mocklab to enrich the CkoBankSimulator test scenarios.

<br/>
In terraform:
<br/>add the aurora postgres with the correct outbound rule to allow migrations and pgAdmin.
<br/>add a s3 bucket to receive the zipped lambda assemblies (currently the zip artifact upload is manual).
<br/>add the payment lambda resource.
<br/>add the sns topic resource.
<br/>add the sqs resource.
<br/>add the queue processor lambda resource.
 
# Improvements:
<br/>Add authentication.
<br/>Modify the payment lambda to use mediator pattern instead of simple dependency injection.
<br/>Use a rest api gateway instead of a http api gateway proxy. The idea being having dedicated routes and dedicated lighter and faster lambda functions.
<br/>Use eventual persistence between the moment that lambda persists the payment order in postgres and the moment that it publishes a message in a sns topic.
<br/>Add restful pagination.


