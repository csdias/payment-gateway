resource "aws_s3_bucket" "consumer_bucket" {
  bucket = "${var.project}-${var.aws_account}-${var.aws_account_name}-consumer"

  force_destroy = true

  tags = {
    Name        = "${var.project}-bucket"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_s3_bucket_object" "consumer_bucket" {
  bucket = aws_s3_bucket.consumer_bucket.id
  key    = "${aws_s3_bucket.consumer_bucket.bucket}/QueueProcessor.zip"
  source = "${path.root}/../src/QueueProcessor/${var.consumer_artifact_zip}"
}