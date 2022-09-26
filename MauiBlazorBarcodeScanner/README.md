# MauiBlazorBarcodeScanner
A barcode scanner for .NET MAUI using MAUI Blazor Hybrid.

Because of current MAUI support for camera preview being limited to the pre-release ZXing and indeed general issues with cross platform camera preview
and scanning, this is an alternative to utlizes the power of Blazor Hybrid and our old friend JavaScript to use the platforms webview to access a camera
preview, and a JS lib (using ZXing JS!) to scan the barcode from video frames.

## Steps to add to your project
1. Install Nuget package
TODO: create Nuget package!
```
Install-Package MauiBlazorBarcodeScanner
```

2. Add as a dependency to the MAUI app builder:
```cs
builder.AddMauiBlazorBarcodeScanner();
```

2. Ensure you have CAMERA permission for Android (Platforms/Android/AndroidManifest.xml) or added it to your Info.plist for iOS.

_Android_
```xml
<uses-permission android:name="android.permission.CAMERA" />
```

_iOS_
```xml
<key>NSCameraUsageDescription</key>
<string>The camera scanner requires this permission to open the camera and look for barcodes to scan.</string>
```

## NOTE
1. If you get build errors mentioning:

_obj\Debug\net6.0-android\staticwebassets_

Being missing in one of the Android- folders, manually copy the one from the net6.0-android folder into the specific architecture folder it
mentions and it will then build.
I think this must be a Visual Studio build bug...

2. If the camera loads but the test scan and/or scanning from the camera isnt working, this is likely an Android bug with webview:

https://github.com/dotnet/maui/issues/9882

If you go to Google Play and uninstall the Android web view control, it will work. It appears to be an issue with the latest webview, 
and was literally resolved yesterday at the time of writing, so im hoping it will be fine by the time you read this!

The permissions part of this project was copied from:
https://github.com/MackinnonBuck/MauiBlazorPermissionsExample
I logged on a issue on there regarding an issue specifically with AppShell where something the app shell does mean the blazor web view on 
initalized method is too late to register with the Android activitie's result handler meaning it throws an error. I modifed this to work by
hooking into the android page lifecycle and altering the permission manager to be a singleton that gets set by both the activity and the web view.
Not necessarily the nicest solution, but any better ideas welcome!