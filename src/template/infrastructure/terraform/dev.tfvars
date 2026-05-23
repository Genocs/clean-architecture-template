# Development Environment Configuration

environment  = "dev"
location     = "East US"
project_name = "genocs-ca"

# Tags
tags = {
  Environment = "Development"
  CostCenter  = "Engineering"
  Project     = "Clean Architecture"
}

# Container Registry
acr_sku          = "Basic"
acr_admin_enabled = true

# App Service Plan - Lower tier for dev
app_service_plan_sku = {
  tier = "Basic"
  size = "B1"
}

app_service_plan_kind = "Linux"

# App Services Configuration
app1_name          = "webapi"
app2_name          = "worker"
app1_docker_image  = "mcr.microsoft.com/appsvc/staticsite:latest"
app2_docker_image  = "mcr.microsoft.com/appsvc/staticsite:latest"
app1_port          = 8080
app2_port          = 8080

# Performance Settings - Lower for dev
always_on         = false
health_check_path = "/health"

# Application Insights
app_insights_retention_days   = 30
app_insights_disable_ip_masking = false

# Scaling - Minimal for dev
app1_min_instances = 1
app1_max_instances = 2
app2_min_instances = 1
app2_max_instances = 2

# Custom App Settings for Development
app1_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "true"
  Logging__LogLevel__Default = "Debug"
}

app2_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "true"
  Logging__LogLevel__Default = "Debug"
}
