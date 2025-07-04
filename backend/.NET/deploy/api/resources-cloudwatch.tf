resource "aws_cloudwatch_log_group" "aws-lambda-net-api_logs" {
  name              = "/aws/lambda/repti-realm"
  retention_in_days = 7
}

data "aws_iam_policy_document" "log_policy_document" {
  statement {
    effect = "Allow"
    actions = [
      "logs:CreateLogGroup",
      "logs:CreateLogStream",
      "logs:PutLogEvents",
    ]
    resources = ["arn:aws:logs:*:*:*"]
  }
}

resource "aws_iam_policy" "log_policy" {
  name   = "log_policy"
  path   = "/"
  policy = data.aws_iam_policy_document.log_policy_document.json
}

resource "aws_iam_role_policy_attachment" "log_policy_attachment" {
  role       = aws_iam_role.repti-realm_role.name
  policy_arn = aws_iam_policy.log_policy.arn
}
