#!/bin/bash

# Color codes
GREEN='\033[0;32m'
NC='\033[0m' # No Color

echo "Cleaning AppsFlyer binding plugin and sample app..."

# Optional: Clear all NuGet caches
read -p "Do you want to clear all global NuGet caches (http, global-packages, temp)? (y/n): " clean_nuget_caches
if [ "$clean_nuget_caches" = "y" ] || [ "$clean_nuget_caches" = "Y" ]; then
  echo "Clearing all NuGet caches..."
  dotnet nuget locals all --clear
fi

# Optional: Remove the .vs solution cache folder
read -p "Do you want to remove the .vs solution cache folder? (y/n): " clean_vs
if [ "$clean_vs" = "y" ] || [ "$clean_vs" = "Y" ]; then
  echo "Removing .vs folder..."
  rm -rf .vs
fi

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

# Optional: Clean generated NuGet package files
read -p "Do you want to clean generated NuGet package files (in ./nugets folder)? (y/n): " clean_nuget_files
if [ "$clean_nuget_files" = "y" ] || [ "$clean_nuget_files" = "Y" ]; then
  echo "Cleaning NuGet package files..."
  rm -f nugets/AppsFlyerXamarinBindingAndroid.*.nupkg
fi

# Create a clean build of the solution
read -p "Do you want to rebuild the binding library? (y/n): " rebuild
if [ "$rebuild" = "y" ] || [ "$rebuild" = "Y" ]; then
  echo "Rebuilding binding library..."
  dotnet build AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj -c Release
fi

echo -e "${GREEN}Clean completed!${NC}" 