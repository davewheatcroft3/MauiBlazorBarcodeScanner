using MauiBlazorBarcodeScanner.Extensions;

namespace MauiBlazorBarcodeScannerSample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.AddMauiBlazorBarcodeScanner(true);

        return builder.Build();
	}
}
