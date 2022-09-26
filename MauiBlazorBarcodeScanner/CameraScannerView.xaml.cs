using MauiBlazorBarcodeScanner.Services;
using Microsoft.AspNetCore.Components.WebView;
using System.Windows.Input;
using ServiceProvider = MauiBlazorBarcodeScanner.Extensions.ServiceProvider;

namespace MauiBlazorBarcodeScanner;

public partial class CameraScannerView : ContentView
{
    private readonly ICameraControlService _cameraControlService;

    public static readonly BindableProperty ScanDelayProperty = BindableProperty.Create(nameof(ScanDelay), typeof(int), typeof(CameraScannerView), 2, propertyChanged: OnIsScanDelayPropertyChanged);
    public static readonly BindableProperty IsFrontCameraProperty = BindableProperty.Create(nameof(IsFrontCamera), typeof(bool), typeof(CameraScannerView), false, propertyChanged: OnIsFrontCameraPropertyChanged);
    public static readonly BindableProperty IsCameraOnProperty = BindableProperty.Create(nameof(IsCameraOn), typeof(bool), typeof(CameraScannerView), false, propertyChanged: OnIsCameraOnPropertyChanged);
    public static readonly BindableProperty IsScanningProperty = BindableProperty.Create(nameof(IsScanning), typeof(bool), typeof(CameraScannerView), false, propertyChanged: OnIsScanningPropertyChanged);

    public static readonly BindableProperty BarcodeDetectedCommandProperty = BindableProperty.Create(nameof(BarcodeDetectedCommand), typeof(ICommand), typeof(CameraScannerView), null);
   
    public int ScanDelay
    {
        get => (int)GetValue(ScanDelayProperty);
        set
        {
            SetValue(ScanDelayProperty, value);
            _cameraControlService.SetScanDelay(value);
        }
    }

    public bool IsFrontCamera
    {
        get => (bool)GetValue(IsFrontCameraProperty);
        set
        {
            SetValue(IsFrontCameraProperty, value);
            _cameraControlService.SetFrontCamera(value);
        }
    }

    public bool IsCameraOn
    {
        get => (bool)GetValue(IsCameraOnProperty);
        set
        {
            SetValue(IsCameraOnProperty, value);
            _cameraControlService.SetCameraOn(value);
        }
    }

    public bool IsScanning
    {
        get => (bool)GetValue(IsScanningProperty);
        set
        {
            SetValue(IsScanningProperty, value);
            _cameraControlService.SetScanning(value);
            if (value && !IsCameraOn)
            {
                IsCameraOn = true;
            }
        }
    }

    public ICommand BarcodeDetectedCommand
    {
        get => (ICommand)GetValue(BarcodeDetectedCommandProperty);
        set => SetValue(BarcodeDetectedCommandProperty, value);
    }

    public event EventHandler<string> BarcodeDetected;

    public CameraScannerView()
    {
        _cameraControlService = ServiceProvider.Current.GetRequiredService<ICameraControlService>();

        InitializeComponent();

        _blazorWebView.BlazorWebViewInitializing += BlazorWebViewInitializing;
        _blazorWebView.BlazorWebViewInitialized += BlazorWebViewInitialized;

        _cameraControlService.BarcodeDetected += _cameraControlService_BarcodeDetected;
    }

    private void _cameraControlService_BarcodeDetected(object sender, string e)
    {
        BarcodeDetectedCommand?.Execute(e);
        BarcodeDetected?.Invoke(this, e);
    }

    private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e);
    private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e);

    public void TestScan()
    {
        _cameraControlService.TestScan();
    }

    public static void OnIsScanDelayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var target = (CameraScannerView)bindable;
        target.ScanDelay = (int)newValue;
    }

    private static void OnIsFrontCameraPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var target = (CameraScannerView)bindable;
        if (target.IsFrontCamera != (bool)newValue)
        {
            target.IsFrontCamera = (bool)newValue;
        }
    }

    private static void OnIsCameraOnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var target = (CameraScannerView)bindable;
        if (target.IsCameraOn != (bool)newValue)
        {
            target.IsCameraOn = (bool)newValue;
        }
    }

    private static void OnIsScanningPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var target = (CameraScannerView)bindable;
        if (target.IsScanning != (bool)newValue)
        {
            target.IsScanning = (bool)newValue;
        }
    }
}