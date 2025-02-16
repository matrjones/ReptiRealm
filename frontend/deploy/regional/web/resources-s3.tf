resource "aws_s3_bucket" "this" {
  bucket = "repti-realm-images-${var.environment}"
}

resource "aws_s3_bucket_acl" "this" {
  bucket = aws_s3_bucket.this.id
  acl    = "public-read"
}

resource "aws_s3_bucket_cors_configuration" "this" {
  bucket = aws_s3_bucket.this.id

  cors_rule {
    allowed_headers = ["*"]
    allowed_methods = [
      "PUT",
      "POST",
      "DELETE",
      "GET"
    ]
    allowed_origins = ["*"]
    expose_headers  = []
  }

}

resource "aws_s3_bucket_policy" "this" {
  bucket = aws_s3_bucket.this.id
  policy = <<EOF
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "Statement1",
            "Effect": "Allow",
            "Principal": "*",
            "Action": "s3:GetObject",
            "Resource": [
                "arn:aws:s3:::${aws_s3_bucket.this.id}",
                "arn:aws:s3:::${aws_s3_bucket.this.id}/*"
            ]
        }
    ]
}
EOF
}
