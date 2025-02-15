locals {
    project_name = get_env("PROJECT_NAME")
    module_name = path_relative_to_include()
    environment_name = get_env("ENVIRONMENT_NAME")
}

inputs = {
    environment_name = local.environment_name
}

remote_state {
    backend = "s3"
    config = {
        bucket = "${local.project_name}-terraform-state-${local.environment_name}"
        key    = "${local.environment_name}/${local.module_name}/terraform.tfstate"
        region = "eu-west-1"
        dynamodb_table="${local.project_name}-terraform-state-lock"
        encrypt= true
    }
}