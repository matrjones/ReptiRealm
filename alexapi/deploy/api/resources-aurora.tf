# resource "aws_rds_cluster" "default" {
#   cluster_identifier = "reptirealm${var.environment_name}"
#   engine             = "aurora-mysql"
#   engine_version     = "8.0.mysql_aurora.3.04.0"
#   database_name      = "reptirealm${var.environment_name}"
#   master_username    = "reptirealm"
#   master_password    = "okndwuhgf093"

#   skip_final_snapshot = true
#   apply_immediately   = true

#   serverlessv2_scaling_configuration {
#     min_capacity = 0.25
#     max_capacity = 0.50
#   }
# }

# resource "aws_rds_cluster_instance" "serverless" {
#   identifier         = "reptirealm-instance-${var.environment_name}"
#   cluster_identifier = aws_rds_cluster.default.id
#   instance_class     = "db.serverless"
#   engine             = aws_rds_cluster.default.engine
#   engine_version     = aws_rds_cluster.default.engine_version

#   publicly_accessible = true
#   apply_immediately   = true
# }

# output "rds_cluster_endpoint" {
#   value = aws_rds_cluster.default.endpoint
# }

# resource "aws_ssm_parameter" "rds_connection_string" {
#   name  = "/ReptiRealm/rds_connection_string"
#   type  = "String"
#   value = "Server=${aws_rds_cluster.default.endpoint};Database=reptirealm${var.environment_name};User Id=reptirealm;Password=okndwuhgf093;"
# }
