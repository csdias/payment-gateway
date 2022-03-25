resource "aws_sqs_queue" "consumer_queue" {
  name                       = "${var.project}-consumer-queue"
  visibility_timeout_seconds = 300
  redrive_policy = jsonencode({
    deadLetterTargetArn = aws_sqs_queue.consumer_queue_error.arn
    maxReceiveCount     = 3
  })
}

resource "aws_sqs_queue" "consumer_queue_error" {
  name                      = "${var.project}-consumer-queue-error"
  receive_wait_time_seconds = 10
}

resource "aws_sqs_queue_policy" "consumer_queue_policy" {
  queue_url = aws_sqs_queue.consumer_queue.id
  policy    = data.aws_iam_policy_document.consumer_queue_policy.json
}
