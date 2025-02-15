
. backend/scripts/api/set-vars.sh $1 $2

terragrunt run-all destroy \
    -auto-approve \
    --terragrunt-non-interactive \
    --terragrunt-working-dir $TERRAGRUNT_DIRECTORY