variable "name_prefix" {
  description = "Prefix used for all Azure resource names."
  type        = string
  default     = "pv260-arkfunds"
}

variable "environment" {
  description = "Target environment to deploy to. Use development or production."
  type        = string

  validation {
    condition     = contains(["development", "production"], lower(var.environment))
    error_message = "environment must be development or production."
  }
}

variable "location" {
  description = "Azure region for the deployment."
  type        = string
  default     = "francecentral"
}

variable "common_tags" {
  description = "Tags applied to all Azure resources."
  type        = map(string)
  default     = {}
}

variable "app_service_sku_name" {
  description = "App Service plan SKU for the selected environment."
  type        = string
  default     = "B1"
}

variable "postgres_sku_name" {
  description = "PostgreSQL Flexible Server SKU for the selected environment."
  type        = string
  default     = "B_Standard_B1ms"
}

variable "postgres_database_name" {
  description = "Database name for the selected environment."
  type        = string
  default     = "arkfunds"
}

variable "postgres_admin_login" {
  description = "Administrator login name used for the PostgreSQL flexible servers."
  type        = string
  default     = "arkfundsadmin"
}

variable "postgres_version" {
  description = "PostgreSQL major version for the flexible servers."
  type        = string
  default     = "18"
}

variable "postgres_storage_mb" {
  description = "Storage size in MB for PostgreSQL flexible servers."
  type        = number
  default     = 32768
}

variable "postgres_backup_retention_days" {
  description = "Backup retention in days for PostgreSQL flexible servers. Azure Flexible Server requires backups, and 7 is the minimum supported retention."
  type        = number
  default     = 7
}

variable "postgres_zone" {
  description = "(Optional) Availability zone for PostgreSQL flexible server. Set to the existing server zone (e.g. \"1\") to avoid Terraform attempting to change/remove the zone."
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
