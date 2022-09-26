namespace MauiBlazorBarcodeScanner.Services;

/// <summary>
/// Inject this into you page or view model to interace with the webview camera and scanner.
/// </summary>
public interface ICameraControlService
{
    event EventHandler<string> BarcodeDetected;

    void SetScanDelay(int value);
    void SetFrontCamera(bool value);
    void SetCameraOn(bool value);
    void SetScanning(bool value);

    /// <summary>
    /// Exists solely to check the JS interop is working.
    /// </summary>
    void TestScan();

    /// <summary>
    /// Perform disposal properly.
    /// </summary>
    void Dispose();
}

public class CameraControlService : ICameraControlService
{
    public event EventHandler<int> ScanDelayChanged;
    public event EventHandler<bool> IsFrontCameraChanged;
    public event EventHandler<bool> IsCameraOnChanged;
    public event EventHandler<bool> IsScanningChanged;

    public event EventHandler<string> BarcodeDetected;

    public event EventHandler TestScanRequested;
    public event EventHandler DisposeRequested;

    public void SetScanDelay(int value)
    {
        ScanDelayChanged?.Invoke(this, value);
    }

    public void SetFrontCamera(bool value)
    {
        IsFrontCameraChanged?.Invoke(this, value);
    }

    public void SetCameraOn(bool value)
    {
        IsCameraOnChanged?.Invoke(this, value);
    }

    public void SetScanning(bool value)
    {
        IsScanningChanged?.Invoke(this, value);
    }

    public void RaiseBarcodeDetected(string barcode)
    {
        BarcodeDetected?.Invoke(this, barcode);
    }

    public void TestScan()
    {
        TestScanRequested?.Invoke(this, new EventArgs());
    }

    public void Dispose()
    {
        DisposeRequested?.Invoke(this, new EventArgs());
    }
}