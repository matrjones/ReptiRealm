#!/bin/bash -e

echo "Setting deployment variables"

export AWS_REGION=eu-west-1
export PROJECT_NAME="alex-api"
export ENVIRONMENT_NAME=$1
export TERRAGRUNT_DIRECTORY="alexapi/deploy/api"
export PLAN_FILE_PATH="/tmp/alex-api.tfplan"
export TF_VAR_lambda_package_file_path="$PWD/alexapi/ReptiRealm/publish"
export TF_VAR_environment_name=$1
export ENVIRONMENT_NAME=$1