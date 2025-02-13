data "archive_file" "lambda" {
  type        = "zip"
  source_dir  = "${var.lambda_package_file_path}/"
  output_path = "./ReptiRealm.zip"
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

resource "aws_iam_role" "repti-realm_role" {
  name               = "repti-realm-api"
  assume_role_policy = data.aws_iam_policy_document.assume_role.json
}

resource "aws_lambda_function" "repti-realm-api" {
  filename         = "ReptiRealm.zip"
  function_name    = "repti-realm-api"
  role             = aws_iam_role.repti-realm_role.arn
  handler          = "ReptiRealm"
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
  function_name      = aws_lambda_function.repti-realm-api.function_name
  authorization_type = "NONE"
}
