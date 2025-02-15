. backend/scripts/api/set-vars.sh

terragrunt run-all destroy \
    -auto-approve \
    --terragrunt-non-interactive \
    --terragrunt-working-dir $TERRAGRUNT_DIRECTORY