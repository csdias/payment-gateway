# Subscribe to SNS topic containing the outbox messages we want to consume
resource "aws_sns_topic_subscription" "consumer_queue" {
  topic_arn            = aws_sns_topic.payment_order_topic.arn
  protocol             = "sqs"
  filter_policy        = jsonencode({ MessageName = "${var.sns_filter_policy}" })
  endpoint             = aws_sqs_queue.consumer_queue.arn
  raw_message_delivery = true
}

resource "aws_sns_topic" "payment_order_topic" {
  name = "${var.project}-payment-order-topic"
}
