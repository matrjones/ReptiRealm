data "archive_file" "function_package" {
  type        = "zip"
  source_dir  = var.lambda_package_file_path
  output_path = "repti-realm-web.zip"
}

resource "aws_lambda_function" "this" {
  filename         = data.archive_file.function_package.output_path
  function_name    = "${var.environment}-repti-realm-web"
  role             = aws_iam_role.this.arn
  handler          = "run.sh"
  runtime          = "nodejs18.x"
  architectures    = ["x86_64"]
  layers           = ["arn:aws:lambda:eu-west-1:753240598075:layer:LambdaAdapterLayerX86:7"]
  memory_size      = 3008
  timeout          = 30
  source_code_hash = data.archive_file.function_package.output_base64sha256
  environment {
    variables = {
      "AWS_LAMBDA_EXEC_WRAPPER" = "/opt/bootstrap",
      "RUST_LOG"                = "info",
      "PORT"                    = "8000",
      "NODE_ENV"                = "staging"
    }
  }

  lifecycle {
    create_before_destroy = true
  }
}
resource "aws_lambda_function_url" "this" {
  function_name      = aws_lambda_function.this.function_name
  authorization_type = "NONE"
}

