module "consumer" {
  source = "./modules/consumer"
  
  project                            = "${var.project}"
  aws_account                        = "${var.aws_account}"
  aws_account_name                   = "${var.aws_account_name}"
  consumer_artifact_zip              = "${var.consumer_artifact_zip}"
  sns_filter_policy                  = "${var.sns_filter_policy}"
  private_subnet_ids                 = module.vpc.private_subnet_ids
  security_group_ids                 = module.vpc.security_group_ids
}