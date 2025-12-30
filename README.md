<div style="text-align:center">
    <div>
        <h1>
            Cloops Microservice Template
        </h1>
    </div>
    <div style="display:flex; gap: 8px; justify-content: center; align-items:center">
        <img src="https://img.shields.io/badge/template-ready-brightgreen">
        <img src="https://img.shields.io/badge/.NET-9.0-purple">
    </div>
</div>

# Getting Started

This is a GitHub template repository for creating Cloops microservices. **You must run the setup script after cloning to bootstrap your project.**

> To learn more about `cloops.microservices`, please checkout our docs on [GitHub](https://github.com/connectionloops/cloops.microservices/tree/main/docs)

## ⚠️ IMPORTANT: Run Setup Script First!

**Before doing anything else, you MUST run the setup script to bootstrap your project:**

```bash
chmod +x setup.sh
./setup.sh
```

The script will prompt you for your namespace (e.g., `my.service` or `MyService`) and automatically:

- Replace all `{{NAMESPACE}}` placeholders in your code
- Rename project folders from `{{NAMESPACE}}` to your namespace
- Rename `.csproj` files to match your namespace
- Update all configuration files (Dockerfile, scripts, etc.)

> **Note:** If you used GitHub Actions to set up the template, the script will detect this and exit as a NO-OP (no operation needed).

## Bootstrapping Process

When you clone this template, the repository contains placeholder folders and files:

- `{{NAMESPACE}}/` - Main project folder (will be renamed to your namespace)
- `{{NAMESPACE}}.Tests/` - Test project folder (will be renamed to your namespace)
- All code files contain `{{NAMESPACE}}` placeholders that need to be replaced

The setup process (`./setup.sh`) performs the following transformations:

1. **Replaces placeholders** in all `.cs`, `.csproj`, `.sh`, `Dockerfile`, and other config files
2. **Renames project files**: `{{NAMESPACE}}.csproj` → `{YourNamespace}.csproj`
3. **Renames folders**: `{{NAMESPACE}}/` → `{YourNamespace}/`
4. **Updates paths** in scripts and configuration files

After running `./setup.sh`, your project structure will be ready to use with your chosen namespace.

## Setup Options

### Option 1: Setup Script (Recommended for Local Development)

```bash
chmod +x setup.sh
./setup.sh
# Enter your namespace when prompted (e.g., my.service)
```

### Option 2: GitHub Actions (Recommended for Automated Setup)

1. Go to the **Actions** tab in your repository
2. Select **Setup Template** workflow
3. Click **Run workflow**
4. Enter your namespace when prompted
5. The workflow will automatically replace placeholders and commit the changes

> **Note:** If GitHub Actions has already run, the `./setup.sh` script will detect this and exit gracefully (NO-OP).

### Option 3: Manual Setup

If you prefer to set up manually (not recommended):

1. Replace all occurrences of `{{NAMESPACE}}` with your actual namespace
2. Rename the `{{NAMESPACE}}` folder to your namespace
3. Rename the `{{NAMESPACE}}.Tests` folder to `{YourNamespace}.Tests`
4. Rename `{{NAMESPACE}}/{{NAMESPACE}}.csproj` to `{YourNamespace}/{YourNamespace}.csproj`
5. Rename `{{NAMESPACE}}.Tests/{{NAMESPACE}}.Tests.csproj` to `{YourNamespace}.Tests/{YourNamespace}.Tests.csproj`
6. Update `Dockerfile` ENTRYPOINT with your executable name
7. Update NATS subjects in controllers and scripts

## Project Structure

After setup, your project will have the following structure:

```
{YourNamespace}/
  ├── {YourNamespace}.csproj
  ├── Program.cs
  ├── controllers/
  ├── services/
  ├── schema/
  ├── util/
  └── Dockerfile

{YourNamespace}.Tests/
  ├── {YourNamespace}.Tests.csproj
  └── Controllers/
```

## Local Setup

### Prerequisites

The repo needs the following local setup infrastructure dependencies:

- **NATS Server** - For messaging
- **.NET 9.0 SDK** - For building and running

### Running the Project Locally

**First, make sure you've run `./setup.sh` to bootstrap your project!**

It is assumed that your environment variables are loaded from `.env` file. Please use a dedicated config and secret management solution. Please see our docs around config for more info.

```bash
# After running ./setup.sh, run the app
chmod +x run.sh
./run.sh

# That's it, you are set, use nats requests in {YourNamespace}/nats/ to test the service
# e.g.
cd {YourNamespace}/nats
./health.sh
```

## Features

- ✅ **NATS Integration** - Built-in NATS client with consumer attributes
- ✅ **Dependency Injection** - Full DI support with Microsoft.Extensions
- ✅ **Background Services** - Scheduled tasks with cron expressions
- ✅ **HTTP Client** - Example HTTP service integration
- ✅ **Metrics** - Application metrics with System.Diagnostics.Metrics
- ✅ **Testing** - XUnit test project with Moq
- ✅ **Docker** - Ready-to-use Dockerfile

## Alive Nudge

The repo out of the box ships with an AliveNudge background service that just logs `service executing nudge` every 30 seconds. This is just to demonstrate how to do scheduled tasks in background. Please remove the file `services/background/alive.nudge.cs` to get rid of it.

## Next Steps

1. **Update README.md** - Add your project-specific information
2. **Configure Services** - Update `AppSettings.cs` with your configuration
3. **Add Your Controllers** - Create NATS consumers in the `controllers/` folder
4. **Write Tests** - Add tests in the `{YourNamespace}.Tests/` project
5. **Update Schema** - Customize NATS message types in the `schema/` folder

## Documentation

To learn more about `cloops.microservices`, please checkout our docs on [GitHub](https://github.com/connectionloops/cloops.microservices/tree/main/docs)

## Support

For issues and questions, please open an issue in the [cloops.microservices](https://github.com/connectionloops/cloops.microservices) repository.
