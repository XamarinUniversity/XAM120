using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Phoneword.iOS
{
	public class PhoneDialer : IDialer
	{
		public Task<bool> DialAsync(string number)
		{
			return Task.FromResult(
				UIApplication.SharedApplication.OpenUrl(
				new NSUrl("tel:" + number))
			);
		}
	}
}
