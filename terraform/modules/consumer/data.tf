data "aws_caller_identity" "current" {}

data "aws_iam_policy_document" "consumer_queue_assume_role" {
  statement {
    actions = ["sts:AssumeRole"]
    effect  = "Allow"
    principals {
      type        = "Service"
      identifiers = ["lambda.amazonaws.com"]
    }
  }
}

data "aws_iam_policy_document" "consumer_queue_policy" {
  statement {
    effect = "Allow"
    actions = [
      "sqs:ReceiveMessage",
      "sqs:ChangeMessageVisibility",
      "sqs:GetQueueUrl",
      "sqs:DeleteMessage",
      "sqs:GetQueueAttributes"
    ]
    resources = [
      aws_sqs_queue.consumer_queue.arn,
    ]
  }
}

data "archive_file" "consumer_artifact_zip" {
  type        = "zip"
  source_dir  = "${path.root}/../src/QueueProcessor"
  output_path = "${path.root}/../src/QueueProcessor//bin/Release/net6.0/${var.consumer_artifact_zip}"
}
