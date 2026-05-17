using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPayroll.Application.Common.Interfaces
{
    public interface ISmsService
    {
        Task SendAsync(string phone, string message, CancellationToken cancellationToken = default);
        Task SendLeaveStatusAsync(string phone, string employeeName, string leaveType, string status, CancellationToken cancellationToken = default);
    }

}
