#!/bin/bash

# Define color codes
GREEN='\033[0;32m'
NC='\033[0m' # No Color

echo "Cleaning AppsFlyer binding plugin and sample app..."

# Clean the binding project with dotnet clean
echo "Cleaning binding project..."
dotnet clean AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj -c Release
dotnet clean AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj -c Debug

# Also manually remove bin/obj directories to ensure complete cleanup
rm -rf AppsFlyerBinding.Android/bin
rm -rf AppsFlyerBinding.Android/obj

# Clean the sample project with dotnet clean
echo "Cleaning sample project..."
dotnet clean samples/Sample.Android/Sample.Android.csproj -c Release
dotnet clean samples/Sample.Android/Sample.Android.csproj -c Debug

# Also manually remove bin/obj directories to ensure complete cleanup
rm -rf samples/Sample.Android/bin
rm -rf samples/Sample.Android/obj

# Uninstall the app from connected devices (optional)
if adb devices | grep -q "device$"; then
  read -p "Do you want to uninstall the app from connected devices? (y/n): " uninstall_app
  if [ "$uninstall_app" = "y" ] || [ "$uninstall_app" = "Y" ]; then
    echo "Uninstalling app from connected devices..."
    adb uninstall com.appsflyer.xamarinsample
  fi
fi

# Optional: Clean NuGet packages
read -p "Do you want to clean generated NuGet packages too? (y/n): " clean_nuget
if [ "$clean_nuget" = "y" ] || [ "$clean_nuget" = "Y" ]; then
  echo "Cleaning NuGet packages..."
  rm -f nugets/AppsFlyerXamarinBindingAndroid.*.nupkg
fi

# Create a clean build of the solution
read -p "Do you want to rebuild the binding library? (y/n): " rebuild
if [ "$rebuild" = "y" ] || [ "$rebuild" = "Y" ]; then
  echo "Rebuilding binding library..."
  dotnet build AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj -c Release
fi

echo -e "${GREEN}Clean completed!${NC}" 