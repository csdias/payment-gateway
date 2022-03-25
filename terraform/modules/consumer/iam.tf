resource "aws_iam_role" "iam_role" {
  assume_role_policy = data.aws_iam_policy_document.consumer_queue_assume_role.json
  name               = "${var.project}-iam-role-consumer-queue"
}

resource "aws_iam_role_policy" "consumer_queue" {
  name   = "${var.project}-iam-role-policy-consumer-queue"
  role   = aws_iam_role.iam_role.name
  policy = data.aws_iam_policy_document.consumer_queue_policy.json
}

resource "aws_iam_role_policy_attachment" "iam_role_policy_attachment_lambda_vpc_access_execution" {
  role       = aws_iam_role.iam_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSLambdaVPCAccessExecutionRole"
}