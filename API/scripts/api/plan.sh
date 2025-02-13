. scripts/api/set-vars.sh $1 $2

terragrunt plan -input=false \
    -out=$PLAN_FILE_PATH \
    --terragrunt-non-interactive \
    --terragrunt-working-dir $TERRAGRUNT_DIRECTORY