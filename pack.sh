#!/bin/bash

# Enhanced packaging script for AppsFlyer Xamarin Android binding
# For internal testing before publishing to NuGet
#
# USAGE EXAMPLES:
# ------------------------------
# Basic usage (builds with default version):
#   ./pack.sh
#
# Specify custom version:
#   ./pack.sh -v 6.18.0
#
# Specify pre-release version:
#   ./pack.sh -v 6.17.0-rc1
#
# Build package and rebuild sample app:
#   ./pack.sh -s
#
# Build package, rebuild and run sample app for testing:
#   ./pack.sh -t
#
# Combine options:
#   ./pack.sh -v 6.18.0 -t
# ------------------------------

# Color codes for better visibility
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Default version (can be overridden with -v parameter)
VERSION=$(grep -o '<PackageVersion>[^<]*' AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj | sed 's/<PackageVersion>//')
if [ -z "$VERSION" ]; then
    VERSION="6.17.0"
fi

# Parse command line arguments
REBUILD_SAMPLE=false
TEST_SAMPLE=false
DEBUG_MODE=false

while getopts "v:std" opt; do
  case $opt in
    v) VERSION="$OPTARG"
       echo -e "${YELLOW}Setting version to: $VERSION${NC}"
       ;;
    s) REBUILD_SAMPLE=true
       echo -e "${YELLOW}Will rebuild sample app with new package${NC}"
       ;;
    t) TEST_SAMPLE=true
       REBUILD_SAMPLE=true
       echo -e "${YELLOW}Will test sample app with new package${NC}"
       ;;
    d) DEBUG_MODE=true
       echo -e "${YELLOW}Debug mode enabled - more verbose output${NC}"
       ;;
    \?) echo -e "${RED}Invalid option -$OPTARG${NC}" >&2
       exit 1
       ;;
  esac
done

# Function to check if last command succeeded
check_result() {
    if [ $? -ne 0 ]; then
        echo -e "${RED}Error: $1 failed${NC}"
        exit 1
    fi
}

echo -e "${GREEN}Starting packaging process for version $VERSION...${NC}"

# Clean build directories
echo "Cleaning build directories..."
rm -rf AppsFlyerBinding.Android/bin
rm -rf AppsFlyerBinding.Android/obj
check_result "Cleaning directories"

# Clean and restore
echo "Running dotnet clean..."
dotnet clean AppsFlyerBinding.Android/
check_result "dotnet clean"

echo "Running dotnet restore..."
dotnet restore AppsFlyerBinding.Android/
check_result "dotnet restore"

# Temporarily update version in project file if different from default
ORIGINAL_PROJECT=$(cat AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj)
TEMP_VERSION_APPLIED=false

if grep -q "<PackageVersion>$VERSION</PackageVersion>" AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj; then
    echo "Using existing version: $VERSION"
else
    echo "Temporarily setting version to $VERSION..."
    sed -i.bak "s|<PackageVersion>[^<]*</PackageVersion>|<PackageVersion>$VERSION</PackageVersion>|g" AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj
    TEMP_VERSION_APPLIED=true
fi

# Remove existing packages with same version first
rm -f nugets/AppsFlyerXamarinBindingAndroid.$VERSION.nupkg
# For pre-release versions, we need to handle possible variations
if [[ "$VERSION" == *-* ]]; then
    # Remove any existing package that starts with the main version part
    MAIN_VERSION=$(echo $VERSION | cut -d'-' -f1)
    rm -f nugets/AppsFlyerXamarinBindingAndroid.$MAIN_VERSION*.nupkg
fi

# Build and pack
echo "Building NuGet package..."
dotnet pack -c Release AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj
check_result "dotnet pack"

# Restore original project file if we modified it
if [ "$TEMP_VERSION_APPLIED" = true ]; then
    echo "Restoring original project file..."
    echo "$ORIGINAL_PROJECT" > AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj
    rm -f AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj.bak
fi

# Create nugets directory if it doesn't exist
mkdir -p nugets

# Move package to nugets folder
echo "Moving package to nugets folder..."
mv AppsFlyerBinding.Android/bin/Release/AppsFlyerXamarinBindingAndroid.*.nupkg nugets/
check_result "Moving package"

# List all packages for debugging
if [ "$DEBUG_MODE" = true ]; then
    echo "Packages in nugets directory:"
    ls -la nugets/
fi

# For pre-release versions, we need a more flexible search
if [[ "$VERSION" == *-* ]]; then
    MAIN_VERSION=$(echo $VERSION | cut -d'-' -f1)
    PACKAGE_PATH=$(find nugets -name "AppsFlyerXamarinBindingAndroid.$MAIN_VERSION*.nupkg" | head -1)
else
    PACKAGE_PATH=$(find nugets -name "AppsFlyerXamarinBindingAndroid.$VERSION.nupkg" | head -1)
fi

if [ -z "$PACKAGE_PATH" ]; then
    echo -e "${RED}Error: Package not found after build${NC}"
    echo "Checking for any packages in the output directory:"
    ls -la nugets/
    exit 1
fi

echo -e "${GREEN}Package created successfully: $PACKAGE_PATH${NC}"

# Validate package (basic checks)
echo "Validating package..."
if unzip -l "$PACKAGE_PATH" | grep -q "AppsFlyerBinding.Android.dll"; then
    echo -e "${GREEN}Package contains required assemblies${NC}"
else
    echo -e "${RED}Warning: Package may be missing required assemblies${NC}"
fi

# Rebuild sample if requested
if [ "$REBUILD_SAMPLE" = true ]; then
    echo "Rebuilding sample app with new package..."
    dotnet clean samples/Sample.Android/Sample.Android.csproj
    dotnet restore samples/Sample.Android/Sample.Android.csproj
    dotnet build samples/Sample.Android/Sample.Android.csproj -c Release
    check_result "Rebuilding sample app"
    
    echo -e "${GREEN}Sample app rebuilt successfully${NC}"
    
    if [ "$TEST_SAMPLE" = true ] && [ -f "run-sample.sh" ]; then
        echo "Testing sample app..."
        ./run-sample.sh
        check_result "Testing sample app"
    fi
fi

echo -e "${GREEN}Package ready for testing: $PACKAGE_PATH${NC}"
echo "Use the following to reference in projects:"
echo -e "${YELLOW}<PackageReference Include=\"AppsFlyerXamarinBindingAndroid\" Version=\"$VERSION\" />${NC}"