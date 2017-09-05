using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Phoneword.Windows;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]

namespace Phoneword.Windows
{
    public class PhoneDialer : IDialer
    {
        public async Task<bool> DialAsync(string number)
        {
            var dialog = new MessageDialog("Connected to " + number) {
                Title = "Phone Dialer Simulator"
            };
            dialog.Commands.Add(new UICommand { Label = "Disconnect", Id = 0 });
            await dialog.ShowAsync();

            return true;
        }
    }
}