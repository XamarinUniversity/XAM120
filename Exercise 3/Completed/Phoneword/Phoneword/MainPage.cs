using System;
using Xamarin.Forms;

namespace Phoneword
{
	public class MainPage : ContentPage
	{
		readonly Entry phoneNumberText;
		readonly Button callButton;
		readonly Button translateButton;
		string translatedNumber;

		public MainPage()
		{
			this.Padding = new Thickness(20, Device.OnPlatform(40, 20, 20), 20, 20);

			// Optionally, if you want to experiment with using the ForPlatform.On system to
			// differentiate between Windows 8 Silverlight (WinPhone) and newer Windows platforms
			// (including UWP), here is an example to get you started.
			//this.Padding = ForPlatform.On(
			//	iOS: new Thickness(20, 40, 20, 20),
			//	defaultValue: new Thickness(20)
			//);

			// Additionally, if you want to experiment with using the ForIdiom.On system to
			// differentiate between different device form factors, here is an example to get you
			// started.
			//this.Padding = ForIdiom.On(
			//	phone: new Thickness(20),
			//	tablet: new Thickness(30),
			//	desktop: new Thickness(10),
			//	defaultValue: new Thickness(20)
			//);

			StackLayout panel = new StackLayout
			{
				Spacing = 15
			};

			panel.Children.Add(new Label
			{
				Text = "Enter a Phoneword:",
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
			});

			panel.Children.Add(phoneNumberText = new Entry
			{
				Text = "1-855-XAMARIN",
			});

			panel.Children.Add(translateButton = new Button
			{
				Text = "Translate"
			});

			panel.Children.Add(callButton = new Button
			{
				Text = "Call",
				IsEnabled = false,
			});

			translateButton.Clicked += OnTranslate;
			callButton.Clicked += OnCall;

			this.Content = panel;
		}

		private void OnTranslate(object sender, EventArgs e)
		{
			translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
			if (!string.IsNullOrEmpty(translatedNumber))
			{
				callButton.IsEnabled = true;
				callButton.Text = "Call " + translatedNumber;
			}
			else
			{
				callButton.IsEnabled = false;
				callButton.Text = "Call";
			}
		}

		private async void OnCall(object sender, EventArgs e)
		{
				if (await this.DisplayAlert(
					"Dial a Number",
					"Would you like to call " + translatedNumber + "?",
					"Yes",
					"No")) {
					var dialer = DependencyService.Get<IDialer>();
					if (dialer != null) {
						await dialer.DialAsync(translatedNumber);
					}
				}
		}
	}
}