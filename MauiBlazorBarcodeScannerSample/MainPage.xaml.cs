namespace MauiBlazorBarcodeScannerSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        buttonCameraFacing.Text = "Camera: Back";
        buttonCameraOn.Text = "Camera: Off";
        buttonCameraScanning.Text = "Scanning: Off";
    }

    private void CameraScanner_BarcodeDetected(object sender, string e)
    {
        LastBarcode.Text = e + " " + DateTime.Now.ToString("HH:mm:ss");
        /*Dispatcher.Dispatch(() =>
        {
            LastBarcode.Text = e;
        });*/
    }

    private void buttonCameraFacing_Clicked(object sender, EventArgs e)
    {
        var isFront = buttonCameraFacing.Text == "Camera: Front";
        CameraScanner.IsFrontCamera = !isFront;

        buttonCameraFacing.Text = isFront ? "Camera: Back" : "Camera: Front";
    }

    private void buttonCameraOn_Clicked(object sender, EventArgs e)
    {
        var isOn = buttonCameraOn.Text == "Camera: On";
        CameraScanner.IsCameraOn = !isOn;

        buttonCameraOn.Text = isOn ? "Camera: Off" : "Camera: On";
    }

    private void buttonCameraScanning_Clicked(object sender, EventArgs e)
    {
        var isScanning = buttonCameraScanning.Text == "Scanning: On";
        CameraScanner.IsScanning = !isScanning;

        var isOn = buttonCameraOn.Text == "Camera: On";
        if (!isOn)
        {
            buttonCameraOn.Text = "Camera: On";
        }

        buttonCameraScanning.Text = isScanning ? "Scanning: Off" : "Scanning: On";
    }

    private void buttonTestScan_Clicked(object sender, EventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            CameraScanner.TestScan();
        });
    }
}
