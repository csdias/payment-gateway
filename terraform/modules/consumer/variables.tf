variable "project" {
  type        = string
  description = "Payment Gateway"
}

variable "aws_account" {
  description = "The AWS account number"
}

variable "aws_account_name" {
  type        = string
  description = "AWS account alias"
}

variable "sns_filter_policy" {
  type        = list
  description = "Payment order sns topic filter policy"
}

variable "consumer_artifact_zip" {
  description = "Name of the artifact in s3 containing the zipped deployable lambda"
  type        = string
}

variable "private_subnet_ids" {
  type        = list
  description = "Private subnet ids"
}

variable "security_group_ids" {
  type        = list
  description = "Security groupd ids"
}