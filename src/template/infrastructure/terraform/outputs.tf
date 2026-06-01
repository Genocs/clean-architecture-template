# Resource Group Outputs
output "resource_group_name" {
  description = "Name of the created resource group"
  value       = azurerm_resource_group.main.name
}

output "resource_group_location" {
  description = "Location of the resource group"
  value       = azurerm_resource_group.main.location
}

output "resource_group_id" {
  description = "ID of the resource group"
  value       = azurerm_resource_group.main.id
}

# Container Registry Outputs
output "acr_name" {
  description = "Name of the Azure Container Registry"
  value       = azurerm_container_registry.main.name
}

output "acr_login_server" {
  description = "Login server URL for ACR"
  value       = azurerm_container_registry.main.login_server
}

output "acr_admin_username" {
  description = "Admin username for ACR"
  value       = azurerm_container_registry.main.admin_username
  sensitive   = true
}

output "acr_admin_password" {
  description = "Admin password for ACR"
  value       = azurerm_container_registry.main.admin_password
  sensitive   = true
}

output "acr_id" {
  description = "ID of the Azure Container Registry"
  value       = azurerm_container_registry.main.id
}

# App Service Plan Outputs
output "app_service_plan_name" {
  description = "Name of the App Service Plan"
  value       = azurerm_service_plan.main.name
}

output "app_service_plan_id" {
  description = "ID of the App Service Plan"
  value       = azurerm_service_plan.main.id
}

# App Service 1 Outputs
output "app1_name" {
  description = "Name of the first App Service"
  value       = azurerm_linux_web_app.app1.name
}

output "app1_default_hostname" {
  description = "Default hostname of the first App Service"
  value       = azurerm_linux_web_app.app1.default_hostname
}

output "app1_url" {
  description = "URL of the first App Service"
  value       = "https://${azurerm_linux_web_app.app1.default_hostname}"
}

output "app1_id" {
  description = "ID of the first App Service"
  value       = azurerm_linux_web_app.app1.id
}

output "app1_principal_id" {
  description = "Principal ID of the first App Service managed identity"
  value       = azurerm_linux_web_app.app1.identity[0].principal_id
}

output "app1_outbound_ip_addresses" {
  description = "Outbound IP addresses of the first App Service"
  value       = azurerm_linux_web_app.app1.outbound_ip_addresses
}

# App Service 2 Outputs
output "app2_name" {
  description = "Name of the second App Service"
  value       = azurerm_linux_web_app.app2.name
}

output "app2_default_hostname" {
  description = "Default hostname of the second App Service"
  value       = azurerm_linux_web_app.app2.default_hostname
}

output "app2_url" {
  description = "URL of the second App Service"
  value       = "https://${azurerm_linux_web_app.app2.default_hostname}"
}

output "app2_id" {
  description = "ID of the second App Service"
  value       = azurerm_linux_web_app.app2.id
}

output "app2_principal_id" {
  description = "Principal ID of the second App Service managed identity"
  value       = azurerm_linux_web_app.app2.identity[0].principal_id
}

output "app2_outbound_ip_addresses" {
  description = "Outbound IP addresses of the second App Service"
  value       = azurerm_linux_web_app.app2.outbound_ip_addresses
}

# Application Insights Outputs
output "app_insights_name" {
  description = "Name of Application Insights"
  value       = azurerm_application_insights.main.name
}

output "app_insights_instrumentation_key" {
  description = "Instrumentation key for Application Insights"
  value       = azurerm_application_insights.main.instrumentation_key
  sensitive   = true
}

output "app_insights_connection_string" {
  description = "Connection string for Application Insights"
  value       = azurerm_application_insights.main.connection_string
  sensitive   = true
}

output "app_insights_app_id" {
  description = "App ID of Application Insights"
  value       = azurerm_application_insights.main.app_id
}

# Log Analytics Workspace Outputs
output "log_analytics_workspace_name" {
  description = "Name of the Log Analytics Workspace"
  value       = azurerm_log_analytics_workspace.main.name
}

output "log_analytics_workspace_id" {
  description = "ID of the Log Analytics Workspace"
  value       = azurerm_log_analytics_workspace.main.id
}

# Summary Output
output "deployment_summary" {
  description = "Summary of the deployment"
  value = {
    environment          = var.environment
    resource_group       = azurerm_resource_group.main.name
    location             = azurerm_resource_group.main.location
    acr_name             = azurerm_container_registry.main.name
    acr_login_server     = azurerm_container_registry.main.login_server
    app_service_plan     = azurerm_service_plan.main.name
    app1_name            = azurerm_linux_web_app.app1.name
    app1_url             = "https://${azurerm_linux_web_app.app1.default_hostname}"
    app2_name            = azurerm_linux_web_app.app2.name
    app2_url             = "https://${azurerm_linux_web_app.app2.default_hostname}"
    app_insights_name    = azurerm_application_insights.main.name
  }
}
