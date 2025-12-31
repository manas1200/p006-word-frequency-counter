# Template Setup Guide

This document provides detailed instructions for setting up this GitHub template.

## What is a GitHub Template?

A GitHub template repository allows you to create new repositories with the same structure and files. This template includes placeholders (`wordfrequency`) that need to be replaced with your actual namespace.

## Setup Methods

### Method 1: Setup Script (Easiest)

1. Clone the template repository:
   ```bash
   git clone <your-repo-url>
   cd <your-repo-name>
   ```

2. Run the setup script:
   ```bash
   chmod +x setup.sh
   ./setup.sh
   ```

3. Enter your namespace when prompted (e.g., `my.service` or `MyService`)

4. The script will automatically:
   - Replace all `wordfrequency` placeholders
   - Rename folders
   - Update all configuration files

### Method 2: GitHub Actions

1. After creating a repository from this template, go to the **Actions** tab
2. Select **Setup Template** workflow
3. Click **Run workflow**
4. Enter your namespace
5. The workflow will commit the changes automatically

### Method 3: Manual Setup

If you prefer manual control:

1. **Find and Replace**: Use your IDE's find-and-replace feature:
   - Find: `wordfrequency`
   - Replace: `Your.Namespace` (use your actual namespace)

2. **Rename Folders**:
   ```bash
   mv "wordfrequency" "YourNamespace"
   mv "wordfrequency.Tests" "YourNamespace.Tests"
   ```

3. **Update Project Files**:
   - Update `.csproj` files with your namespace
   - Update `RootNamespace` and `PackageId` properties

4. **Update Scripts**:
   - Update `run.sh` to use the correct folder path
   - Update `Dockerfile` ENTRYPOINT with your executable name
   - Update NATS subject strings in controllers and scripts

## Namespace Guidelines

- Use valid C# namespace format (alphanumeric, dots, underscores)
- Examples:
  - `my.service`
  - `MyService`
  - `my_company.service`
  - `MyCompany.Service`

## Verification

After setup, verify everything works:

```bash
# Restore packages
dotnet restore

# Build the project
dotnet build

# Run tests
dotnet test

# Run the application
./run.sh
```

## Troubleshooting

### Setup script fails
- Make sure you're running from the repository root
- Check that `wordfrequency` folders exist
- Verify you have write permissions

### Build errors
- Ensure all `wordfrequency` placeholders are replaced
- Check that folder names match your namespace
- Verify `.csproj` files reference correct paths

### Runtime errors
- Check NATS subject strings are updated
- Verify Dockerfile ENTRYPOINT uses correct executable name
- Ensure environment variables are set correctly

## Need Help?

If you encounter issues, please:
1. Check the main [README.md](README.md)
2. Review the [cloops.microservices documentation](https://github.com/connectionloops/cloops.microservices/tree/main/docs)
3. Open an issue in the main repository

