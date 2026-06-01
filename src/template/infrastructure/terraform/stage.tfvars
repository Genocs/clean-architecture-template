# Staging Environment Configuration

environment  = "stage"
location     = "East US"
project_name = "genocs-ca"

# Tags
tags = {
  Environment = "Staging"
  CostCenter  = "Engineering"
  Project     = "Clean Architecture"
}

# Container Registry
acr_sku          = "Standard"
acr_admin_enabled = true

# App Service Plan - Production-like for staging
app_service_plan_sku = {
  tier = "Premium"
  size = "P1v2"
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

# Application Insights
app_insights_retention_days   = 90
app_insights_disable_ip_masking = false

# Scaling - Production-like
app1_min_instances = 2
app1_max_instances = 5
app2_min_instances = 2
app2_max_instances = 5

# Custom App Settings for Staging
app1_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "false"
  Logging__LogLevel__Default = "Information"
}

app2_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "false"
  Logging__LogLevel__Default = "Information"
}
