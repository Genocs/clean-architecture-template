#!/bin/bash

# Terraform Deployment Script
# Usage: ./deploy.sh <environment> [action]
# Example: ./deploy.sh dev plan
# Example: ./deploy.sh prod apply

set -e

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to print colored output
print_info() {
    echo -e "${GREEN}[INFO]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Check if environment is provided
if [ -z "$1" ]; then
    print_error "Environment not specified"
    echo "Usage: $0 <environment> [action]"
    echo "Environments: dev, test, stage, prod"
    echo "Actions: plan, apply, destroy (default: plan)"
    exit 1
fi

ENVIRONMENT=$1
ACTION=${2:-plan}

# Validate environment
if [[ ! "$ENVIRONMENT" =~ ^(dev|test|stage|prod)$ ]]; then
    print_error "Invalid environment: $ENVIRONMENT"
    echo "Valid environments: dev, test, stage, prod"
    exit 1
fi

# Validate action
if [[ ! "$ACTION" =~ ^(plan|apply|destroy|output)$ ]]; then
    print_error "Invalid action: $ACTION"
    echo "Valid actions: plan, apply, destroy, output"
    exit 1
fi

TFVARS_FILE="${ENVIRONMENT}.tfvars"

# Check if tfvars file exists
if [ ! -f "$TFVARS_FILE" ]; then
    print_error "Configuration file not found: $TFVARS_FILE"
    exit 1
fi

print_info "Starting Terraform $ACTION for $ENVIRONMENT environment"

# Check if Terraform is installed
if ! command -v terraform &> /dev/null; then
    print_error "Terraform is not installed"
    exit 1
fi

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    print_error "Azure CLI is not installed"
    exit 1
fi

# Check if logged in to Azure
print_info "Checking Azure login status..."
if ! az account show &> /dev/null; then
    print_error "Not logged in to Azure. Please run 'az login'"
    exit 1
fi

SUBSCRIPTION=$(az account show --query name -o tsv)
print_info "Using Azure subscription: $SUBSCRIPTION"

# Initialize Terraform if needed
if [ ! -d ".terraform" ]; then
    print_info "Initializing Terraform..."
    terraform init
fi

# Format and validate
print_info "Formatting Terraform files..."
terraform fmt

print_info "Validating Terraform configuration..."
terraform validate

# Execute Terraform action
case $ACTION in
    plan)
        print_info "Creating Terraform plan..."
        terraform plan -var-file="$TFVARS_FILE" -out="${ENVIRONMENT}.tfplan"
        print_info "Plan saved to ${ENVIRONMENT}.tfplan"
        ;;
    apply)
        if [ -f "${ENVIRONMENT}.tfplan" ]; then
            print_warning "Applying existing plan: ${ENVIRONMENT}.tfplan"
            read -p "Continue? (yes/no): " confirm
            if [ "$confirm" = "yes" ]; then
                terraform apply "${ENVIRONMENT}.tfplan"
                rm -f "${ENVIRONMENT}.tfplan"
            else
                print_info "Apply cancelled"
                exit 0
            fi
        else
            print_warning "Applying changes for $ENVIRONMENT environment"
            terraform plan -var-file="$TFVARS_FILE"
            read -p "Continue with apply? (yes/no): " confirm
            if [ "$confirm" = "yes" ]; then
                terraform apply -var-file="$TFVARS_FILE"
            else
                print_info "Apply cancelled"
                exit 0
            fi
        fi
        print_info "Deployment summary:"
        terraform output deployment_summary
        ;;
    destroy)
        print_warning "This will DESTROY all resources for $ENVIRONMENT environment"
        read -p "Are you absolutely sure? Type 'destroy-$ENVIRONMENT' to confirm: " confirm
        if [ "$confirm" = "destroy-$ENVIRONMENT" ]; then
            terraform destroy -var-file="$TFVARS_FILE"
            print_info "Resources destroyed"
        else
            print_info "Destroy cancelled"
            exit 0
        fi
        ;;
    output)
        terraform output
        ;;
esac

print_info "Terraform $ACTION completed successfully for $ENVIRONMENT environment"
