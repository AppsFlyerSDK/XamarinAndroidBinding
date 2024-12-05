mkdir -p nugets
rm nugets/*

rm -rf AppsFlyerBinding.Android/bin
rm -rf AppsFlyerBinding.Android/obj

dotnet clean AppsFlyerBinding.Android/
dotnet restore AppsFlyerBinding.Android/
dotnet pack -c Release AppsFlyerBinding.Android/AppsFlyerBinding.Android.csproj

mv AppsFlyerBinding.Android/bin/Release/AppsFlyerXamarinBinding*.nupkg nugets/
mv AppsFlyerBinding.Android/bin/Release/AppsFlyerXamarinBinding*.snupkg nugets/
