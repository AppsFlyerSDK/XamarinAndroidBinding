rm -rf AppsFlyerBinding.Android/bin
rm -rf AppsFlyerBinding.Android/obj

dotnet clean
dotnet restore
dotnet pack -c Release AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj

mv AppsFlyerBinding.Android/bin/Release/AppsFlyerXamarinBinding*.nupkg nugets/