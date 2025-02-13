resource "aws_s3_bucket" "static_images" {
  bucket = "repti-realm-images-${var.environment}"
}

resource "aws_s3_bucket_cors_configuration" "cors" {
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

resource "aws_s3_bucket_acl" "acl" {
  bucket = aws_s3_bucket.static_images.id
  acl    = "public-read"
}

resource "aws_s3_bucket_policy" "policy" {
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
                "arn:aws:s3:::${aws_s3_bucket.static_images.id}",
                "arn:aws:s3:::${aws_s3_bucket.static_images.id}/*"
            ]
        }
    ]
}
EOF
}
