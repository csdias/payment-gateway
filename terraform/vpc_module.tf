module "vpc" {
  source = "./modules/vpc"
  
  project                             = "${var.project}"
  aws_account                         = "${var.aws_account}"
  aws_account_name                    = "${var.aws_account_name}"
  vpc_cidr                            = "${var.vpc_cidr}"
  public_subnets_cidr                 = "${var.public_subnets_cidr}"
  private_subnets_cidr                = "${var.private_subnets_cidr}"
  availability_zones                  = "${local.availability_zones}"
}