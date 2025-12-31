#!/usr/bin/env bash

# GitHub Template Setup Script
# This script replaces wordfrequency placeholders with your actual namespace

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${GREEN}=== GitHub Template Setup ===${NC}"
echo ""

# Check if setup has already been completed (NO-OP check)
# If wordfrequency folders don't exist, check if setup was already done
if [ ! -d "wordfrequency" ] && [ ! -d "wordfrequency.Tests" ]; then
    # Look for .csproj files that don't contain wordfrequency (indicating setup was done)
    EXISTING_PROJECTS=$(find . -maxdepth 2 -name "*.csproj" ! -path "./.git/*" ! -name "*wordfrequency*" 2>/dev/null | head -1)
    if [ -n "$EXISTING_PROJECTS" ]; then
        echo -e "${GREEN}✓ Setup has already been completed (likely by GitHub Actions)${NC}"
        echo -e "${YELLOW}This script is a NO-OP. Your project is ready to use!${NC}"
        echo ""
        echo "Found existing project files:"
        echo "$EXISTING_PROJECTS" | sed 's/^/  - /'
        echo ""
        exit 0
    fi
    echo -e "${RED}Error: Template folders not found. Make sure you're running this from the repository root.${NC}"
    exit 1
fi

# Prompt for namespace
if [ -z "$1" ]; then
    echo -e "${YELLOW}Enter your namespace (e.g., my.service or MyService):${NC}"
    read -r NAMESPACE
else
    NAMESPACE="$1"
fi

if [ -z "$NAMESPACE" ]; then
    echo -e "${RED}Error: Namespace cannot be empty${NC}"
    exit 1
fi

# Validate namespace (basic check - should be valid C# namespace)
if [[ ! "$NAMESPACE" =~ ^[a-zA-Z][a-zA-Z0-9._]*$ ]]; then
    echo -e "${RED}Error: Invalid namespace format. Use alphanumeric characters, dots, and underscores.${NC}"
    exit 1
fi

echo -e "${GREEN}Using namespace: ${NAMESPACE}${NC}"
echo ""

# Function to replace in file
replace_in_file() {
    local file="$1"
    if [ -f "$file" ]; then
        # Use sed with different syntax for macOS vs Linux
        if [[ "$OSTYPE" == "darwin"* ]]; then
            sed -i '' "s/wordfrequency/${NAMESPACE}/g" "$file"
        else
            sed -i "s/wordfrequency/${NAMESPACE}/g" "$file"
        fi
    fi
}

# Function to replace in all files recursively
replace_in_all_files() {
    local dir="$1"
    if [ ! -d "$dir" ]; then
        echo -e "${YELLOW}Warning: Directory $dir does not exist, skipping...${NC}"
        return
    fi
    # Find all files and replace placeholders
    find "$dir" -type f \( -name "*.cs" -o -name "*.csproj" -o -name "*.sh" -o -name "Dockerfile" -o -name "*.md" -o -name "*.yml" -o -name "*.yaml" \) | while read -r file; do
        replace_in_file "$file"
    done
}

echo -e "${YELLOW}Replacing placeholders in files...${NC}"

# Replace in all files BEFORE renaming folders
replace_in_all_files "wordfrequency"
replace_in_all_files "wordfrequency.Tests"
replace_in_file "run.sh"
replace_in_file "README.md"
replace_in_file "setup.sh"

echo -e "${GREEN}✓ Placeholders replaced${NC}"

# Verify replacements worked
REMAINING_PLACEHOLDERS=$(grep -r "wordfrequency" "wordfrequency" "wordfrequency.Tests" 2>/dev/null | wc -l | tr -d ' ' || echo "0")
if [ "$REMAINING_PLACEHOLDERS" -gt 0 ]; then
    echo -e "${YELLOW}Warning: Found $REMAINING_PLACEHOLDERS remaining placeholders after replacement${NC}"
fi

# Rename .csproj files first (before renaming folders)
echo -e "${YELLOW}Renaming project files...${NC}"

if [ -f "wordfrequency/wordfrequency.csproj" ]; then
    mv "wordfrequency/wordfrequency.csproj" "wordfrequency/${NAMESPACE}.csproj"
    echo -e "${GREEN}✓ Renamed wordfrequency/wordfrequency.csproj to ${NAMESPACE}.csproj${NC}"
fi

if [ -f "wordfrequency.Tests/wordfrequency.Tests.csproj" ]; then
    mv "wordfrequency.Tests/wordfrequency.Tests.csproj" "wordfrequency.Tests/${NAMESPACE}.Tests.csproj"
    echo -e "${GREEN}✓ Renamed wordfrequency.Tests/wordfrequency.Tests.csproj to ${NAMESPACE}.Tests.csproj${NC}"
fi

# Rename folders
echo -e "${YELLOW}Renaming folders...${NC}"

if [ -d "wordfrequency" ]; then
    mv "wordfrequency" "${NAMESPACE}"
    echo -e "${GREEN}✓ Renamed wordfrequency to ${NAMESPACE}${NC}"
fi

if [ -d "wordfrequency.Tests" ]; then
    mv "wordfrequency.Tests" "${NAMESPACE}.Tests"
    echo -e "${GREEN}✓ Renamed wordfrequency.Tests to ${NAMESPACE}.Tests${NC}"
fi

# Update run.sh to use correct path
if [ -f "run.sh" ]; then
    if [[ "$OSTYPE" == "darwin"* ]]; then
        sed -i '' "s/cd wordfrequency/cd ${NAMESPACE}/g" "run.sh"
    else
        sed -i "s/cd wordfrequency/cd ${NAMESPACE}/g" "run.sh"
    fi
fi

echo ""
echo -e "${GREEN}=== Setup Complete! ===${NC}"
echo ""
echo -e "Your project has been configured with namespace: ${GREEN}${NAMESPACE}${NC}"
echo ""
echo "Next steps:"
echo "  1. Review the changes"
echo "  2. Run 'dotnet restore' in both project folders"
echo "  3. Run 'dotnet build' to verify everything compiles"
echo "  4. Update README.md with your project-specific information"
echo ""

