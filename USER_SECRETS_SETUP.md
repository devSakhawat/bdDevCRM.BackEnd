# User Secrets Setup Guide

## What are User Secrets?

User Secrets is a secure way to store sensitive configuration data during development. The secrets are stored outside your project directory and are never committed to source control.

## Setup Instructions

### 1. Initialize User Secrets (Already Done)

The project has been initialized with User Secrets ID: `ec3acf75-ea5d-45e7-92b0-f414016fc25e`

### 2. Set Your Secrets

Run these commands from the `bdDevCRM.Api` project directory:

```bash
cd bdDevCRM.Api

# Database Connection String
dotnet user-secrets set "ConnectionStrings:DbLocation" "Server=YOUR_SERVER;User ID=sa;Password=YOUR_PASSWORD;Database=dbDevCRM;TrustServerCertificate=True;"

# JWT Secret Key (must be at least 32 characters)
dotnet user-secrets set "Jwt:SecretKey" "YOUR-VERY-SECURE-SECRET-KEY-AT-LEAST-32-CHARACTERS-LONG"

# Application Insights (if using Azure)
dotnet user-secrets set "ApplicationInsights:InstrumentationKey" "YOUR-APP-INSIGHTS-KEY"

# Redis Connection (if different from default)
dotnet user-secrets set "Redis:Configuration" "localhost:6379"
dotnet user-secrets set "ConnectionStrings:Redis" "localhost:6379"
```

### 3. Verify Your Secrets

```bash
dotnet user-secrets list
```

### 4. Clear Secrets (if needed)

```bash
# Remove a specific secret
dotnet user-secrets remove "ConnectionStrings:DbLocation"

# Clear all secrets
dotnet user-secrets clear
```

## Example Values for Development

```bash
# Example for local SQL Server
dotnet user-secrets set "ConnectionStrings:DbLocation" "Server=localhost\\SQLEXPRESS;Database=dbDevCRM;Trusted_Connection=true;TrustServerCertificate=True;"

# Example JWT Secret
dotnet user-secrets set "Jwt:SecretKey" "WeAreBangladeshiDevelopersWeAreActiveWeAreProductive3011PlusExtraChars"

# Example Redis (local)
dotnet user-secrets set "Redis:Configuration" "localhost:6379"
```

## How It Works

1. During development, .NET automatically reads User Secrets
2. Configuration priority:
   - appsettings.json (base configuration)
   - appsettings.Development.json (development overrides)
   - User Secrets (sensitive data - highest priority in dev)
   - Environment Variables
   - Command-line arguments

3. User Secrets location:
   - **Windows**: `%APPDATA%\Microsoft\UserSecrets\ec3acf75-ea5d-45e7-92b0-f414016fc25e\secrets.json`
   - **Linux/Mac**: `~/.microsoft/usersecrets/ec3acf75-ea5d-45e7-92b0-f414016fc25e/secrets.json`

## Production Deployment

**⚠️ Important**: User Secrets only work in Development environment!

For production, use:
- **Azure**: Azure Key Vault
- **Docker**: Environment Variables
- **Kubernetes**: Secrets
- **Other**: Encrypted configuration files or secret management systems

## Security Notes

✅ **DO**:
- Use User Secrets for development
- Keep secrets.json file backed up securely
- Use different secrets for each environment
- Rotate secrets regularly

❌ **DON'T**:
- Commit appsettings.json with actual secrets
- Share your secrets.json file
- Use production secrets in development
- Store secrets in source control

## Team Setup

Each team member should:
1. Clone the repository
2. Run `dotnet user-secrets init` (if not already done)
3. Set their own secrets using the commands above
4. Never commit their secrets.json file

## Troubleshooting

**Problem**: "Configuration is missing required keys"
- **Solution**: Make sure you've set all required secrets using `dotnet user-secrets set`

**Problem**: "Cannot find secrets.json"
- **Solution**: Run `dotnet user-secrets init` from the project directory

**Problem**: "Secrets not loading"
- **Solution**: Ensure you're running in Development environment (`ASPNETCORE_ENVIRONMENT=Development`)

## Validation

The application validates configuration on startup. If required secrets are missing, you'll see an error message indicating which configuration keys are required.
