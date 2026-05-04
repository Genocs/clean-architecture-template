# Bicep Resources

## 1. Log Analytics Workspace (`Microsoft.OperationalInsights/workspaces@2023-09-01`)
- **Name**: `law-{uniqueString}` (following Azure naming conventions)
- **SKU**: `PerGB2018` (pay-as-you-go pricing model)
- **Retention**: 30 days (cost-effective for development/testing)
- **Features**: Enabled resource-based access control

## 2. Application Insights (`Microsoft.Insights/components@2020-02-02`)
- **Name**: `appi-{uniqueString}` (following Azure naming conventions)
- **Kind**: `web` (optimized for web applications)
- **Application Type**: `web`
- **Workspace Integration**: Connected to the Log Analytics workspace
- **Retention**: 90 days (suitable for application monitoring)
- **Security**: IP masking enabled, local auth disabled for better security

## Enhanced Web App Configuration:
I also updated your web app to include Application Insights configuration:
- Added `APPLICATIONINSIGHTS_CONNECTION_STRING` setting
- Added `ApplicationInsightsAgent_EXTENSION_VERSION` for automatic instrumentation

## Output Values:
Added comprehensive outputs for both services:
- Log Analytics workspace ID and name
- Application Insights ID, name, and instrumentation key
- Application Insights connection string (marked as `@secure()`)

## Key Benefits:
1. **Integrated Monitoring**: Application Insights is connected to Log Analytics for centralized logging
2. **Cost Optimization**: Appropriate retention periods and SKUs for development scenarios
3. **Security**: Following best practices with secure connection strings and proper authentication
4. **Scalability**: Resources can easily be scaled up for production use
5. **Best Practices**: Following Azure naming conventions and Bicep best practices

The template now provides a complete monitoring and logging solution for your web application infrastructure!

Made changes.