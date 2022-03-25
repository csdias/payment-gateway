resource "aws_lambda_function" "lambda_payment_gateway" {
  function_name     = "PaymentGateway"
  s3_bucket         = aws_s3_bucket.lambda_bucket.id
  s3_key            = aws_s3_bucket_object.lambda_payment_gateway.key
  source_code_hash = data.archive_file.lambda_zip.output_base64sha256                                                                                                                                                

  role              = aws_iam_role.iam_role.arn
  handler           = "FrameworksAndDrivers::FrameworksAndDrivers.LambdaEntryPoint::FunctionHandlerAsync"
  runtime           = "dotnet6"
  memory_size       = "256"
  timeout           = 120
  publish           = true

  vpc_config {
    subnet_ids         = module.vpc.private_subnet_ids
    security_group_ids = module.vpc.security_group_ids
  }

  environment {
    variables = {
      ASPNETCORE_ENVIRONMENT  = "Lambda Development"
      RDS_HOSTNAME            = aws_rds_cluster.pg.arn
    }
  }

  tags = {
    Name        = "${var.project}-lambda"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_lambda_permission" "api_gw_lambda" {
  statement_id  = "AllowExecutionFromAPIGateway"
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.lambda_payment_gateway.function_name
  principal     = "apigateway.amazonaws.com"

  # More: http://docs.aws.amazon.com/apigateway/latest/developerguide/api-gateway-control-access-using-iam-policies-to-invoke-api.html
  source_arn = "arn:aws:execute-api:${var.region}:${var.aws_account}:${aws_apigatewayv2_api.api.id}/*/*/{proxy+}"
}