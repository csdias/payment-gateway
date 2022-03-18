locals {
  availability_zones    = ["${var.region}a", "${var.region}b"]
}

resource "aws_vpc" "vpc" {
  cidr_block           = "${var.vpc_cidr}"
  enable_dns_hostnames = true
  enable_dns_support   = true

  tags = {
    Name        = "${var.project}-vpc"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_internet_gateway" "ig" {
  vpc_id = "${aws_vpc.vpc.id}"

  tags = {
    Name        = "${var.project}-igw"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_eip" "nat_eip" {
  vpc        = true
  depends_on = [aws_internet_gateway.ig]

  tags = {
    Name        = "${var.project}-nat"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_nat_gateway" "nat" {
  allocation_id = "${aws_eip.nat_eip.id}"
  subnet_id     = "${element(aws_subnet.public_subnet.*.id, 0)}"
  depends_on    = [aws_internet_gateway.ig]

  tags = {
    Name        = "${var.project}-nat"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_subnet" "public_subnet" {
  vpc_id                  = "${aws_vpc.vpc.id}"
  count                   = "${length(var.public_subnets_cidr)}"
  cidr_block              = "${element(var.public_subnets_cidr, count.index)}"
  availability_zone       = "${element(local.availability_zones, count.index)}"
  map_public_ip_on_launch = true

  tags = {
    Name        = "${var.project}-${element(local.availability_zones, count.index)}-public-subnet"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_subnet" "private_subnet" {
  vpc_id                  = "${aws_vpc.vpc.id}"
  count                   = "${length(var.private_subnets_cidr)}"
  cidr_block              = "${element(var.private_subnets_cidr, count.index)}"
  availability_zone       = "${element(local.availability_zones, count.index)}"
  map_public_ip_on_launch = false

  tags = {
    Name        = "${var.project}-${element(local.availability_zones, count.index)}-private-subnet"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_route_table" "private" {
  vpc_id = "${aws_vpc.vpc.id}"

  tags = {
    Name        = "${var.project}-private-route-table"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_route_table" "public" {
  vpc_id = "${aws_vpc.vpc.id}"

  tags = {
    Name        = "${var.project}-public-route-table"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_route" "public_internet_gateway" {
  route_table_id         = "${aws_route_table.public.id}"
  destination_cidr_block = "0.0.0.0/0"
  gateway_id             = "${aws_internet_gateway.ig.id}"
}

resource "aws_route" "private_nat_gateway" {
  route_table_id         = "${aws_route_table.private.id}"
  destination_cidr_block = "0.0.0.0/0"
  nat_gateway_id         = "${aws_nat_gateway.nat.id}"
}

resource "aws_route_table_association" "public" {
  count          = "${length(var.public_subnets_cidr)}"
  subnet_id      = "${element(aws_subnet.public_subnet.*.id, count.index)}"
  route_table_id = "${aws_route_table.public.id}"
}

resource "aws_route_table_association" "private" {
  count          = "${length(var.private_subnets_cidr)}"
  subnet_id      = "${element(aws_subnet.private_subnet.*.id, count.index)}"
  route_table_id = "${aws_route_table.private.id}"
}

# resource "aws_default_network_acl" "default_network_acl" {
#   default_network_acl_id = aws_vpc.vpc.default_network_acl_id
#   subnet_ids             = [aws_subnet.public_subnet.id, aws_subnet.private_subnet.id]

#   ingress {
#     protocol   = -1
#     rule_no    = 100
#     action     = "allow"
#     cidr_block = "0.0.0.0/0"
#     from_port  = 0
#     to_port    = 0
#   }

#   egress {
#     protocol   = -1
#     rule_no    = 100
#     action     = "allow"
#     cidr_block = "0.0.0.0/0"
#     from_port  = 0
#     to_port    = 0
#   }

#   tags = {
#     Name = "${var.project}-default-network-acl"
#   }
# }

resource "aws_security_group" "default" {
  name        = "${var.project}-default-sg"
  description = "Default security group to allow inbound/outbound from the VPC"
  vpc_id      = "${aws_vpc.vpc.id}"
  depends_on  = [aws_vpc.vpc]

  ingress {
    from_port = "0"
    to_port   = "0"
    protocol  = "-1"
    self      = true
  }

  egress {
    from_port = "0"
    to_port   = "0"
    protocol  = "-1"
    self      = "true"
  }

  tags = {
    Name        = "${var.project}-sg"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_s3_bucket" "lambda_bucket" {
  bucket = "${var.project}-${var.aws_account}-${var.aws_account_name}"

  force_destroy = true

  tags = {
    Name        = "${var.project}-bucket"
    Environment = "${var.aws_account_name}"
  }
}

data "archive_file" "payment_gateway_src" {
  type        = "zip"
  source_dir  = "${path.root}/../src/FrameworksAndDrivers"
  output_path = "${path.root}/../src/FrameworksAndDrivers/bin/Release/net6.0-windows/${var.src_zip_artifact}"
}

resource "aws_s3_bucket_object" "lambda_payment_gateway" {
  bucket = aws_s3_bucket.lambda_bucket.id
  key    = "${aws_s3_bucket.lambda_bucket.bucket}/FrameworksAndDrivers.zip"
  source = "${path.root}/../src/FrameworksAndDrivers/bin/Release/net6.0-windows/${var.src_zip_artifact}"
}

resource "aws_lambda_function" "lambda_payment_gateway" {
  function_name     = "PaymentGateway"
  s3_bucket         = aws_s3_bucket.lambda_bucket.id
  s3_key            = aws_s3_bucket_object.lambda_payment_gateway.key
  role              = aws_iam_role.iam_role.arn
  handler           = "FrameworksAndDrivers:FrameworksAndDrivers.LambdaEntryPoint::FunctionHandlerAsync"
  runtime           = "dotnet6"
  memory_size       = "256"
  timeout           = 120
  publish           = true

  vpc_config {
    subnet_ids         = [aws_subnet.private_subnet[0].id, aws_subnet.private_subnet[1].id] #todo: refactor tech debt
    security_group_ids = [aws_security_group.default.id]
  }

  tags = {
    Name        = "${var.project}-lambda"
    Environment = "${var.aws_account_name}"
  }
}

data "aws_iam_policy_document" "AWSLambdaTrustPolicy" {
  version = "2012-10-17"
  statement {
    actions = ["sts:AssumeRole"]
    effect  = "Allow"
    principals {
      type        = "Service"
      identifiers = ["lambda.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "iam_role" {
  assume_role_policy = data.aws_iam_policy_document.AWSLambdaTrustPolicy.json
  name               = "${var.project}-iam-role-lambda-trigger"
}

resource "aws_iam_role_policy_attachment" "iam_role_policy_attachment_lambda_vpc_access_execution" {
  role       = aws_iam_role.iam_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSLambdaVPCAccessExecutionRole"
}

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

resource "aws_apigatewayv2_route" "route" {
  api_id             = aws_apigatewayv2_api.api.id
  route_key          = "ANY /{proxy+}"
  target             = "integrations/${aws_apigatewayv2_integration.integration.id}"
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

resource "aws_lambda_permission" "api_gw_lambda" {
  statement_id  = "AllowExecutionFromAPIGateway"
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.lambda_payment_gateway.function_name
  principal     = "apigateway.amazonaws.com"

  # More: http://docs.aws.amazon.com/apigateway/latest/developerguide/api-gateway-control-access-using-iam-policies-to-invoke-api.html
  source_arn = "arn:aws:execute-api:${var.region}:${var.aws_account}:${aws_apigatewayv2_api.api.id}/*/${aws_apigatewayv2_integration.integration.integration_method}{/proxy+}"
}
