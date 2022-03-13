region      = "eu-west-2"
environment = "payment-gateway"
vpc_cidr             = "10.0.0.0/16"
public_subnets_cidr  = ["10.0.0.0/24", "10.0.1.0/24"] 
private_subnets_cidr = ["10.0.2.0/24", "10.0.3.0/24"]
production_availability_zones = ["10.0.2.0/24", "10.0.3.0/24"]