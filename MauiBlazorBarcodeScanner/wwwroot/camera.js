function Camera() {
    var self = this;

    self.initialize = function (elementId, cSharpRefControl) {
        var width = window.innerWidth;
        var height = window.innerHeight;

        self.cSharpRefControl = cSharpRefControl;

        Html5Client.init(elementId, width, height, false);
        Html5Client.attach(self.onBarcodeDetected);

        return null;
    };

    self.onBarcodeDetected = function (barcode) {
        console.log(`Code matched = ${barcode}`);

        self.cSharpRefControl.invokeMethodAsync('BarcodeFoundAsync', barcode)
            .then(() => {
                console.log('Back from C#');
            })
            .catch(err => console.log(err));
    };

    self.setScanDelay = function (value) {
        try {
            Html5Client.state.scanDelay = value;
            return true;
        } catch (err) {
            console.log(err);
            return false;
        }
    };

    self.setFrontCamera = function (value) {
        try {
            Html5Client.setFrontCamera(value);
            return true;
        } catch (err) {
            console.log(err);
            return false;
        }
    };

    self.setCameraOn = function (value) {
        try {
            if (value && !Html5Client.state.isOn) {
                Html5Client.start();
            }
            else if (Html5Client.state.isOn && !value) {
                Html5Client.stop();
            }
            return true;
        } catch (err) {
            console.log(err);
            return false;
        }
    };

    self.setScanning = function (value) {
        try {
            Html5Client.setScanning(value);
            return true;
        } catch (err) {
            console.log(err);
            return false;
        }
    };

    self.testScan = function () {
        self.onBarcodeDetected('testscan1234');
    };

    self.dispose = function () {
        Html5Client.dispose();
    };
}
