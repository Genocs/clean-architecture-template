# Azure Terraform Infrastructure

This Terraform configuration deploys a complete Azure infrastructure for hosting two Docker containers with Application Insights monitoring across multiple environments (Dev, Test, Stage, and Prod).

## Architecture Overview

The solution provisions the following Azure resources:

- **Resource Group**: Container for all Azure resources
- **Azure Container Registry (ACR)**: Private Docker registry for container images
- **App Service Plan**: Compute resources for hosting web applications
- **App Service (Web API)**: First containerized application
- **App Service (Worker)**: Second containerized application
- **Application Insights**: Application performance monitoring
- **Log Analytics Workspace**: Centralized logging and analytics
- **Auto-scaling**: Automatic scaling based on CPU metrics
- **Managed Identities**: System-assigned identities for secure ACR access

## Prerequisites

1. **Azure Subscription**: Active Azure subscription with appropriate permissions
2. **Azure CLI**: Install from [https://docs.microsoft.com/cli/azure/install-azure-cli](https://docs.microsoft.com/cli/azure/install-azure-cli)
3. **Terraform**: Version >= 1.5.0. Install from [https://www.terraform.io/downloads](https://www.terraform.io/downloads)
4. **Docker Images**: Prepared container images to deploy (or use default placeholder images)

## Getting Started

### 1. Authenticate with Azure

```bash
az login
az account set --subscription "<your-subscription-id>"
```

### 2. Initialize Terraform

```bash
# Navigate to the terraform directory
cd src/template/infrastructure/terraform

# Initialize Terraform (downloads required providers)
terraform init
```

### 3. Review and Customize Configuration

Edit the environment-specific `.tfvars` files to match your requirements:

- [dev.tfvars](dev.tfvars) - Development environment
- [test.tfvars](test.tfvars) - Test environment
- [stage.tfvars](stage.tfvars) - Staging environment
- [prod.tfvars](prod.tfvars) - Production environment

### 4. Validate Configuration

```bash
# Validate the Terraform configuration
terraform validate

# Format Terraform files
terraform fmt
```

### 5. Plan Deployment

```bash
# Preview changes for a specific environment (e.g., dev)
terraform plan -var-file="dev.tfvars"
```

### 6. Deploy Infrastructure

```bash
# Deploy to development environment
terraform apply -var-file="dev.tfvars"

# Or deploy to other environments
terraform apply -var-file="test.tfvars"
terraform apply -var-file="stage.tfvars"
terraform apply -var-file="prod.tfvars"
```

### 7. View Outputs

```bash
# Display deployment outputs
terraform output

# Get specific output values
terraform output app1_url
terraform output app2_url
terraform output acr_login_server
```

## Environment Configuration

### Development (dev.tfvars)

- **App Service Plan**: Basic B1
- **ACR SKU**: Basic
- **Scaling**: 1-2 instances
- **Always On**: Disabled
- **Retention**: 30 days
- **Cost**: ~$15-30/month

### Test (test.tfvars)

- **App Service Plan**: Standard S1
- **ACR SKU**: Standard
- **Scaling**: 1-3 instances
- **Always On**: Enabled
- **Retention**: 60 days
- **Cost**: ~$75-150/month

### Staging (stage.tfvars)

- **App Service Plan**: Premium P1v2
- **ACR SKU**: Standard
- **Scaling**: 2-5 instances
- **Always On**: Enabled
- **Retention**: 90 days
- **Cost**: ~$150-300/month

### Production (prod.tfvars)

- **App Service Plan**: Premium P2v3
- **ACR SKU**: Premium
- **Scaling**: 3-10 instances
- **Always On**: Enabled
- **Retention**: 90 days
- **ACR Admin**: Disabled (uses managed identities)
- **Cost**: ~$300-800/month

## Deploying Custom Docker Images

### 1. Build and Tag Your Images

```bash
# Build your images
docker build -t myapp-webapi:latest ./src/WebApi
docker build -t myapp-worker:latest ./src/Worker
```

### 2. Push to Azure Container Registry

```bash
# Get ACR login server from Terraform output
ACR_LOGIN_SERVER=$(terraform output -raw acr_login_server)

# Login to ACR
az acr login --name $(terraform output -raw acr_name)

# Tag images for ACR
docker tag myapp-webapi:latest $ACR_LOGIN_SERVER/myapp-webapi:latest
docker tag myapp-worker:latest $ACR_LOGIN_SERVER/myapp-worker:latest

# Push images to ACR
docker push $ACR_LOGIN_SERVER/myapp-webapi:latest
docker push $ACR_LOGIN_SERVER/myapp-worker:latest
```

### 3. Update Terraform Variables

Edit your `.tfvars` file:

```hcl
app1_docker_image = "your-acr-name.azurecr.io/myapp-webapi:latest"
app2_docker_image = "your-acr-name.azurecr.io/myapp-worker:latest"
```

### 4. Apply Changes

```bash
terraform apply -var-file="dev.tfvars"
```

## Remote State Management (Recommended)

For team collaboration and production use, configure remote state storage:

### 1. Create Backend Storage

```bash
# Variables
RESOURCE_GROUP_NAME="rg-terraform-state"
STORAGE_ACCOUNT_NAME="sttfstate$(openssl rand -hex 4)"
CONTAINER_NAME="tfstate"
LOCATION="eastus"

# Create resource group
az group create --name $RESOURCE_GROUP_NAME --location $LOCATION

# Create storage account
az storage account create \
  --name $STORAGE_ACCOUNT_NAME \
  --resource-group $RESOURCE_GROUP_NAME \
  --location $LOCATION \
  --sku Standard_LRS \
  --encryption-services blob

# Create container
az storage container create \
  --name $CONTAINER_NAME \
  --account-name $STORAGE_ACCOUNT_NAME

echo "Storage Account Name: $STORAGE_ACCOUNT_NAME"
```

### 2. Configure Backend

Edit [backend.tf](backend.tf) and uncomment the backend block:

```hcl
terraform {
  backend "azurerm" {
    resource_group_name  = "rg-terraform-state"
    storage_account_name = "sttfstate<your-suffix>"
    container_name       = "tfstate"
    key                  = "genocs-ca-dev.tfstate"
  }
}
```

### 3. Initialize with Backend

```bash
# For dev environment
terraform init -backend-config="key=genocs-ca-dev.tfstate"

# For other environments
terraform init -backend-config="key=genocs-ca-test.tfstate"
terraform init -backend-config="key=genocs-ca-stage.tfstate"
terraform init -backend-config="key=genocs-ca-prod.tfstate"
```

## Managing Multiple Environments

### Option 1: Using Workspaces

```bash
# Create workspaces
terraform workspace new dev
terraform workspace new test
terraform workspace new stage
terraform workspace new prod

# Switch between workspaces
terraform workspace select dev
terraform apply -var-file="dev.tfvars"

terraform workspace select prod
terraform apply -var-file="prod.tfvars"
```

### Option 2: Using Separate Directories

```bash
# Create environment-specific directories
mkdir -p environments/{dev,test,stage,prod}

# Copy configuration to each directory
for env in dev test stage prod; do
  cp *.tf environments/$env/
  cp ${env}.tfvars environments/$env/terraform.tfvars
done

# Deploy from specific directory
cd environments/dev
terraform init
terraform apply
```

## Monitoring and Logging

### Application Insights

Access Application Insights from Azure Portal or use the URL from outputs:

```bash
terraform output app_insights_name
```

### View App Service Logs

```bash
# Get App Service name
APP1_NAME=$(terraform output -raw app1_name)

# Stream logs
az webapp log tail --name $APP1_NAME --resource-group $(terraform output -raw resource_group_name)

# Download logs
az webapp log download --name $APP1_NAME --resource-group $(terraform output -raw resource_group_name)
```

### Application Insights Query

```kusto
# Query application requests
requests
| where timestamp > ago(1h)
| summarize count() by bin(timestamp, 5m), resultCode
| render timechart

# Query exceptions
exceptions
| where timestamp > ago(24h)
| summarize count() by type, outerMessage
| order by count_ desc
```

## Security Best Practices

1. **Managed Identities**: The solution uses System-Assigned Managed Identities for ACR access
2. **HTTPS Only**: All App Services enforce HTTPS
3. **TLS 1.2+**: Minimum TLS version is set to 1.2
4. **Admin Disabled**: ACR admin is disabled in production (use managed identities)
5. **Secrets Management**: Use Azure Key Vault for sensitive configuration (not included, can be added)
6. **Network Security**: Consider adding VNet integration and Private Endpoints for production

## Scaling Configuration

Auto-scaling rules are configured based on CPU percentage:

- **Scale Out**: When CPU > 70% for 5 minutes
- **Scale In**: When CPU < 30% for 5 minutes
- **Cooldown**: 5 minutes between scaling operations

Modify scaling thresholds in [main.tf](main.tf) as needed.

## Cost Optimization

| Environment | Estimated Monthly Cost |
|-------------|------------------------|
| Dev         | $15-30                 |
| Test        | $75-150                |
| Stage       | $150-300               |
| Prod        | $300-800               |

### Cost Reduction Tips

1. **Dev Environment**: Use Basic tier, disable Always On, reduce retention
2. **Deallocate Non-Production**: Stop dev/test environments after hours
3. **Reserved Instances**: Purchase reserved instances for production
4. **Right-Sizing**: Monitor and adjust App Service Plan SKUs based on actual usage

## Troubleshooting

### Issue: Container fails to start

```bash
# Check App Service logs
az webapp log tail --name <app-name> --resource-group <rg-name>

# Verify Docker image
az acr repository show --name <acr-name> --image <image-name:tag>
```

### Issue: Cannot pull from ACR

```bash
# Verify managed identity has AcrPull role
az role assignment list --assignee <principal-id> --all

# Manually assign role if needed
az role assignment create \
  --role "AcrPull" \
  --assignee <principal-id> \
  --scope <acr-id>
```

### Issue: High costs

```bash
# Check App Service Plan metrics
az monitor metrics list \
  --resource <app-service-plan-id> \
  --metric "CpuPercentage,MemoryPercentage"

# Review scaling events
az monitor autoscale-settings list \
  --resource-group <rg-name>
```

## Cleaning Up

### Destroy Specific Environment

```bash
# Destroy dev environment
terraform destroy -var-file="dev.tfvars"
```

### Destroy All Resources

```bash
# WARNING: This will delete ALL resources
terraform destroy -var-file="dev.tfvars"
terraform destroy -var-file="test.tfvars"
terraform destroy -var-file="stage.tfvars"
terraform destroy -var-file="prod.tfvars"
```

## CI/CD Integration

### GitHub Actions Example

```yaml
name: Deploy Infrastructure

on:
  push:
    branches: [main]
    paths:
      - 'src/template/infrastructure/terraform/**'

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      
      - name: Setup Terraform
        uses: hashicorp/setup-terraform@v2
        with:
          terraform_version: 1.5.0
      
      - name: Azure Login
        uses: azure/login@v1
        with:
          creds: ${{ secrets.AZURE_CREDENTIALS }}
      
      - name: Terraform Init
        run: terraform init
        working-directory: ./src/template/infrastructure/terraform
      
      - name: Terraform Plan
        run: terraform plan -var-file="dev.tfvars"
        working-directory: ./src/template/infrastructure/terraform
      
      - name: Terraform Apply
        run: terraform apply -var-file="dev.tfvars" -auto-approve
        working-directory: ./src/template/infrastructure/terraform
```

### Azure DevOps Pipeline Example

```yaml
trigger:
  branches:
    include:
      - main
  paths:
    include:
      - src/template/infrastructure/terraform/*

pool:
  vmImage: 'ubuntu-latest'

variables:
  - group: terraform-vars

stages:
  - stage: Plan
    jobs:
      - job: TerraformPlan
        steps:
          - task: TerraformInstaller@0
            inputs:
              terraformVersion: '1.5.0'
          
          - task: TerraformTaskV2@2
            inputs:
              provider: 'azurerm'
              command: 'init'
              workingDirectory: '$(System.DefaultWorkingDirectory)/src/template/infrastructure/terraform'
          
          - task: TerraformTaskV2@2
            inputs:
              provider: 'azurerm'
              command: 'plan'
              commandOptions: '-var-file="dev.tfvars"'
              workingDirectory: '$(System.DefaultWorkingDirectory)/src/template/infrastructure/terraform'
  
  - stage: Apply
    dependsOn: Plan
    condition: succeeded()
    jobs:
      - deployment: TerraformApply
        environment: 'dev'
        strategy:
          runOnce:
            deploy:
              steps:
                - task: TerraformTaskV2@2
                  inputs:
                    provider: 'azurerm'
                    command: 'apply'
                    commandOptions: '-var-file="dev.tfvars" -auto-approve'
                    workingDirectory: '$(System.DefaultWorkingDirectory)/src/template/infrastructure/terraform'
```

## Additional Resources

- [Azure App Service Documentation](https://docs.microsoft.com/azure/app-service/)
- [Azure Container Registry Documentation](https://docs.microsoft.com/azure/container-registry/)
- [Application Insights Documentation](https://docs.microsoft.com/azure/azure-monitor/app/app-insights-overview)
- [Terraform Azure Provider](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)

## Variables Reference

| Variable | Description | Default | Required |
|----------|-------------|---------|----------|
| environment | Environment name (dev, test, stage, prod) | - | Yes |
| location | Azure region | East US | No |
| project_name | Project name for resource naming | genocs-ca | No |
| acr_sku | ACR SKU (Basic, Standard, Premium) | Standard | No |
| app_service_plan_sku | App Service Plan SKU | {tier="Standard", size="S1"} | No |
| app1_docker_image | Docker image for first app | mcr.microsoft.com/appsvc/staticsite:latest | No |
| app2_docker_image | Docker image for second app | mcr.microsoft.com/appsvc/staticsite:latest | No |
| app1_min_instances | Min instances for app1 | 1 | No |
| app1_max_instances | Max instances for app1 | 3 | No |

For a complete list of variables, see [variables.tf](variables.tf).

## Outputs Reference

| Output | Description |
|--------|-------------|
| resource_group_name | Name of the resource group |
| acr_login_server | ACR login server URL |
| app1_url | URL of the first App Service |
| app2_url | URL of the second App Service |
| app_insights_connection_string | Application Insights connection string |

For a complete list of outputs, see [outputs.tf](outputs.tf).

## Support

For issues or questions:
1. Check the [Troubleshooting](#troubleshooting) section
2. Review Terraform logs: `TF_LOG=DEBUG terraform apply`
3. Open an issue in the repository

## License

This infrastructure code is part of the Genocs Clean Architecture Template.
