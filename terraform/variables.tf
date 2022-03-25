variable "region" {
  type        = string
  description = "London"
  default     = "eu-wes-2"
}

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

variable "src_zip_artifact" {
  description = "Name of the artifact in s3 containing the zipped deployable lambda"
  type        = string
}

variable "consumer_artifact_zip" {
  description = "Name of the artifact in s3 containing the zipped deployable lambda"
  type        = string
}

variable "sns_filter_policy" {
  type        = list
  description = "Payment order sns topic filter policy"
}

variable "rds_auto_pause" {
  description = "Boolean to indicate whether we wish to use the Aurora auto pause feature"
  type = bool
}

variable "rds_auto_pause_delay" {
  description = "If using auto pause, time in seconds to wait before pausing the aurora database"
  type = number
}

variable "rds_max_capacity" {
  description = "Maximum number of Aurora capicity units"
  type = number
}

variable "rds_min_capacity" {
  description = "Minimum number of Aurora capicity units"
  type = number
}

variable "rds_backup_retention_period" {
  description = "Number of days to retain RDS backups"
  type        = number
}

variable "rds_deletion_protection" {
  description = "Indicates whether to apply deletion protection to the RDS"
  type        = bool
}