data "archive_file" "lambda" {
  type        = "zip"
  source_dir  = "${var.lambda_package_file_path}/"
  output_path = "./repti-realm.zip"
}

data "aws_iam_policy_document" "assume_role" {
  statement {
    effect = "Allow"
    principals {
      type        = "Service"
      identifiers = ["lambda.amazonaws.com"]
    }
    actions = ["sts:AssumeRole"]
  }
}

resource "aws_iam_role" "aws-lambda-net-api_role" {
  name               = "aws-lambda-net-api"
  assume_role_policy = data.aws_iam_policy_document.assume_role.json
}

resource "aws_lambda_function" "aws-lambda-net-api" {
  filename         = "aws-lambda-net-api.zip"
  function_name    = "aws-lambda-net-api"
  role             = aws_iam_role.aws-lambda-net-api_role.arn
  handler          = "aws-lambda-net-api"
  source_code_hash = data.archive_file.lambda.output_base64sha256
  runtime          = "dotnet8"
  depends_on       = [data.archive_file.lambda]
  environment {
    variables = {
      ASPNETCORE_ENVIRONMENT = "Development"
    }
  }
}
resource "aws_lambda_function_url" "aws-lambda-net-api" {
  function_name      = aws_lambda_function.aws-lambda-net-api.function_name
  authorization_type = "NONE"
}
