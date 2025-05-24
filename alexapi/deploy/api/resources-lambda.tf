data "archive_file" "lambda" {
  type        = "zip"
  source_dir  = "${var.lambda_package_file_path}/"
  output_path = "./AlexAPI.zip"
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

resource "aws_iam_role" "alex-api_role" {
  name               = "alex-api"
  assume_role_policy = data.aws_iam_policy_document.assume_role.json
}

resource "aws_lambda_function" "alex-api" {
  filename         = "AlexAPI.zip"
  function_name    = "alex-api"
  role             = aws_iam_role.alex-api_role.arn
  handler          = "AlexAPI"
  source_code_hash = data.archive_file.lambda.output_base64sha256
  timeout          = 60
  runtime          = "dotnet8"
  depends_on       = [data.archive_file.lambda]
  environment {
    variables = {
      ASPNETCORE_ENVIRONMENT = "${var.environment_name == "stage" ? "Staging" : "Production"}"
    }
  }
}
resource "aws_lambda_function_url" "aws-lambda-net-api" {
  function_name      = aws_lambda_function.alex-api.function_name
  authorization_type = "NONE"
}
