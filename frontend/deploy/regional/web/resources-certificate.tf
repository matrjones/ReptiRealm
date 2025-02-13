resource "aws_acm_certificate" "cert" {
  domain_name               = "${var.environment}-images.traveltrekker.co.uk"
  validation_method         = "DNS"
  provider                  = aws.useast1
  subject_alternative_names = ["${var.environment}-images.traveltrekker.co.uk", "www.${var.environment}-images.traveltrekker.co.uk"]
}

resource "aws_acm_certificate" "certweb" {
  domain_name               = var.environment == "prod" ? "traveltrekker.co.uk" : "stage.traveltrekker.co.uk"
  validation_method         = "DNS"
  provider                  = aws.useast1
  subject_alternative_names = var.environment == "prod" ? ["www.traveltrekker.co.uk", "traveltrekker.co.uk"] : []
}
