using Microsoft.Web.WebView2.Core;

namespace MauiBlazorBarcodeScanner.Permissions;

internal interface IPermissionRequestHandler
{
    void OnPermissionRequested(CoreWebView2 sender, CoreWebView2PermissionRequestedEventArgs args);
}
