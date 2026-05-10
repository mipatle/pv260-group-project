# Deployment Guide

This guide shows how to deploy the application to Azure App Service with Terraform and GitHub Actions.

## Checklist

- [ ] Install `.NET 10`
- [ ] Install Terraform
- [ ] Have access to an Azure subscription
- [ ] Have write access to the GitHub repository
- [ ] Create the Terraform `tfvars` file
- [ ] Apply Terraform to create Azure resources
- [ ] Add GitHub secrets
- [ ] Push to GitHub to trigger CI/CD
- [ ] Verify the `/health` endpoint

## 1. Prerequisites

You need:

- .NET SDK from `global.json`
- Terraform installed locally
- Azure CLI installed and signed in with `az login`
- Azure subscription access
- GitHub repo admin or maintainer access
- Permission to create GitHub Environments and Secrets

## 2. Terraform infrastructure

Terraform is located in `infra/terraform/`.

This repository uses Terraform workspaces to keep dev and prod state separate:

- Use the `default` workspace for development
- Create and use a `production` workspace for production

Terraform provisions:

- Azure Resource Group
- Azure App Service Plan
- Azure Linux Web App
- Azure Database for PostgreSQL Flexible Server
- Log Analytics Workspace
- Application Insights

### Quick start

```powershell
az login
cd .\infra\terraform
Copy-Item terraform.tfvars.example terraform.tfvars
terraform init
terraform workspace select default
terraform plan --var-file=terraform.tfvars
terraform apply --var-file=terraform.tfvars
```

For production commands, see `Separate dev and prod state`.

### Optional overrides

You can override these in `tfvars` if needed:

- `name_prefix`
- `location`
- `app_service_sku_name`
- `postgres_sku_name`
- `postgres_database_name`
- `postgres_version`
- `common_tags`

Current defaults are:

- Region: `germanywestcentral`
- App Service plan: `B1`
- PostgreSQL Flexible Server: `B_Standard_B1ms`
- PostgreSQL version: `18`

### Separate dev and prod state

Use Terraform workspaces so production gets its own state and does not replace development resources.

```powershell
cd .\infra\terraform

# Development (default workspace)
terraform workspace select default
terraform plan --var-file=terraform.tfvars
terraform apply --var-file=terraform.tfvars

# Production (new workspace)
terraform workspace new production
terraform workspace select production
terraform plan --var-file=terraform.prod.tfvars
terraform apply --var-file=terraform.prod.tfvars
```

If the `production` workspace already exists, use `terraform workspace select production` instead of `new`.

## 3. Azure setup

If you are creating the Azure resources from scratch:

1. Choose the target workspace (`default` for development or `production` for production).
2. Create or edit the matching tfvars file (`terraform.tfvars` or `terraform.prod.tfvars`).
3. Run the Terraform commands above for that workspace.
4. Note the output values from Terraform:
   - web app URL
   - health URL
   - resource group name

The app is configured by Terraform with:

- `ASPNETCORE_ENVIRONMENT`
- `ConnectionStrings__Default`
- `WEBSITE_HEALTHCHECK_PATH=/health`

### GitHub Actions authentication (publish profile only)

This repository uses publish profiles for both CD workflows:

- `dev-cd.yml` uses `AZURE_WEBAPP_PUBLISH_PROFILE_DEV`
- `prod-cd.yml` uses `AZURE_WEBAPP_PUBLISH_PROFILE_PROD`

The App Service setting `WEBSITE_WEBDEPLOY_USE_SCM=true` is configured by Terraform.

### Download publish profiles

```powershell
az login

# Development
az webapp deployment list-publishing-profiles `
  --resource-group "<DEV_RESOURCE_GROUP>" `
  --name "<DEV_WEBAPP_NAME>" `
  --output xml | Out-File -Encoding utf8 dev-publish-profile.xml

# Production
az webapp deployment list-publishing-profiles `
  --resource-group "<PROD_RESOURCE_GROUP>" `
  --name "<PROD_WEBAPP_NAME>" `
  --output xml | Out-File -Encoding utf8 prod-publish-profile.xml
```

### Store GitHub secrets

```powershell
# Development repository secrets
gh secret set AZURE_WEBAPP_NAME_DEV --body "<DEV_WEBAPP_NAME>"
gh secret set AZURE_WEBAPP_PUBLISH_PROFILE_DEV --body (Get-Content -Raw .\dev-publish-profile.xml)

# Production environment secrets (environment name: production)
gh secret set AZURE_WEBAPP_NAME_PROD --env production --body "<PROD_WEBAPP_NAME>"
gh secret set AZURE_WEBAPP_PUBLISH_PROFILE_PROD --env production --body (Get-Content -Raw .\prod-publish-profile.xml)
```

## 4. GitHub repository setup

Follow the GitHub Actions guidance at: https://docs.github.com/en/actions/how-tos/deploy/deploy-to-third-party-platforms/net-to-azure-app-service

Create these GitHub secrets used by the workflows in this repo:

### Dev (repository secrets)
- `AZURE_WEBAPP_NAME_DEV` — app name string (e.g. `pv260-arkfunds-development-app`)
- `AZURE_WEBAPP_PUBLISH_PROFILE_DEV` — publish profile XML

### Prod (environment `production` secrets)
- `AZURE_WEBAPP_NAME_PROD` — app name string (e.g. `pv260-arkfunds-production-app`)
- `AZURE_WEBAPP_PUBLISH_PROFILE_PROD` — publish profile XML


Notes:
- Both `dev-cd.yml` and `prod-cd.yml` deploy with `azure/webapps-deploy@v2` using publish profile authentication.

## 5. GitHub Actions flow

### CI

`CI` runs on pull requests and pushes to `main` and `milestone-*` branches.
It builds and tests the solution.

### Development deployment

`dev-cd.yml` deploys after any successful CI run.
It publishes the app, uploads it to the development App Service, and checks `/health`.

### Production deployment

`prod-cd.yml` deploys on:

- pushes to `main`
- pushes to `milestone-*`
- tags matching `release-*`

It publishes the app, uploads it to the production App Service, and checks `/health`.

## 6. Verify the deployment

Open:

```text
https://<your-app-name>.azurewebsites.net/health
```

Expected behavior:

- `200 OK` when the database is reachable
- `503 Service Unavailable` when the database is not reachable

## 7. Useful notes

- Keep one Terraform workspace/state per environment.
- Do not run development and production in the same Terraform workspace.
- The database backups on Azure PostgreSQL Flexible Server are required; this setup uses the minimum supported retention.
- If you need more detail on Terraform values, see `infra/terraform/README.md`.


