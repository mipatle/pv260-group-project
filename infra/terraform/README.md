# Terraform Azure Deployment

This folder contains Terraform for deploying the application to Azure App Service with a PostgreSQL Flexible Server backend.

## What it provisions

- One environment per Terraform run, selected by `environment` in `tfvars`
- Linux App Service plan and web app
- PostgreSQL Flexible Server and database
- Log Analytics workspace
- Application Insights resource

### Default settings

- Azure region: `germanywestcentral`
- App Service plan: `B1`
- PostgreSQL Flexible Server: `B_Standard_B1ms`
- PostgreSQL version: `18`

You can still override any of these in your `tfvars` file if you need a different environment-specific value.

## Quick start

```powershell
az login

Copy-Item .\terraform.tfvars.example .\terraform.tfvars -Force
terraform init
terraform workspace select default
terraform plan --var-file=terraform.tfvars
terraform apply --var-file=terraform.tfvars
```

If you prefer service principal auth, set the `ARM_*` environment variables before running Terraform.

## Important variables

- `environment` – `development` or `production`
- `name_prefix` – optional override; defaults to `pv260-arkfunds`
- `location` – optional override; defaults to `germanywestcentral`
- `app_service_sku_name` – optional override; defaults to `B1`
- `postgres_sku_name` – optional override; defaults to `B_Standard_B1ms`
- `postgres_version` – optional override; defaults to `18`
- `common_tags` – optional tags for all resources

Azure PostgreSQL Flexible Server always uses backups; this setup keeps the retention at the minimum supported value of 7 days.

### Environment-specific files

- `terraform.tfvars.example` – development template
- `terraform.prod.tfvars.example` – production template

The simplest workflow is:

1. Development (`default` workspace): copy `terraform.tfvars.example` to `terraform.tfvars`
2. Production (`production` workspace): copy `terraform.prod.tfvars.example` to `terraform.prod.tfvars`
3. Set `environment` in the matching file
4. Add only the overrides you actually need

Use one file per deployment and keep the state isolated per environment.

### Separate state for development and production

This repository uses Terraform workspaces to keep the dev and prod states separate.

- Use the `default` workspace for development
- Create and use a `production` workspace for production

Example:

```powershell
terraform workspace select default
terraform plan --var-file=terraform.tfvars
terraform apply --var-file=terraform.tfvars

terraform workspace new production
terraform workspace select production
terraform plan --var-file=terraform.prod.tfvars
terraform apply --var-file=terraform.prod.tfvars
```

If `production` already exists, skip `terraform workspace new production`.

Each workspace stores its own state, so production will plan as a new environment instead of replacing development resources.

## Deployment contract

The web app is configured with these settings:

- `ASPNETCORE_ENVIRONMENT`
- `ConnectionStrings__Default`
- `WEBSITE_HEALTHCHECK_PATH=/health`

The app's readiness endpoint is `/health`; it returns `200 OK` only when the database dependency is reachable.










