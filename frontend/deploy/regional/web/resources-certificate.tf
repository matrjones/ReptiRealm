resource "aws_acm_certificate" "cert" {
  domain_name               = "images-${var.environment}.pineappleexplorers.com"
  validation_method         = "DNS"
  provider                  = aws.useast1
  subject_alternative_names = ["images-${var.environment}.pineappleexplorers.com", "www.images-${var.environment}.pineappleexplorers.com"]
}

resource "aws_acm_certificate" "certweb" {
  domain_name               = var.environment == "prod" ? "pineappleexplorers.com" : "stage.pineappleexplorers.com"
  validation_method         = "DNS"
  provider                  = aws.useast1
  subject_alternative_names = var.environment == "prod" ? ["www.pineappleexplorers.com", "pineappleexplorers.com"] : []
}
