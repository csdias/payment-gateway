output "vpc_id" {
  value = "${aws_vpc.vpc.id}"
}

output "integration_uri" {
  value = "${aws_apigatewayv2_integration.integration.integration_uri}"
}