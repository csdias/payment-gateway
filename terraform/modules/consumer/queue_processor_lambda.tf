resource "aws_lambda_function" "queue_processor" {
  function_name     = "QueueProcessor"
  s3_bucket         = aws_s3_bucket.consumer_bucket.id
  s3_key            = aws_s3_bucket_object.consumer_bucket.key
  source_code_hash  = data.archive_file.consumer_artifact_zip.output_base64sha256                                                                                                                                                

  role              = aws_iam_role.iam_role.arn
  handler           = "QueueProcessor::QueueProcessor.Function::FunctionHandler"
  runtime           = "dotnet6"
  memory_size       = "256"
  timeout           = 120
  publish           = true

  vpc_config {
    subnet_ids         = var.private_subnet_ids
    security_group_ids = var.security_group_ids
  }

  tags = {
    Name        = "${var.project}-queue-processor"
    Environment = "${var.aws_account_name}"
  }
}

# SQS trigger for message consumer lambda
resource "aws_lambda_event_source_mapping" "queue_processor" {
  event_source_arn = aws_sqs_queue.consumer_queue.arn
  function_name    = aws_lambda_function.queue_processor.arn
  batch_size       = 10
  enabled          = true
}