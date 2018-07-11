using System.Threading.Tasks;

namespace Phoneword
{
    public interface IDialer
    {
        Task<bool> DialAsync(string number);
    }
}
