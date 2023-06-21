using Microsoft.Extensions.Logging;

namespace OrderDocument;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		InitializeParametersApp();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
	}

	private static void InitializeParametersApp()
	{
		var path = $"{FileSystem.AppDataDirectory}/documents/";

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
    }
}
