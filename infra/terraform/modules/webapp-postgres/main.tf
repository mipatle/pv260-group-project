locals {
  resource_group_name  = lower(replace("${var.name_prefix}-${var.environment_label}-rg", "_", "-"))
  web_app_name         = lower(replace("${var.name_prefix}-${var.environment_label}-app", "_", "-"))
  service_plan_name    = lower(replace("${var.name_prefix}-${var.environment_label}-plan", "_", "-"))
  postgres_server_name = lower(replace("${var.name_prefix}-${var.environment_label}-pg", "_", "-"))
  log_analytics_name   = lower(replace("${var.name_prefix}-${var.environment_label}-law", "_", "-"))
  app_insights_name    = lower(replace("${var.name_prefix}-${var.environment_label}-appi", "_", "-"))

  common_tags = merge(var.tags, {
    environment = var.environment_label
    service     = "arkfunds-web"
  })
}

resource "azurerm_resource_group" "this" {
  name     = local.resource_group_name
  location = var.location
  tags     = local.common_tags
}

resource "azurerm_log_analytics_workspace" "this" {
  name                = local.log_analytics_name
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
  tags                = local.common_tags
}

resource "azurerm_application_insights" "this" {
  name                = local.app_insights_name
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  workspace_id        = azurerm_log_analytics_workspace.this.id
  application_type    = "web"
  tags                = local.common_tags
}

resource "random_password" "postgres_admin" {
  length           = 24
  special          = true
  override_special = "_@-%+!"
}

resource "azurerm_postgresql_flexible_server" "this" {
  name                          = local.postgres_server_name
  location                      = azurerm_resource_group.this.location
  resource_group_name           = azurerm_resource_group.this.name
  version                       = var.postgres_version
  administrator_login           = var.postgres_admin_login
  administrator_password        = random_password.postgres_admin.result
  sku_name                      = var.postgres_sku_name
  storage_mb                    = var.postgres_storage_mb
  zone                          = var.postgres_zone
  backup_retention_days         = var.postgres_backup_retention_days
  public_network_access_enabled = true
  tags                          = local.common_tags
}

resource "azurerm_postgresql_flexible_server_database" "this" {
  name      = var.postgres_database_name
  server_id = azurerm_postgresql_flexible_server.this.id
  charset   = "UTF8"
  collation = "en_US.utf8"
}

resource "azurerm_postgresql_flexible_server_firewall_rule" "allow_azure_services" {
  name             = "allow-azure-services"
  server_id        = azurerm_postgresql_flexible_server.this.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_service_plan" "this" {
  name                = local.service_plan_name
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  os_type             = "Linux"
  sku_name            = var.app_service_sku_name
  tags                = local.common_tags
}

resource "azurerm_linux_web_app" "this" {
  name                          = local.web_app_name
  location                      = azurerm_resource_group.this.location
  resource_group_name           = azurerm_resource_group.this.name
  service_plan_id               = azurerm_service_plan.this.id
  https_only                    = true
  public_network_access_enabled = true
  tags                          = local.common_tags

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on           = true
    ftps_state          = "Disabled"
    minimum_tls_version = "1.2"
    health_check_path   = var.health_check_path

    application_stack {
      dotnet_version = "10.0"
    }
    # Health check eviction time (required by provider when health_check_path is set)
    health_check_eviction_time_in_min = var.health_check_eviction_time_in_min
  }

  app_settings = merge({
    ASPNETCORE_ENVIRONMENT                = var.app_environment
    WEBSITE_HEALTHCHECK_PATH              = var.health_check_path
    WEBSITE_WEBDEPLOY_USE_SCM             = "true"
    WEBSITE_RUN_FROM_PACKAGE              = "1"
    WEBSITES_ENABLE_APP_SERVICE_STORAGE   = "false"
    APPLICATIONINSIGHTS_CONNECTION_STRING = azurerm_application_insights.this.connection_string
    ConnectionStrings__Default            = local.postgres_connection_string
    Application__ArkUrl                   = var.ark_url
    AdminUser__Email                      = var.admin_user_email
    AdminUser__Password                   = var.admin_user_password
  },
  var.cron_fetch_expression != "" ? { "Application__CronFetchExpression" = var.cron_fetch_expression } : {})

  depends_on = [
    azurerm_postgresql_flexible_server_database.this,
    azurerm_postgresql_flexible_server_firewall_rule.allow_azure_services
  ]
}

locals {
  postgres_connection_string = "Host=${azurerm_postgresql_flexible_server.this.fqdn};Port=5432;Database=${azurerm_postgresql_flexible_server_database.this.name};Username=${azurerm_postgresql_flexible_server.this.administrator_login};Password=${random_password.postgres_admin.result};SSL Mode=Require;Trust Server Certificate=true"
}




