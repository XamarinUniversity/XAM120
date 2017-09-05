using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword
{
    public interface IDialer
    {
        Task<bool> DialAsync(string number);
    }
}
