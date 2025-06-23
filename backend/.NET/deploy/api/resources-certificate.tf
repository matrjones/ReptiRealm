resource "aws_acm_certificate" "cert" {
  domain_name       = "api-${var.environment_name}.pineappleexplorers.com"
  validation_method = "DNS"
  provider          = aws.useast1
}
