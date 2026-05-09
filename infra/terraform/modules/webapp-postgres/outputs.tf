output "resource_group_name" {
  description = "Resource group name for the environment."
  value       = azurerm_resource_group.this.name
}

output "web_app_name" {
  description = "App Service name."
  value       = azurerm_linux_web_app.this.name
}

output "web_app_url" {
  description = "Default hostname for the App Service."
  value       = "https://${azurerm_linux_web_app.this.default_hostname}"
}

output "health_url" {
  description = "Health endpoint for the App Service."
  value       = "https://${azurerm_linux_web_app.this.default_hostname}${var.health_check_path}"
}

output "postgres_fqdn" {
  description = "Hostname for the PostgreSQL server."
  value       = azurerm_postgresql_flexible_server.this.fqdn
}

output "postgres_connection_string" {
  description = "Connection string for the application."
  value       = local.postgres_connection_string
  sensitive   = true
}

output "application_insights_connection_string" {
  description = "Application Insights connection string."
  value       = azurerm_application_insights.this.connection_string
  sensitive   = true
}

