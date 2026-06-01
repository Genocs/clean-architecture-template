# Generate a random suffix for unique naming
resource "random_id" "suffix" {
  byte_length = 4
}

# Local values for common configurations
locals {
  resource_suffix = lower("${var.project_name}-${var.environment}-${random_id.suffix.hex}")
  resource_group_name = var.resource_group_name != "" ? var.resource_group_name : "rg-${local.resource_suffix}"
  acr_name = lower(replace("acr${var.project_name}${var.environment}${random_id.suffix.hex}", "-", ""))
  
  common_tags = merge(
    var.tags,
    {
      Environment = var.environment
      Project     = var.project_name
      ManagedBy   = "Terraform"
      CreatedDate = timestamp()
    }
  )
  
  # App Service names
  app1_full_name = "app-${var.app1_name}-${local.resource_suffix}"
  app2_full_name = "app-${var.app2_name}-${local.resource_suffix}"
}

# Resource Group
resource "azurerm_resource_group" "main" {
  name     = local.resource_group_name
  location = var.location
  tags     = local.common_tags
}

# Log Analytics Workspace (required for Application Insights)
resource "azurerm_log_analytics_workspace" "main" {
  name                = "log-${local.resource_suffix}"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  sku                 = "PerGB2018"
  retention_in_days   = var.app_insights_retention_days
  tags                = local.common_tags
}

# Application Insights
resource "azurerm_application_insights" "main" {
  name                = "appi-${local.resource_suffix}"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  workspace_id        = azurerm_log_analytics_workspace.main.id
  application_type    = "web"
  retention_in_days   = var.app_insights_retention_days
  disable_ip_masking  = var.app_insights_disable_ip_masking
  tags                = local.common_tags
}

# Azure Container Registry
resource "azurerm_container_registry" "main" {
  name                = local.acr_name
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  sku                 = var.acr_sku
  admin_enabled       = var.acr_admin_enabled
  
  tags = local.common_tags
}

# App Service Plan
resource "azurerm_service_plan" "main" {
  name                = "asp-${local.resource_suffix}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  os_type             = var.app_service_plan_kind
  sku_name            = "${var.app_service_plan_sku.tier}_${var.app_service_plan_sku.size}"
  
  tags = local.common_tags
}

# First App Service (e.g., WebAPI)
resource "azurerm_linux_web_app" "app1" {
  name                = local.app1_full_name
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_service_plan.main.location
  service_plan_id     = azurerm_service_plan.main.id
  
  https_only = true
  
  site_config {
    always_on              = var.always_on
    health_check_path      = var.health_check_path
    ftps_state            = "FtpsOnly"
    http2_enabled         = true
    minimum_tls_version   = "1.2"
    
    application_stack {
      docker_image     = split(":", var.app1_docker_image)[0]
      docker_image_tag = length(split(":", var.app1_docker_image)) > 1 ? split(":", var.app1_docker_image)[1] : "latest"
    }
    
    # Enable health check with retries
    health_check_eviction_time_in_min = 5
  }
  
  app_settings = merge(
    {
      APPLICATIONINSIGHTS_CONNECTION_STRING      = azurerm_application_insights.main.connection_string
      ApplicationInsightsAgent_EXTENSION_VERSION = "~3"
      DOCKER_REGISTRY_SERVER_URL                 = "https://${azurerm_container_registry.main.login_server}"
      DOCKER_REGISTRY_SERVER_USERNAME            = azurerm_container_registry.main.admin_username
      DOCKER_REGISTRY_SERVER_PASSWORD            = azurerm_container_registry.main.admin_password
      WEBSITES_ENABLE_APP_SERVICE_STORAGE        = "false"
      WEBSITES_PORT                              = tostring(var.app1_port)
      ASPNETCORE_ENVIRONMENT                     = title(var.environment)
      ENVIRONMENT                                = var.environment
    },
    var.app1_custom_settings
  )
  
  identity {
    type = "SystemAssigned"
  }
  
  logs {
    detailed_error_messages = true
    failed_request_tracing  = true
    
    http_logs {
      file_system {
        retention_in_days = 7
        retention_in_mb   = 35
      }
    }
    
    application_logs {
      file_system_level = "Information"
    }
  }
  
  tags = merge(
    local.common_tags,
    {
      AppService = var.app1_name
    }
  )
}

