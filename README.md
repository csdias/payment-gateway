# Payment Gateway Api
Api that allows merchants to charge their customers through credit card transactions.

## About
This is a study purpose project only.
The idea is to apply modern standards and good practices, for example, it is build using clean architecture, it uses some concepts of microservices,
it uses message queue pattern and it is hosted in the cloud for high availability and scalability.

![clean_architecture](https://user-images.githubusercontent.com/16576809/158211294-48b0d242-a61a-4d99-a33f-6976e2017681.jpg)

## Mechanism
There is a payment lambda that exposes an api that receives payment orders and writes them in an aurora postgres. This api is meant to be used by the merchants.
The payment lambda saves the payment order and returns a transcation id so that the merchants can track the status of payment orders in a future time when it is done processing.
The payment lambda publishes a message in a sns topic so that the credit analysis will be decoupled and scalable.
A queue subscribes to this sns topic.
A queue processor lambda is hooked up to this queue.
The queue processor lambda process each new queue item by unwrapping the message, calling the CkoBankSimulator and updating the the payment order status with the CkoBankSimulator response.

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

To destroy everything and stop incorring in costs
```
  terraform destroy

```

# What is done and what needs to be done:

The solution with the payment lambda and its tests are building without errors. More test scenarios are required. When running locally (a postgres installation is needed, or a docker installation with a postgres image) the payment lambda exposes an api in which the routes are in place to alow creating payment orders (POST), check payment order statuses (GET), find all payments given a merchant id (GET), update a payment order (PUT).
<br/>
When the POST method is called, the payment lambda saves the payment order in postgres and then return a transaction id to the merchants so that the payment order status can be checked in the future using the the same api method GET

<br/>
The terraform is already working and deploying to an Aws account.

Needs to be done:
<br/>
Create a new project for the queue processor lambda.
Make this queue processor lambda call the CkoBankSimulator (exernal url https://get.mocklab.io/) and update a payment order with the response.
Create the tests for the queue processor lambda.

In terraform configuration is necessary to
add the aurora postgres with the correct outbound rule to allow migrations and pgAdmin
add a s3 bucket to receive the zipped lambda assemblies.
add the payment lambda 
add the sns topic
add the sqs 
add the queue processor lambda
 
Reavaluate the possiblity to:
Tefactor the payment lambda to use mediator pattern instead of simple dependency injection.
Use a rest api gateway instead of a http api gateway proxy. the idea being having dedicated routes and dedicated micro lambda functions.
