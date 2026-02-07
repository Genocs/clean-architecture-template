# General Configuration
variable "environment" {
  description = "Environment name (dev, test, stage, prod)"
  type        = string
  validation {
    condition     = contains(["dev", "test", "stage", "prod"], var.environment)
    error_message = "Environment must be one of: dev, test, stage, prod."
  }
}

variable "location" {
  description = "Azure region for resources"
  type        = string
  default     = "East US"
}

variable "project_name" {
  description = "Project name used for resource naming"
  type        = string
  default     = "genocs-ca"
}

variable "tags" {
  description = "Common tags to be applied to all resources"
  type        = map(string)
  default     = {}
}

# Resource Group Configuration
variable "resource_group_name" {
  description = "Name of the resource group (optional, will be generated if not provided)"
  type        = string
  default     = ""
}

# Container Registry Configuration
variable "acr_sku" {
  description = "SKU for Azure Container Registry"
  type        = string
  default     = "Standard"
  validation {
    condition     = contains(["Basic", "Standard", "Premium"], var.acr_sku)
    error_message = "ACR SKU must be Basic, Standard, or Premium."
  }
}

variable "acr_admin_enabled" {
  description = "Enable admin user for ACR"
  type        = bool
  default     = true
}

# App Service Plan Configuration
variable "app_service_plan_sku" {
  description = "SKU for App Service Plan"
  type = object({
    tier = string
    size = string
  })
  default = {
    tier = "Standard"
    size = "S1"
  }
}

variable "app_service_plan_kind" {
  description = "Kind of App Service Plan (Linux or Windows)"
  type        = string
  default     = "Linux"
}

# App Service Configuration
variable "app1_name" {
  description = "Name for the first App Service"
  type        = string
  default     = "webapi"
}

variable "app2_name" {
  description = "Name for the second App Service"
  type        = string
  default     = "worker"
}

variable "app1_docker_image" {
  description = "Docker image for the first App Service"
  type        = string
  default     = "mcr.microsoft.com/appsvc/staticsite:latest"
}

variable "app2_docker_image" {
  description = "Docker image for the second App Service"
  type        = string
  default     = "mcr.microsoft.com/appsvc/staticsite:latest"
}

variable "app1_port" {
  description = "Port for the first App Service"
  type        = number
  default     = 8080
}

variable "app2_port" {
  description = "Port for the second App Service"
  type        = number
  default     = 8080
}

variable "always_on" {
  description = "Enable Always On for App Services"
  type        = bool
  default     = true
}

variable "health_check_path" {
  description = "Health check path for App Services"
  type        = string
  default     = "/health"
}

# Application Insights Configuration
variable "app_insights_retention_days" {
  description = "Retention period in days for Application Insights"
  type        = number
  default     = 90
}

variable "app_insights_disable_ip_masking" {
  description = "Disable IP masking in Application Insights"
  type        = bool
  default     = false
}

# Scaling Configuration
variable "app1_min_instances" {
  description = "Minimum number of instances for first App Service"
  type        = number
  default     = 1
}

variable "app1_max_instances" {
  description = "Maximum number of instances for first App Service"
  type        = number
  default     = 3
}

variable "app2_min_instances" {
  description = "Minimum number of instances for second App Service"
  type        = number
  default     = 1
}

variable "app2_max_instances" {
  description = "Maximum number of instances for second App Service"
  type        = number
  default     = 3
}

# Custom App Settings
variable "app1_custom_settings" {
  description = "Custom application settings for the first App Service"
  type        = map(string)
  default     = {}
}

variable "app2_custom_settings" {
  description = "Custom application settings for the second App Service"
  type        = map(string)
  default     = {}
}
