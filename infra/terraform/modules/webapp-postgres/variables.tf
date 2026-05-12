variable "name_prefix" {
  description = "Prefix used for Azure resource names."
  type        = string
}

variable "environment_label" {
  description = "Environment name used in resource names and tags."
  type        = string
}

variable "app_environment" {
  description = "ASPNETCORE_ENVIRONMENT value written to the App Service."
  type        = string
}

variable "location" {
  description = "Azure region where the environment is deployed."
  type        = string
}

variable "tags" {
  description = "Tags to apply to all resources in the environment."
  type        = map(string)
  default     = {}
}

variable "app_service_sku_name" {
  description = "App Service plan SKU."
  type        = string
}

variable "postgres_sku_name" {
  description = "PostgreSQL Flexible Server SKU."
  type        = string
}

variable "postgres_database_name" {
  description = "Database name created on the PostgreSQL server."
  type        = string
}

variable "postgres_admin_login" {
  description = "Administrator login name for PostgreSQL."
  type        = string
}

variable "postgres_version" {
  description = "PostgreSQL major version."
  type        = string
}

variable "postgres_storage_mb" {
  description = "Storage size in MB for PostgreSQL."
  type        = number
}

variable "postgres_backup_retention_days" {
  description = "Backup retention in days for PostgreSQL."
  type        = number
}

variable "health_check_path" {
  description = "Path used by the App Service health check."
  type        = string
  default     = "/health"
}

variable "health_check_eviction_time_in_min" {
  description = "Time in minutes after which App Service health check is considered evicted/unresponsive. Required when health_check_path is set."
  type        = number
  default     = 5
}

variable "postgres_zone" {
  description = "(Optional) Availability zone for PostgreSQL flexible server. Set to the existing server zone (e.g. \"1\") to avoid zone drift when adopting resources into state."
  type        = string
  default     = null
}

variable "admin_user_email" {
  description = "Email for the default admin user."
  type        = string
  sensitive   = true
}

variable "admin_user_password" {
  description = "Password for the default admin user."
  type        = string
  sensitive   = true
}
