data "archive_file" "lambda_zip" {
  type        = "zip"
  source_dir  = "${path.root}/../src/FrameworksAndDrivers"
  output_path = "${path.root}/../src/FrameworksAndDrivers/bin/Release/net6.0/${var.src_zip_artifact}"
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
