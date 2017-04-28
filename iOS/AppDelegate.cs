using Foundation;
using FreshMvvm;
using UIKit;

namespace mondaynightclash.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			var repository = new DataService(FileAccess.GetLocalFilePath("stats.db3"));
			FreshIOC.Container.Register(repository);

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
