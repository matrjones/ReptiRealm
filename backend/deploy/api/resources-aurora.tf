resource "aws_rds_cluster" "default" {
  cluster_identifier      = "repti-realm-${var.environment_name}"
  engine                  = "aurora-mysql"
  engine_version          = "8.0.mysql_aurora.3.04.0"
  availability_zones      = ["us-west-2a", "us-west-2b", "us-west-2c"]
  database_name           = "repti-realm-${var.environment_name}"
  master_username         = "repti-realm"
  master_password         = "okndwuhgf093"
  backup_retention_period = 5
  preferred_backup_window = "07:00-09:00"
}
