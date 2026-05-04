// Think of this as Infrastructure as Code (IaC) for deploying resources
// Bicep file to create a storage account and a web app in Azure

// Get the resource group location
@description('Location for all resources.')
param location string = 'italia north'

@description('The name of you Web Site.')
param webSiteName string = 'demo-website'

@description('A unique string to ensure resource names are globally unique.')
param uniqueString string = '{uniqueString(resourceGroup().id)}'

// Generate a unique storage account name
@description('The name of the Storage Account.')
param storageAccountName string = 'demostorage${uniqueString}'

@description('The name of the Log Analytics workspace.')
param logAnalyticsWorkspaceName string = 'law-${uniqueString}'

@description('The name of the Application Insights instance.')
param appInsightsName string = 'appi-${uniqueString}'

// Create a Log Analytics workspace
resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
    features: {
      enableLogAccessUsingOnlyResourcePermissions: true
    }
  }
}

// Create Application Insights
resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
    RetentionInDays: 90
    DisableIpMasking: false
    DisableLocalAuth: false
  }
}

// Create a storage account
resource storageAccount 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
  }
}

// Create an App Service Plan
resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: 'asp-${uniqueString}'
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

// Create a web app
resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: webSiteName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'STORAGE_ACCOUNT_NAME'
          value: storageAccount.name
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: applicationInsights.properties.ConnectionString
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
      ]
    }
  }
}

// Output values
output logAnalyticsWorkspaceId string = logAnalyticsWorkspace.id
output logAnalyticsWorkspaceName string = logAnalyticsWorkspace.name
output applicationInsightsId string = applicationInsights.id
output applicationInsightsName string = applicationInsights.name
output applicationInsightsInstrumentationKey string = applicationInsights.properties.InstrumentationKey
@secure()
output applicationInsightsConnectionString string = applicationInsights.properties.ConnectionString
