# Payment Gateway Api
Api that allows merchants to charge its customers through credit card transactions.

## About
This is a study purpose project.

It applies good practices, uses clean architecture and can be hosted in the cloud

Environment options: 
On Amazon Cloud through Terraform
On premisses through Docker (deprecated)

## AWS Environment
Amazon Resources Created Using Terraform

- 1 AWS VPC with 10.0.0.0/16 CIDR.
- 2 AWS VPC public subnets would be reachable from the internet; which means traffic from the internet can hit a 
machine in the public subnet.
- 2 AWS VPC private subnets which mean it is not reachable to the internet directly without NAT Gateway.
- AWS VPC Internet Gateway and attach it to AWS VPC.
- Public and private AWS VPC Route Tables.
- AWS VPC NAT Gateway.
- Associating AWS VPC Subnets with VPC route tables.
- A Lambda function of proxy type
- A Lambda role with permission to execution inside the vpc 
- An Http Api Gateway of proxy type and lambda integration
- A subnet group attaching the two public subnets and using the vcp security group
- One RDS Postgres


## Environment Variables

* `DATABASE_CONNECTION_STRING` - Connection string with relational database.

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

### Deploying
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
