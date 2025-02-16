resource "aws_cloudfront_distribution" "cloudfront_distribution" {
  origin {
    domain_name              = aws_s3_bucket.this.bucket_regional_domain_name
    origin_access_control_id = aws_cloudfront_origin_access_control.static.id
    origin_id                = aws_s3_bucket.this.bucket_regional_domain_name
  }

  default_cache_behavior {
    allowed_methods            = ["GET", "HEAD", "OPTIONS"]
    cached_methods             = ["GET", "HEAD", "OPTIONS"]
    target_origin_id           = aws_s3_bucket.this.bucket_regional_domain_name
    compress                   = true
    cache_policy_id            = "658327ea-f89d-4fab-a63d-7e88639e58f6"
    origin_request_policy_id   = "88a5eaf4-2fd4-4709-b370-b4c650ea3fcf"
    response_headers_policy_id = "5cc3b908-e619-4b99-88e5-2cf7f45965bd"
    viewer_protocol_policy     = "redirect-to-https"
  }

  restrictions {
    geo_restriction {
      locations        = []
      restriction_type = "none"
    }
  }

  viewer_certificate {
    acm_certificate_arn      = aws_acm_certificate.cert.arn
    ssl_support_method       = "sni-only"
    minimum_protocol_version = "TLSv1.2_2021"
  }


  aliases          = var.environment == "prod" ? ["images-prod.pineappleexplorers.com"] : ["images-stage.pineappleexplorers.com"]
  is_ipv6_enabled  = true
  enabled          = true
  http_version     = "http2"
  price_class      = "PriceClass_100"
  retain_on_delete = false
}


resource "aws_cloudfront_origin_access_control" "static" {
  name                              = aws_s3_bucket.this.bucket_regional_domain_name
  origin_access_control_origin_type = "s3"
  signing_behavior                  = "always"
  signing_protocol                  = "sigv4"
}

resource "aws_cloudfront_distribution" "cloudfront_distribution_web" {
  origin {
    domain_name = replace(replace(aws_lambda_function_url.this.function_url, "https://", ""), "/", "")
    origin_id   = "api"

    custom_origin_config {
      http_port              = 80
      https_port             = 443
      origin_protocol_policy = "https-only"
      origin_ssl_protocols   = ["TLSv1.2"]
    }
  }


  enabled         = true
  is_ipv6_enabled = true

  aliases = var.environment == "prod" ? ["www.pineappleexplorers.com", "pineappleexplorers.com"] : ["stage.pineappleexplorers.com"]

  default_cache_behavior {
    allowed_methods          = ["DELETE", "GET", "HEAD", "OPTIONS", "PATCH", "POST", "PUT"]
    cached_methods           = ["GET", "HEAD"]
    target_origin_id         = "api"
    cache_policy_id          = "4135ea2d-6df8-44a3-9df3-4b5a84be39ad"
    origin_request_policy_id = "b689b0a8-53d0-40ab-baf2-68738e2966ac"

    viewer_protocol_policy = "allow-all"
    min_ttl                = 0
    default_ttl            = 3600
    max_ttl                = 86400
  }

  price_class = "PriceClass_100"

  restrictions {
    geo_restriction {
      restriction_type = "none"
    }
  }

  viewer_certificate {
    acm_certificate_arn            = aws_acm_certificate.certweb.arn
    ssl_support_method             = "sni-only"
    cloudfront_default_certificate = false
  }
}
