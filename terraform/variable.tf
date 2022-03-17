variable "project" {
  type        = string
  description = "Payment Gateway"
}

variable "region" {
  type        = string
  description = "London"
  default     = "eu-wes-2"
}

variable "aws_account" {
  description = "The AWS account number"
}

variable "vpc_cidr" {
}

variable "public_subnets_cidr" {
  type        = list
}

variable "private_subnets_cidr" {
  type        = list
}

variable "src_zip_artifact" {
  description = "Name of the artifact in s3 containing the zipped deployable lambda"
  type        = string
}

variable "aws_account_name" {
  type        = string
  description = "AWS account alias"
}