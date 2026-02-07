# Quick Start Guide

This guide will help you deploy the infrastructure in under 10 minutes.

## Prerequisites Check

```bash
# Check Terraform
terraform version

# Check Azure CLI
az version

# Login to Azure
az login
```

## 5-Minute Deployment

### Step 1: Navigate to terraform directory

```bash
cd src/template/infrastructure/terraform
```

### Step 2: Initialize Terraform

```bash
terraform init
```

### Step 3: Deploy to Development

```bash
# Review the plan
terraform plan -var-file="dev.tfvars"

# Apply the changes
terraform apply -var-file="dev.tfvars"
```

### Step 4: Get your URLs

```bash
# View all outputs
terraform output

# Get App URLs
terraform output app1_url
terraform output app2_url
```

## Using Helper Scripts

### Linux/macOS

```bash
# Make script executable
chmod +x deploy.sh

# Deploy to dev
./deploy.sh dev plan
./deploy.sh dev apply

# Deploy to production
./deploy.sh prod plan
./deploy.sh prod apply
```

### Windows

```cmd
REM Deploy to dev
deploy.bat dev plan
deploy.bat dev apply

REM Deploy to production
deploy.bat prod plan
deploy.bat prod apply
```

### Using Makefile

```bash
# View all available commands
make help

# Deploy to dev (plan + apply)
make dev

# Deploy to production
make prod

# Just create a plan
make plan ENV=dev

# View outputs
make output ENV=dev

# Destroy resources
make destroy ENV=dev
```

## Deploying Your Own Docker Images

### 1. Get ACR name

```bash
ACR_NAME=$(terraform output -raw acr_name)
ACR_LOGIN_SERVER=$(terraform output -raw acr_login_server)
```

### 2. Login to ACR

```bash
az acr login --name $ACR_NAME
```

### 3. Build and push images

```bash
# Build images
docker build -t myapp-webapi:v1.0 ./src/WebApi
docker build -t myapp-worker:v1.0 ./src/Worker

# Tag for ACR
docker tag myapp-webapi:v1.0 $ACR_LOGIN_SERVER/myapp-webapi:v1.0
docker tag myapp-worker:v1.0 $ACR_LOGIN_SERVER/myapp-worker:v1.0

# Push to ACR
docker push $ACR_LOGIN_SERVER/myapp-webapi:v1.0
docker push $ACR_LOGIN_SERVER/myapp-worker:v1.0
```

### 4. Update tfvars file

Edit `dev.tfvars`:

```hcl
app1_docker_image = "youracr.azurecr.io/myapp-webapi:v1.0"
app2_docker_image = "youracr.azurecr.io/myapp-worker:v1.0"
```

### 5. Apply changes

```bash
terraform apply -var-file="dev.tfvars"
```

## Common Tasks

### View Application Logs

```bash
# Get app name
APP_NAME=$(terraform output -raw app1_name)
RG_NAME=$(terraform output -raw resource_group_name)

# Stream logs
az webapp log tail --name $APP_NAME --resource-group $RG_NAME

# Download logs
az webapp log download --name $APP_NAME --resource-group $RG_NAME
```

### Restart App Service

```bash
az webapp restart --name $APP_NAME --resource-group $RG_NAME
```

### Scale App Service

```bash
# Scale up (change plan size)
az appservice plan update --name $(terraform output -raw app_service_plan_name) \
  --resource-group $RG_NAME --sku P2v3

# Scale out (change instance count)
az appservice plan update --name $(terraform output -raw app_service_plan_name) \
  --resource-group $RG_NAME --number-of-workers 5
```

### View Application Insights

```bash
# Get Application Insights name
APP_INSIGHTS=$(terraform output -raw app_insights_name)

# Open in browser (requires jq)
az monitor app-insights component show \
  --app $APP_INSIGHTS \
  --resource-group $RG_NAME \
  --query 'id' -o tsv | xargs -I {} \
  echo "https://portal.azure.com/#@/resource{}/overview"
```

## Cleanup

### Destroy all resources

```bash
# Using terraform directly
terraform destroy -var-file="dev.tfvars"

# Using helper script (Linux/macOS)
./deploy.sh dev destroy

# Using helper script (Windows)
deploy.bat dev destroy

# Using Makefile
make destroy ENV=dev
```

## Troubleshooting

### Issue: Terraform init fails

```bash
# Clear cache
rm -rf .terraform .terraform.lock.hcl

# Re-initialize
terraform init
```

### Issue: Azure authentication fails

```bash
# Login again
az login

# Set subscription
az account set --subscription "Your-Subscription-Name"

# Verify
az account show
```

### Issue: Container fails to start

```bash
# Check logs
APP_NAME=$(terraform output -raw app1_name)
RG_NAME=$(terraform output -raw resource_group_name)
az webapp log tail --name $APP_NAME --resource-group $RG_NAME

# Check container settings
az webapp config container show --name $APP_NAME --resource-group $RG_NAME
```

### Issue: Can't access ACR

```bash
# Verify ACR exists
az acr list --query "[].{Name:name, LoginServer:loginServer}" -o table

# Test ACR login
az acr login --name $(terraform output -raw acr_name)
```

## Next Steps

1. **Configure CI/CD**: Set up automated deployments using GitHub Actions or Azure DevOps
2. **Add Custom Domain**: Configure custom domains and SSL certificates
3. **Enable VNet Integration**: Add network security with Virtual Network integration
4. **Set up Monitoring**: Configure alerts and dashboards in Application Insights
5. **Implement Backup**: Set up backup policies for critical data
6. **Add Key Vault**: Store secrets securely in Azure Key Vault

## Cost Monitoring

```bash
# View resource group costs
az consumption usage list \
  --start-date $(date -d '30 days ago' +%Y-%m-%d) \
  --end-date $(date +%Y-%m-%d) \
  --query "[?resourceGroup=='$(terraform output -raw resource_group_name)']"

# Install Infracost for cost estimates
brew install infracost  # macOS
infracost breakdown --path .
```

## Getting Help

- Review the [full README](README.md) for detailed documentation
- Check the [troubleshooting section](README.md#troubleshooting)
- Review Terraform [variables](variables.tf) and [outputs](outputs.tf)
- Check Azure [App Service documentation](https://docs.microsoft.com/azure/app-service/)

## Resources Created

After deployment, you'll have:

✅ 1 Resource Group  
✅ 1 Azure Container Registry  
✅ 1 App Service Plan  
✅ 2 App Services (WebAPI + Worker)  
✅ 1 Application Insights  
✅ 1 Log Analytics Workspace  
✅ 2 Managed Identities  
✅ Auto-scaling configured  
✅ HTTPS enabled  
✅ Monitoring enabled  

**Estimated costs**: $15-30/month (dev), $300-800/month (prod)
