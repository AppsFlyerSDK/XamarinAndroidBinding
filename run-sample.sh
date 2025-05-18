#!/bin/bash

# Build the app
dotnet build samples/Sample.Android/Sample.Android.csproj -c Release

# Check if an Android device is connected
if ! adb devices | grep -q "device$"; then
  echo "No Android device connected."
  echo "Please connect a device or start an emulator, then run this script again."
  exit 1
fi

# Force stop any existing app instances
echo "Stopping any running instances of the app..."
adb shell am force-stop com.appsflyer.xamarinsample

# Install and run the app
echo "Installing and launching AppsFlyer sample app..."
adb install -r samples/Sample.Android/bin/Release/net8.0-android/com.appsflyer.xamarinsample-Signed.apk
adb shell am start -n com.appsflyer.xamarinsample/com.appsflyer.xamarinsample.MainActivity

echo "App launched! Check the Android logcat for AppsFlyer SDK logs:"
echo "adb logcat -s AppsFlyer_[4UGrDF4]"
echo "adb logcat | grep AppsFlyer" 