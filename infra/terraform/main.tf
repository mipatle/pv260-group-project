locals {
  environment_label = lower(var.environment)
  app_environment   = local.environment_label == "development" ? "Development" : "Production"

  tags = merge(var.common_tags, {
    environment = local.environment_label
    workload    = "arkfunds-web"
  })
}

module "deployment" {
  source = "./modules/webapp-postgres"

  name_prefix                    = var.name_prefix
  environment_label              = local.environment_label
  app_environment                = local.app_environment
  location                       = var.location
  tags                           = local.tags
  app_service_sku_name           = var.app_service_sku_name
  postgres_sku_name              = var.postgres_sku_name
  postgres_database_name         = var.postgres_database_name
  postgres_admin_login           = var.postgres_admin_login
  postgres_version               = var.postgres_version
  postgres_storage_mb            = var.postgres_storage_mb
  postgres_backup_retention_days = var.postgres_backup_retention_days
  postgres_zone                  = var.postgres_zone
  admin_user_email               = var.admin_user_email
  admin_user_password            = var.admin_user_password
  cron_fetch_expression          = var.cron_fetch_expression
  ark_url                        = var.ark_url
}


