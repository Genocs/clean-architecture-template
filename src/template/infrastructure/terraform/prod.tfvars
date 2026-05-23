# Production Environment Configuration

environment  = "prod"
location     = "East US"
project_name = "genocs-ca"

# Tags
tags = {
  Environment = "Production"
  CostCenter  = "Operations"
  Project     = "Clean Architecture"
  Criticality = "High"
}

# Container Registry - Premium for production
acr_sku          = "Premium"
acr_admin_enabled = false  # Disabled for better security, use managed identities

# App Service Plan - Premium for production
app_service_plan_sku = {
  tier = "Premium"
  size = "P2v3"
}

app_service_plan_kind = "Linux"

# App Services Configuration
app1_name          = "webapi"
app2_name          = "worker"
app1_docker_image  = "mcr.microsoft.com/appsvc/staticsite:latest"
app2_docker_image  = "mcr.microsoft.com/appsvc/staticsite:latest"
app1_port          = 8080
app2_port          = 8080

# Performance Settings
always_on         = true
health_check_path = "/health"

# Application Insights - Longer retention for production
app_insights_retention_days   = 90
app_insights_disable_ip_masking = false

# Scaling - Higher for production
app1_min_instances = 3
app1_max_instances = 10
app2_min_instances = 2
app2_max_instances = 8

# Custom App Settings for Production
app1_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "false"
  Logging__LogLevel__Default = "Warning"
  Logging__LogLevel__Microsoft = "Warning"
  Logging__LogLevel__System = "Warning"
}

app2_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "false"
  Logging__LogLevel__Default = "Warning"
  Logging__LogLevel__Microsoft = "Warning"
  Logging__LogLevel__System = "Warning"
}
