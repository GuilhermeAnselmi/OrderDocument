using Foundation;
using UIKit;

namespace OrderDocument;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
        {
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.DarkContent, false);
        }

        return base.FinishedLaunching(application, launchOptions);
    }
}
