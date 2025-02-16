resource "aws_s3_bucket" "static_images" {
  bucket = "repti-realm-images-${var.environment}"
}

resource "aws_s3_bucket_public_access_block" "this" {
  bucket = aws_s3_bucket.static_images.id

  block_public_acls       = false
  block_public_policy     = false
  ignore_public_acls      = false
  restrict_public_buckets = false
}

resource "aws_s3_bucket_ownership_controls" "this" {
  bucket = aws_s3_bucket.static_images.id
  rule {
    object_ownership = "ObjectWriter"
  }
}

resource "aws_s3_bucket_acl" "this" {
  bucket     = aws_s3_bucket.static_images.id
  acl        = "public-read"
  depends_on = [aws_s3_bucket_public_access_block.this]
}

resource "aws_s3_bucket_cors_configuration" "this" {
  bucket = aws_s3_bucket.static_images.id

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
  bucket = aws_s3_bucket.static_images.id
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
