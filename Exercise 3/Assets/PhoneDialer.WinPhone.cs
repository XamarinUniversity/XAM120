using System.Threading.Tasks;
using Phoneword.WinPhone81;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]

namespace Phoneword.WinPhone81
{
    public class PhoneDialer : IDialer
    {
        public Task<bool> DialAsync(string number)
        {
            try
            {
                Windows.ApplicationModel.Calls
                    .PhoneCallManager.ShowPhoneCallUI(number, "Phoneword");

                return Task.FromResult(true);
            }
            catch
            {
            }
            return Task.FromResult(false);
        }
    }
}