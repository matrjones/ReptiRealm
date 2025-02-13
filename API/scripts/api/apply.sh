. scripts/api/set-vars.sh $1 $2

cp $PLAN_FILE_PATH $TERRAGRUNT_DIRECTORY

terragrunt apply -auto-approve \
    $PLAN_FILE_PATH \
    --terragrunt-non-interactive \
    --terragrunt-working-dir $TERRAGRUNT_DIRECTORY