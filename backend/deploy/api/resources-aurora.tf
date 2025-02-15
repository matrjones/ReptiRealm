resource "aws_rds_cluster" "default" {
  cluster_identifier = "reptirealm${var.environment_name}"
  engine             = "aurora-mysql"
  engine_version     = "8.0.mysql_aurora.3.04.0"
  database_name      = "reptirealm${var.environment_name}"
  master_username    = "reptirealm"
  master_password    = "okndwuhgf093"

  vpc_security_group_ids    = [aws_security_group.rds_sg.id]
  db_subnet_group_name      = aws_db_subnet_group.my_db_subnet_group.name
  skip_final_snapshot       = false
  final_snapshot_identifier = "reptirealm-${get_env("ENVIRONMENT_NAME")}-final"
}

output "rds_cluster_endpoint" {
  value = aws_rds_cluster.default.endpoint
}

resource "aws_ssm_parameter" "rds_connection_string" {
  name  = "/ReptiRealm/rds_connection_string"
  type  = "String"
  value = "Server=${aws_rds_cluster.default.endpoint};Database=reptirealm${var.environment_name};User Id=reptirealm;Password=okndwuhgf093;"
}

resource "aws_db_subnet_group" "my_db_subnet_group" {
  name       = "reptrealm-db-subnet-group-${var.environment_name}"
  subnet_ids = [aws_subnet.subnet_a.id, aws_subnet.subnet_b.id]

  tags = {
    Name = "Subnet group for ReptiRealm RDS ${var.environment_name}"
  }
}
