. backend/scripts/api/set-vars.sh $1 $2


terragrunt run-all refresh

terragrunt state rm aws_rds_cluster.default

terragrunt plan -input=false \
    -out=$PLAN_FILE_PATH \
    --terragrunt-non-interactive \
    --terragrunt-working-dir $TERRAGRUNT_DIRECTORY