. backend/scripts/api/set-vars.sh $1 $2

terraform taint aws_rds_cluster.default --terragrunt-working-dir $TERRAGRUNT_DIRECTORY

terragrunt plan -input=false \
    -out=$PLAN_FILE_PATH \
    --terragrunt-non-interactive \
    --terragrunt-working-dir $TERRAGRUNT_DIRECTORY