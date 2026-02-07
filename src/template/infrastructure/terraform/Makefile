.PHONY: help init validate format plan apply destroy clean output login

# Default environment
ENV ?= dev

# Colors for output
BLUE := \033[0;34m
GREEN := \033[0;32m
YELLOW := \033[1;33m
RED := \033[0;31m
NC := \033[0m # No Color

help: ## Display this help message
	@echo "$(BLUE)Terraform Azure Infrastructure$(NC)"
	@echo ""
	@echo "$(GREEN)Usage:$(NC)"
	@echo "  make [target] ENV=<environment>"
	@echo ""
	@echo "$(GREEN)Environments:$(NC)"
	@echo "  dev, test, stage, prod (default: dev)"
	@echo ""
	@echo "$(GREEN)Targets:$(NC)"
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "  $(YELLOW)%-15s$(NC) %s\n", $$1, $$2}'

login: ## Login to Azure CLI
	@echo "$(BLUE)Logging in to Azure...$(NC)"
	@az login
	@az account list --output table

init: ## Initialize Terraform
	@echo "$(BLUE)Initializing Terraform...$(NC)"
	@terraform init
	@terraform version

validate: init ## Validate Terraform configuration
	@echo "$(BLUE)Validating Terraform configuration...$(NC)"
	@terraform validate

format: ## Format Terraform files
	@echo "$(BLUE)Formatting Terraform files...$(NC)"
	@terraform fmt -recursive

plan: validate format ## Create Terraform plan
	@echo "$(BLUE)Creating Terraform plan for $(ENV) environment...$(NC)"
	@terraform plan -var-file="$(ENV).tfvars" -out="$(ENV).tfplan"
	@echo "$(GREEN)Plan created: $(ENV).tfplan$(NC)"

apply: ## Apply Terraform plan
	@if [ -f "$(ENV).tfplan" ]; then \
		echo "$(YELLOW)Applying existing plan: $(ENV).tfplan$(NC)"; \
		terraform apply "$(ENV).tfplan"; \
		rm -f "$(ENV).tfplan"; \
	else \
		echo "$(YELLOW)No plan found. Creating and applying...$(NC)"; \
		terraform apply -var-file="$(ENV).tfvars"; \
	fi
	@echo "$(GREEN)Deployment complete!$(NC)"
	@make output

destroy: ## Destroy Terraform resources
	@echo "$(RED)WARNING: This will destroy all resources for $(ENV) environment!$(NC)"
	@read -p "Type 'destroy-$(ENV)' to confirm: " confirm; \
	if [ "$$confirm" = "destroy-$(ENV)" ]; then \
		terraform destroy -var-file="$(ENV).tfvars"; \
		echo "$(GREEN)Resources destroyed$(NC)"; \
	else \
		echo "$(YELLOW)Destroy cancelled$(NC)"; \
	fi

output: ## Show Terraform outputs
	@echo "$(BLUE)Terraform outputs for $(ENV) environment:$(NC)"
	@terraform output

output-json: ## Show Terraform outputs in JSON format
	@terraform output -json

clean: ## Clean up plan files
	@echo "$(BLUE)Cleaning up plan files...$(NC)"
	@rm -f *.tfplan
	@echo "$(GREEN)Clean complete$(NC)"

state-list: ## List resources in Terraform state
	@terraform state list

state-show: ## Show detailed state information (use RESOURCE=xxx)
	@terraform state show $(RESOURCE)

refresh: ## Refresh Terraform state
	@terraform refresh -var-file="$(ENV).tfvars"

graph: ## Generate dependency graph
	@terraform graph | dot -Tpng > graph.png
	@echo "$(GREEN)Graph generated: graph.png$(NC)"

cost-estimate: plan ## Estimate costs (requires Infracost)
	@if command -v infracost >/dev/null 2>&1; then \
		infracost breakdown --path $(ENV).tfplan; \
	else \
		echo "$(RED)Infracost not installed. Visit: https://www.infracost.io/docs/$(NC)"; \
	fi

# Environment-specific shortcuts
dev: ## Deploy to dev environment
	@$(MAKE) apply ENV=dev

test: ## Deploy to test environment
	@$(MAKE) apply ENV=test

stage: ## Deploy to stage environment
	@$(MAKE) apply ENV=stage

prod: ## Deploy to prod environment
	@$(MAKE) apply ENV=prod

# Quick commands
quick-plan: ## Quick plan without validation
	@terraform plan -var-file="$(ENV).tfvars"

quick-apply: ## Quick apply without plan file
	@terraform apply -var-file="$(ENV).tfvars" -auto-approve

# Utility commands
check-azure: ## Check Azure CLI login status
	@echo "$(BLUE)Checking Azure CLI status...$(NC)"
	@az account show --output table || echo "$(RED)Not logged in to Azure$(NC)"

install-tools: ## Install required tools (macOS/Linux)
	@echo "$(BLUE)Installing required tools...$(NC)"
	@if command -v brew >/dev/null 2>&1; then \
		brew install terraform azure-cli; \
	else \
		echo "$(YELLOW)Homebrew not found. Please install tools manually.$(NC)"; \
		echo "Terraform: https://www.terraform.io/downloads"; \
		echo "Azure CLI: https://docs.microsoft.com/cli/azure/install-azure-cli"; \
	fi
