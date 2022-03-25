output "vpc_id" {
  value = module.vpc.vpc_id
}

output "integration_uri" {
  value = "${aws_apigatewayv2_integration.integration.integration_uri}"
}

output "private_subnet_ids" {
  description = "IDs of the VPC's private subnets"
  value       = module.vpc.private_subnet_ids
}

output "rds_arn" {
  description = "RDS arn"
  value       = aws_rds_cluster.pg.arn
  sensitive   = true
}

