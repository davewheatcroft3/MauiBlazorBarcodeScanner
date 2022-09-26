using MauiBlazorBarcodeScanner.Permissions;
using Microsoft.AspNetCore.Components.WebView;
using ServiceProvider = MauiBlazorBarcodeScanner.Extensions.ServiceProvider;

namespace MauiBlazorBarcodeScanner;

public partial class CameraScannerView
{
    // To manage Android permissions, update AndroidManifest.xml to include the permissions and
    // features required by your app. You may have to perform additional configuration to enable
    // use of those APIs from the WebView, as is done below. A custom WebChromeClient is needed
    // to define what happens when the WebView requests a set of permissions. See
    // PermissionManagingBlazorWebChromeClient.cs to explore the approach taken in this example.

    private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
    {
    }

    private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.Settings.JavaScriptEnabled = true;
        e.WebView.Settings.MediaPlaybackRequiresUserGesture = false;

        var permissionsManager = ServiceProvider.Current.GetRequiredService<IBlazorWebViewPermissionsManager>();
        var permissionWebView = permissionsManager.SetWebView(e.WebView.WebChromeClient!);
        e.WebView.SetWebChromeClient(permissionWebView);
    }
}