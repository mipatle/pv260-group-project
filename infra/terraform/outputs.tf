output "web_app_url" {
  description = "Default hostname for the deployed web app."
  value       = module.deployment.web_app_url
}

output "health_url" {
  description = "Health endpoint for the deployed environment."
  value       = module.deployment.health_url
}

output "resource_group_name" {
  description = "Resource group created for the deployed environment."
  value       = module.deployment.resource_group_name
}