# Second App Service (e.g., Worker)
resource "azurerm_linux_web_app" "app2" {
  name                = local.app2_full_name
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_service_plan.main.location
  service_plan_id     = azurerm_service_plan.main.id
  
  https_only = true
  
  site_config {
    always_on              = var.always_on
    health_check_path      = var.health_check_path
    ftps_state            = "FtpsOnly"
    http2_enabled         = true
    minimum_tls_version   = "1.2"
    
    application_stack {
      docker_image     = split(":", var.app2_docker_image)[0]
      docker_image_tag = length(split(":", var.app2_docker_image)) > 1 ? split(":", var.app2_docker_image)[1] : "latest"
    }
    
    # Enable health check with retries
    health_check_eviction_time_in_min = 5
  }
  
  app_settings = merge(
    {
      APPLICATIONINSIGHTS_CONNECTION_STRING      = azurerm_application_insights.main.connection_string
      ApplicationInsightsAgent_EXTENSION_VERSION = "~3"
      DOCKER_REGISTRY_SERVER_URL                 = "https://${azurerm_container_registry.main.login_server}"
      DOCKER_REGISTRY_SERVER_USERNAME            = azurerm_container_registry.main.admin_username
      DOCKER_REGISTRY_SERVER_PASSWORD            = azurerm_container_registry.main.admin_password
      WEBSITES_ENABLE_APP_SERVICE_STORAGE        = "false"
      WEBSITES_PORT                              = tostring(var.app2_port)
      ASPNETCORE_ENVIRONMENT                     = title(var.environment)
      ENVIRONMENT                                = var.environment
    },
    var.app2_custom_settings
  )
  
  identity {
    type = "SystemAssigned"
  }
  
  logs {
    detailed_error_messages = true
    failed_request_tracing  = true
    
    http_logs {
      file_system {
        retention_in_days = 7
        retention_in_mb   = 35
      }
    }
    
    application_logs {
      file_system_level = "Information"
    }
  }
  
  tags = merge(
    local.common_tags,
    {
      AppService = var.app2_name
    }
  )
}

# Auto-scale settings for App Service 1
resource "azurerm_monitor_autoscale_setting" "app1" {
  name                = "autoscale-${local.app1_full_name}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  target_resource_id  = azurerm_service_plan.main.id
  
  profile {
    name = "default"
    
    capacity {
      default = var.app1_min_instances
      minimum = var.app1_min_instances
      maximum = var.app1_max_instances
    }
    
    rule {
      metric_trigger {
        metric_name        = "CpuPercentage"
        metric_resource_id = azurerm_service_plan.main.id
        time_grain         = "PT1M"
        statistic          = "Average"
        time_window        = "PT5M"
        time_aggregation   = "Average"
        operator           = "GreaterThan"
        threshold          = 70
      }
      
      scale_action {
        direction = "Increase"
        type      = "ChangeCount"
        value     = "1"
        cooldown  = "PT5M"
      }
    }
    
    rule {
      metric_trigger {
        metric_name        = "CpuPercentage"
        metric_resource_id = azurerm_service_plan.main.id
        time_grain         = "PT1M"
        statistic          = "Average"
        time_window        = "PT5M"
        time_aggregation   = "Average"
        operator           = "LessThan"
        threshold          = 30
      }
      
      scale_action {
        direction = "Decrease"
        type      = "ChangeCount"
        value     = "1"
        cooldown  = "PT5M"
      }
    }
  }
  
  tags = local.common_tags
}

# Grant ACR pull permission to App Service 1 managed identity
resource "azurerm_role_assignment" "app1_acr_pull" {
  scope                = azurerm_container_registry.main.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_linux_web_app.app1.identity[0].principal_id
}

# Grant ACR pull permission to App Service 2 managed identity
resource "azurerm_role_assignment" "app2_acr_pull" {
  scope                = azurerm_container_registry.main.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_linux_web_app.app2.identity[0].principal_id
}
