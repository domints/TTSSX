﻿
using Foundation;
using Syncfusion.ListView.XForms.iOS;
using UIKit;

namespace TTSSX.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
            SfListViewRenderer.Init();
            LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
