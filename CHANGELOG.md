# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## [6.17.5] - 2025-12-18

### Changed

- Updated Android SDK to v6.17.5

## [6.17.3] - 2024-12-19

### Changed

- Updated Android SDK to v6.17.3
- **Breaking Change**: Simplified `AFPurchaseDetails` constructor as part of ValidateAndLog V2 API updates. Constructor now only requires 3 parameters: `purchaseType`, `purchaseToken`, and `productId`. Revenue and currency information is handled automatically by the SDK.

## [6.13.1] - 2024-07-22

### Fixed

- Resolved the issue with .NET 8 support

## [6.13.0] - 2024-03-14

### Changed

- Updated Android SDK to v6.13.0
- Add DMA supprot
  
## [6.4.5.0] - 2022-04-18

### Changed

- Updated SDK to v6.4.5
- Xamarin.Android 12.0.0.3

## [6.4.0.1] - 2021-09-30

### Added

- [setSharingFilterForPartners](https://dev.appsflyer.com/hc/docs/android-sdk-reference-appsflyerlib#setsharingfilterforpartners)

### Changed

- Updated SDK to v6.4.0

### Deprecated

- [setSharingFilterForAllPartners](https://dev.appsflyer.com/hc/docs/android-sdk-reference-appsflyerlib#setsharingfilterforallpartners)
- [sharingFilter](https://dev.appsflyer.com/hc/docs/android-sdk-reference-appsflyerlib#sharingfilter)

