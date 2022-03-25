resource "aws_apigatewayv2_api" "api" {
  name          = "${var.project}-${var.aws_account}-${var.aws_account_name}-api"
  protocol_type = "HTTP"

  cors_configuration {
    allow_credentials = false
    allow_headers     = ["*"]
    allow_methods     = ["*"]
    allow_origins     = ["*"]
    expose_headers    = ["*"]
    max_age           = 3600
  }

  tags = {
    Name        = "${var.project}-api-gw"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_apigatewayv2_integration" "integration" {
  api_id               = aws_apigatewayv2_api.api.id
  integration_type     = "AWS_PROXY"
  connection_type      = "INTERNET"
  description          = "This is our {proxy+} integration"
  integration_method   = "POST"
  integration_uri      = aws_lambda_function.lambda_payment_gateway.invoke_arn
  passthrough_behavior = "WHEN_NO_MATCH"

  lifecycle {
    ignore_changes = [
      passthrough_behavior
    ]
  }
}

resource "aws_apigatewayv2_route" "route" {
  api_id             = aws_apigatewayv2_api.api.id
  route_key          = "ANY /{proxy+}"
  target             = "integrations/${aws_apigatewayv2_integration.integration.id}"
}

resource "aws_apigatewayv2_stage" this {
  api_id               = aws_apigatewayv2_api.api.id
  name                 = "$default"
  auto_deploy = true
}

