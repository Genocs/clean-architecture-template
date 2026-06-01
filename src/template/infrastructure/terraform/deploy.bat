@echo off
REM Terraform Deployment Script for Windows
REM Usage: deploy.bat <environment> [action]
REM Example: deploy.bat dev plan
REM Example: deploy.bat prod apply

setlocal enabledelayedexpansion

if "%1"=="" (
    echo [ERROR] Environment not specified
    echo Usage: %0 ^<environment^> [action]
    echo Environments: dev, test, stage, prod
    echo Actions: plan, apply, destroy ^(default: plan^)
    exit /b 1
)

set ENVIRONMENT=%1
set ACTION=%2
if "%ACTION%"=="" set ACTION=plan

REM Validate environment
if not "%ENVIRONMENT%"=="dev" if not "%ENVIRONMENT%"=="test" if not "%ENVIRONMENT%"=="stage" if not "%ENVIRONMENT%"=="prod" (
    echo [ERROR] Invalid environment: %ENVIRONMENT%
    echo Valid environments: dev, test, stage, prod
    exit /b 1
)

REM Validate action
if not "%ACTION%"=="plan" if not "%ACTION%"=="apply" if not "%ACTION%"=="destroy" if not "%ACTION%"=="output" (
    echo [ERROR] Invalid action: %ACTION%
    echo Valid actions: plan, apply, destroy, output
    exit /b 1
)

set TFVARS_FILE=%ENVIRONMENT%.tfvars

REM Check if tfvars file exists
if not exist "%TFVARS_FILE%" (
    echo [ERROR] Configuration file not found: %TFVARS_FILE%
    exit /b 1
)

echo [INFO] Starting Terraform %ACTION% for %ENVIRONMENT% environment

REM Check if Terraform is installed
where terraform >nul 2>nul
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Terraform is not installed
    exit /b 1
)

REM Check if Azure CLI is installed
where az >nul 2>nul
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Azure CLI is not installed
    exit /b 1
)

REM Check if logged in to Azure
echo [INFO] Checking Azure login status...
az account show >nul 2>nul
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Not logged in to Azure. Please run 'az login'
    exit /b 1
)

for /f "tokens=*" %%i in ('az account show --query name -o tsv') do set SUBSCRIPTION=%%i
echo [INFO] Using Azure subscription: %SUBSCRIPTION%

REM Initialize Terraform if needed
if not exist ".terraform" (
    echo [INFO] Initializing Terraform...
    terraform init
)

REM Format and validate
echo [INFO] Formatting Terraform files...
terraform fmt

echo [INFO] Validating Terraform configuration...
terraform validate

REM Execute Terraform action
if "%ACTION%"=="plan" (
    echo [INFO] Creating Terraform plan...
    terraform plan -var-file="%TFVARS_FILE%" -out="%ENVIRONMENT%.tfplan"
    echo [INFO] Plan saved to %ENVIRONMENT%.tfplan
) else if "%ACTION%"=="apply" (
    if exist "%ENVIRONMENT%.tfplan" (
        echo [WARNING] Applying existing plan: %ENVIRONMENT%.tfplan
        set /p confirm="Continue? (yes/no): "
        if "!confirm!"=="yes" (
            terraform apply "%ENVIRONMENT%.tfplan"
            del "%ENVIRONMENT%.tfplan"
        ) else (
            echo [INFO] Apply cancelled
            exit /b 0
        )
    ) else (
        echo [WARNING] Applying changes for %ENVIRONMENT% environment
        terraform plan -var-file="%TFVARS_FILE%"
        set /p confirm="Continue with apply? (yes/no): "
        if "!confirm!"=="yes" (
            terraform apply -var-file="%TFVARS_FILE%"
        ) else (
            echo [INFO] Apply cancelled
            exit /b 0
        )
    )
    echo [INFO] Deployment summary:
    terraform output deployment_summary
) else if "%ACTION%"=="destroy" (
    echo [WARNING] This will DESTROY all resources for %ENVIRONMENT% environment
    set /p confirm="Are you absolutely sure? Type 'destroy-%ENVIRONMENT%' to confirm: "
    if "!confirm!"=="destroy-%ENVIRONMENT%" (
        terraform destroy -var-file="%TFVARS_FILE%"
        echo [INFO] Resources destroyed
    ) else (
        echo [INFO] Destroy cancelled
        exit /b 0
    )
) else if "%ACTION%"=="output" (
    terraform output
)

echo [INFO] Terraform %ACTION% completed successfully for %ENVIRONMENT% environment
