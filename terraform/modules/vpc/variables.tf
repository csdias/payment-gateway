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

variable "vpc_cidr" {
}

variable "public_subnets_cidr" {
  type        = list
}

variable "private_subnets_cidr" {
  type        = list
}

variable "availability_zones" {
  type        = list
}