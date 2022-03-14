# Payment Gateway Api
Api that allows merchants to charge their customers through credit card transactions.

## About
This is a study purpose project only.
The idea is to apply modern standards and good practices, fo example, it is build using clean architecture, it uses some concepts of microservices,
it uses message queue pattern and it is hosted in the cloud for high availability and scalability.

## Mechanism
There is a main lambda that exposes an api that receives payment orders and writes them in an aurora postgres. This api is meant to be used by the merchants.
The main lambda saves the payment order and return a transcation id so that the merchants can track the status of payment orders in a future time when it is done processing.
The main lambda publishes a message in a sns topic so that the credit analysis will be decoupled and scalable.
A queue subscribes to this sns topic.
A queue processor lambda is hooked up to this queue.
The queue processor lambda process each new queue, by unwrapping the message, and calling the CkoBankSimulator and updating the the payment order status with the CkoBankSimulator response.

## AWS Environment
Amazon Resources Created Using Terraform

- 1 Vpc.
- 2 Public subnets.
- 2 Private subnets.
- 1 Internet Gateway attached to the vpc.
- 1 Private route table and 1 public route table.
- 1 NAT Gateway.
- 1 Main lambda function of proxy type
- 1 Lambda role with permission to execution inside the vpc to increase availability.
- 1 Http Api Gateway of proxy type and lambda integration.
- 1 subnet group attaching the two public subnets to increase availability and using the vcp security group to allow postgress to be connected exernally.
- 1 Outbound rule to allow postgres to be connected externally so that we can migrate data.
- 1 Rds postgres
- 1 Sns topic that will receive payment orders.
- 1 Sqs that will subscribe to the payment orders topic.
- 1 second lambda attached to the Sqs to process the queue itens, then call the https://get.mocklab.io/ to simulate the CkoBankSimulator, then update the 
payment status with the CkoBankSimulator response.

## Environment Variables

* `DATABASE_CONNECTION_STRING` - Connection string with relational database. ie. Host=127.0.0.1;Port=5432;Pooling=true;Database=PaymentGateway;User Id=postgres;Password=postgrespwd;"

* `CKOBANK_SIMULATOR_ENDPOINT` - TBD

### Installing

Download Terraform and Intall. => https://www.terraform.io/downloads

- Setup Environment Variables via `export` command or via `appsettings.json` file on the src/InterfaceAdapters path of the repo
- DotNet cli (Core SDK)
- DotNet 3.1+
- Node.js v8+
- Npm v6+
- Yarn v1.21+
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

Open the solution and press F5

Use the Payment Transation fc782e65-0117-4c5e-b6d0-afa845effa3e in the GET method

### Deploying to Cloud
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

The solution builds and when it is running locally (for this a postgres installation is needed) is already able to receive payment orders write them on postgres and return a transaction id to be searched in a future time in the same api.

The terraform is already working and deploying to an Aws account.

Needs to be done:
Create a new project for the queue processor lambda.
Make this queue processor lambda call the CkoBankSimulator (exernal url https://get.mocklab.io/) and update a payment order with the response.

In terraform configuration is necessary to
add the aurora postgres with the correct outbound rule to allow migrations and pgAdmin
add a s3 bucket to receive the zipped lambda assemblies.
add the payment lambda 
add the sns topic
add the sqs 
add the queue processor lambda
 
