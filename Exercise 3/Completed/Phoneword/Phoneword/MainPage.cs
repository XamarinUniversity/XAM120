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
            switch(Device.RuntimePlatform)
            {
                case Device.iOS:
                    this.Padding = new Thickness(20, 40, 20, 20);
                    break;
                default:
                    this.Padding = new Thickness(20);
                    break;
            }

            StackLayout panel = new StackLayout { Spacing = 15 };

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

		void OnTranslate(object sender, EventArgs e)
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

		async void OnCall(object sender, EventArgs e)
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