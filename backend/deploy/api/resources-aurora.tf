resource "aws_rds_cluster" "default" {
  cluster_identifier      = "reptirealm${var.environment_name}"
  engine                  = "aurora-mysql"
  engine_version          = "8.0.mysql_aurora.3.04.0"
  database_name           = "reptirealm${var.environment_name}"
  master_username         = "reptirealm"
  master_password         = "okndwuhgf093"
  backup_retention_period = 5
  preferred_backup_window = "07:00-09:00"
}
