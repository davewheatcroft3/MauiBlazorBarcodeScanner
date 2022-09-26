using AndroidX.Activity;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.Platform;
using AWebView = Android.Webkit.WebView;

namespace MauiBlazorBarcodeScanner.Permissions
{
    public partial class CustomBlazorWebViewHandler : BlazorWebViewHandler
    {
        protected override AWebView CreatePlatformView()
        {
            var webView = base.CreatePlatformView();

            if (webView.Context?.GetActivity() is not ComponentActivity activity)
            {
                throw new InvalidOperationException($"The permission-managing WebChromeClient requires that the current activity be a '{nameof(ComponentActivity)}'.");
            }

            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.MediaPlaybackRequiresUserGesture = false;

            webView.SetWebChromeClient(new PermissionManagingBlazorWebChromeClient(webView.WebChromeClient!, activity));

            return webView;
        }
    }
}