# Backend configuration for Terraform state management
# This uses Azure Storage Account for remote state storage
# Uncomment and configure this block after creating the backend storage

# terraform {
#   backend "azurerm" {
#     resource_group_name  = "rg-terraform-state"
#     storage_account_name = "sttfstate<unique-suffix>"
#     container_name       = "tfstate"
#     key                  = "genocs-ca-${var.environment}.tfstate"
#   }
# }

# Instructions for setting up the backend:
# 1. Create a storage account for Terraform state:
#    az group create --name rg-terraform-state --location "East US"
#    az storage account create --name sttfstate<unique-suffix> --resource-group rg-terraform-state --location "East US" --sku Standard_LRS
#    az storage container create --name tfstate --account-name sttfstate<unique-suffix>
#
# 2. Uncomment the backend block above and replace <unique-suffix> with your unique identifier
#
# 3. Initialize Terraform with the backend:
#    terraform init -backend-config="key=genocs-ca-dev.tfstate"
#
# 4. For each environment, use a different key:
#    - dev:   genocs-ca-dev.tfstate
#    - test:  genocs-ca-test.tfstate
#    - stage: genocs-ca-stage.tfstate
#    - prod:  genocs-ca-prod.tfstate

# Alternative: Use local backend for development (default if backend block is commented)
# State files will be stored locally in terraform.tfstate
