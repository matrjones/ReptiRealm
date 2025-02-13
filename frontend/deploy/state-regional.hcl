locals {
    account_id = get_env("ACCOUNT_ID")
    project_name = get_env("PROJECT_NAME")
    environment_name = get_env("ENVIRONMENT_NAME")
    region_name = get_env("AWS_DEFAULT_REGION")
    module_name = path_relative_to_include()
}


remote_state {
    backend = "s3"
    config = {
        bucket = "${local.project_name}-terraform-state-${local.environment_name}"
        key    = "${local.account_id}/${local.environment_name}/${local.module_name}/${local.region_name}/terraform.tfstate"
        dynamodb_table="${local.project_name}-terraform-state-lock"
        region = "eu-west-1"
        encrypt= true
    }
}