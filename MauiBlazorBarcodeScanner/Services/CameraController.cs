using Microsoft.JSInterop;

namespace MauiBlazorBarcodeScanner.Services
{
    /// <summary>
    /// Essentially the bridge between C# and JavaScript.
    /// </summary>
    internal class CameraController : IAsyncDisposable
    {
        private readonly IJSRuntime JSRuntime;
        private readonly ICameraControlService _cameraControlService;

        private DotNetObjectReference<CameraController> ThisRef = null!;
        private IJSObjectReference JSObject = null!;

        public CameraController(IJSRuntime runtime, ICameraControlService cameraControlService)
        {
            JSRuntime = runtime;
            _cameraControlService = cameraControlService;
        }

        public async Task InitializeAsync(string elementId)
        {
            ThisRef = DotNetObjectReference.Create(this);
            JSObject = await JSRuntime.InvokeAsync<IJSObjectReference>("window.createCamera");
            await JSObject.InvokeAsync<string?>("initialize", elementId, ThisRef);
        }

        public async Task<bool> SetScanDelayAsync(int scanDelayInSeconds)
        {
            return await JSObject.InvokeAsync<bool>("setScanDelay", scanDelayInSeconds);
        }

        public async Task<bool> SetFrontCameraAsync(bool isFront)
        {
            return await JSObject.InvokeAsync<bool>("setFrontCamera", isFront);
        }

        public async Task<bool> SetCameraOnAsync(bool isOn)
        {
            return await JSObject.InvokeAsync<bool>("setCameraOn", isOn);
        }

        public async Task<bool> SetScanningAsync(bool isScanning)
        {
            return await JSObject.InvokeAsync<bool>("setScanning", isScanning);
        }

        public async Task TestScanAsync()
        {
            await JSObject.InvokeVoidAsync("testScan");
        }

        [JSInvokable]
        public Task BarcodeFoundAsync(string barcode)
        {
            var instance = (_cameraControlService as CameraControlService);
            if (instance == null)
            {
                throw new Exception("Do not use a custom implementation of the CameraControlService");
            }

            instance.RaiseBarcodeDetected(barcode);
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await JSObject.InvokeVoidAsync("dispose");
            await JSObject.DisposeAsync();
            ThisRef?.Dispose();
        }
    }
}
