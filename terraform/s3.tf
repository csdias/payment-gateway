resource "aws_s3_bucket" "lambda_bucket" {
  bucket = "${var.project}-${var.aws_account}-${var.aws_account_name}"

  force_destroy = true

  tags = {
    Name        = "${var.project}-bucket"
    Environment = "${var.aws_account_name}"
  }
}

resource "aws_s3_bucket_object" "lambda_payment_gateway" {
  bucket = aws_s3_bucket.lambda_bucket.id
  key    = "${aws_s3_bucket.lambda_bucket.bucket}/FrameworksAndDrivers.zip"
  source = "${path.root}/../src/FrameworksAndDrivers/bin/Release/net6.0-windows/${var.src_zip_artifact}"
}