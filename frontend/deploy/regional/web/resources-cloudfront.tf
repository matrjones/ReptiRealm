resource "aws_cloudfront_distribution" "cloudfront_distribution" {
  origin {
    domain_name              = aws_s3_bucket.static_images.bucket_regional_domain_name
    origin_access_control_id = aws_cloudfront_origin_access_control.static.id
    origin_id                = aws_s3_bucket.static_images.bucket_regional_domain_name
    connection_attempts      = 3
    connection_timeout       = 10
  }

  origin {
    domain_name = var.environment == "stage" ? "p5g54ukbfkk2itte6erdevxade0xxqhp.lambda-url.eu-west-1.on.aws" : "22mruypv23gdrtrihcr63dmpyi0qbtyb.lambda-url.eu-west-1.on.aws"
    origin_id   = "LambdaResizerOrigin"
    custom_origin_config {
      http_port              = 80
      https_port             = 443
      origin_protocol_policy = "https-only"
      origin_ssl_protocols   = ["TLSv1.2"]
    }
  }

  origin_group {
    origin_id = "default-to-s3"

    failover_criteria {
      status_codes = [403, 404, 500, 504, 503]
    }

    member {
      origin_id = aws_s3_bucket.static_images.bucket_regional_domain_name
    }

    member {
      origin_id = "LambdaResizerOrigin"
    }
  }

  default_cache_behavior {
    allowed_methods            = ["GET", "HEAD", "OPTIONS"]
    cached_methods             = ["GET", "HEAD", "OPTIONS"]
    target_origin_id           = "default-to-s3"
    compress                   = true
    cache_policy_id            = "658327ea-f89d-4fab-a63d-7e88639e58f6"
    origin_request_policy_id   = "88a5eaf4-2fd4-4709-b370-b4c650ea3fcf"
    response_headers_policy_id = "5cc3b908-e619-4b99-88e5-2cf7f45965bd"
    viewer_protocol_policy     = "redirect-to-https"
    function_association {
      event_type   = "viewer-request"
      function_arn = aws_cloudfront_function.image-optimiser-rewrite-url.arn
    }
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


  aliases          = var.environment == "prod" ? ["prod-images.traveltrekker.co.uk"] : ["stage-images.traveltrekker.co.uk"]
  is_ipv6_enabled  = true
  enabled          = true
  http_version     = "http2"
  price_class      = "PriceClass_100"
  retain_on_delete = false
}


resource "aws_cloudfront_origin_access_control" "static" {
  name                              = aws_s3_bucket.static_images.bucket_regional_domain_name
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

  aliases = var.environment == "prod" ? ["www.traveltrekker.co.uk", "traveltrekker.co.uk"] : ["stage.traveltrekker.co.uk"]

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
