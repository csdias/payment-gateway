resource "aws_rds_cluster_parameter_group" "this" {
  description = "RDS DB cluster parameter group"
  family      = "aurora-postgresql10"
  parameter {
    name  = "client_encoding"
    value = "utf8"
  }
}

resource "aws_db_subnet_group" "this" {
  description = "Access RDS DB subnet group"
  subnet_ids  = module.vpc.public_subnet_ids
}

resource "aws_rds_cluster" "pg" {
  cluster_identifier              = "payment-gateway"
  engine                          = "aurora-postgresql"
  engine_mode                     = "serverless"
  engine_version                  = "10.14"
  db_subnet_group_name            = aws_db_subnet_group.this.name
  storage_encrypted               = true
  db_cluster_parameter_group_name = aws_rds_cluster_parameter_group.this.name
  vpc_security_group_ids          = module.vpc.security_group_ids
  copy_tags_to_snapshot           = true
  deletion_protection             = var.rds_deletion_protection
  backup_retention_period         = var.rds_backup_retention_period
  apply_immediately               = true
  # Terraform bug around skipping snapshot
  # https://stackoverflow.com/questions/50930470/terraform-error-rds-cluster-finalsnapshotidentifier-is-required-when-a-final-s
  skip_final_snapshot = true

  master_username     = "postgres"
  master_password     = "postgrespwd"
  snapshot_identifier = "snapshot_identifier"

  scaling_configuration {

    auto_pause               = var.rds_auto_pause
    max_capacity             = var.rds_max_capacity
    min_capacity             = var.rds_min_capacity
    seconds_until_auto_pause = var.rds_auto_pause_delay
    timeout_action           = "ForceApplyCapacityChange"
  }
}
