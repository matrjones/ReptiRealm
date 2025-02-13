resource "aws_cloudfront_function" "image-optimiser-rewrite-url" {
  name    = "image-optimiser-rewrite-url-${var.environment}"
  runtime = "cloudfront-js-1.0"
  publish = true
  code    = file("${path.module}/image-optimiser-rewrite-url.js")
}
