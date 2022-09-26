var Html5Client = {
    state: {
        facingMode: "environment",
        height: 420,
        width: 640,
        isOn: false,
        isScanning: false,
        onDetected: null,
        scanDelay: 2,
        pauseScanDetection: false
    },

    scanner: null,

    init: function (elementId, width, height, debug) {
        this.state.width = width;
        this.state.height = height;

        document.getElementById(elementId).style.width = width;
        document.getElementById(elementId).style.height = height;

        Html5Client.scanner = new Html5Qrcode(elementId, debug);
    },

    onScanSuccess: function (decodedText, decodedResult) {
        if (!Html5Client.state.pauseScanDetection) {
            Html5Client.state.onDetected(decodedText);

            if (Html5Client.state.scanDelay > 0) {
                Html5Client.state.pauseScanDetection = true;
                setTimeout(() => {
                    Html5Client.state.pauseScanDetection = false;
                }, Html5Client.state.scanDelay * 1000);
            }
        }
        else {
            console.log('Scanning paused');
        }
    },

    onScanFailure: function (error) {
        //console.warn(`Code scan error = ${error}`);
    },

    getConfig: function () {
        let aspectRatio;
        if (this.state.width > this.state.height) {
            aspectRatio = this.state.width / this.state.height;
        }
        else {
            aspectRatio = this.state.height / this.state.width;
        }

        let config = {
            fps: 10,
            aspectRatio: aspectRatio,
            //qrbox: { width: 250, height: 250 },
            qrbox: (viewfinderWidth, viewfinderHeight) => {
                let width = Math.floor(viewfinderWidth / 1.5);
                let height = Math.floor(viewfinderHeight / 3);
                return { width: width, height: height };
            }
        };
        return config;
    },

    getOptions: function () {
        let options = {
            facingMode: Html5Client.state.facingMode,
        };
        return options;
    },

    attach: function (onDetected) {
        Html5Client.state.onDetected = onDetected;
    },

    refresh: function () {
        Html5Client.stop(() => {
            Html5Client.start();
        });
    },

    start: function () {
        let options = Html5Client.getOptions();
        let config = Html5Client.getConfig();

        Html5Client.scanner.start(options, config, Html5Client.onScanSuccess, Html5Client.onScanFailure)
            .then(_ => {
                //console.log('started');
                Html5Client.state.isOn = true;
            }).catch((err) => {
                console.log(err);
            });
    },

    stop: function (onComplete) {
        Html5Client.scanner.stop()
            .then(_ => {
                //console.log('stopped');
                Html5Client.state.isOn = false;
                if (onComplete) {
                    onComplete();
                }
            }).catch((err) => {
                console.log(err);
            });
    },

    setScanning: function (value) {
        if (value) {
            if (Html5Client.scanner.getState() == Html5QrcodeScannerState.NOT_STARTED) {
                Html5Client.start();
            }
            else if (Html5Client.scanner.getState() == Html5QrcodeScannerState.PAUSED) {
                Html5Client.scanner.resume();
            }
        }
        else {
            if (Html5Client.scanner.getState() == Html5QrcodeScannerState.SCANNING) {
                // True is for also pausing video
                Html5Client.scanner.pause(true);
            }
        }
    },

    setFrontCamera: function (value) {
        Html5Client.state.facingMode = value == true ? "user" : "environment";
        if (Html5Client.state.isOn) {
            Html5Client.refresh();
        }
    },

    dispose: function () {
        if (Html5Client.state.isOn) {
            Html5Client.scanner.stop();
        }
        Html5Client.scanner = null;
    }
};