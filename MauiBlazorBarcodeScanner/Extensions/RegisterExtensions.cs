#if ANDROID
using AndroidX.Activity;
using MauiBlazorBarcodeScanner.Permissions;
#endif
using MauiBlazorBarcodeScanner.Services;
using Microsoft.Maui.LifecycleEvents;

namespace MauiBlazorBarcodeScanner.Extensions
{
    public static class RegisterExtensions
    {
        public static void AddMauiBlazorBarcodeScanner(this MauiAppBuilder builder, bool addBlazorWebView = false)
        {
            builder.ConfigureLifecycleEvents(e =>
            {
#if ANDROID
                e.AddAndroid(android =>
                {
                    android.OnCreate((activity, bundle) =>
                    {
                        // Called once per page it seems...
                        var permissionManager = ServiceProvider.Current.GetRequiredService<IBlazorWebViewPermissionsManager>();

                        if (activity is not ComponentActivity componentActivity)
                        {
                            throw new InvalidOperationException($"The permission-managing WebChromeClient requires that the current activity be a '{nameof(ComponentActivity)}'.");
                        }
                        permissionManager.SetActivity(componentActivity);
                    });
                });
#endif
            });

            if (addBlazorWebView)
            {
                builder.Services.AddMauiBlazorWebView();
#if DEBUG
                builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            }

#if ANDROID
            builder.Services.AddSingleton<IBlazorWebViewPermissionsManager, PermissionManager>();
#endif

            builder.Services.AddSingleton<ICameraControlService, CameraControlService>();
        }
    }
}
