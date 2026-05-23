# Test Environment Configuration

environment  = "test"
location     = "East US"
project_name = "genocs-ca"

# Tags
tags = {
  Environment = "Test"
  CostCenter  = "Engineering"
  Project     = "Clean Architecture"
}

# Container Registry
acr_sku          = "Standard"
acr_admin_enabled = true

# App Service Plan
app_service_plan_sku = {
  tier = "Standard"
  size = "S1"
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
app_insights_retention_days   = 60
app_insights_disable_ip_masking = false

# Scaling
app1_min_instances = 1
app1_max_instances = 3
app2_min_instances = 1
app2_max_instances = 3

# Custom App Settings for Test
app1_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "true"
  Logging__LogLevel__Default = "Information"
}

app2_custom_settings = {
  ASPNETCORE_DETAILEDERRORS = "true"
  Logging__LogLevel__Default = "Information"
}
