using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Application.Interfaces.Logger
{
    public interface IAppLogger<T>
    {
        public void LogInformation(string message);
        public void LogWarning(string message);
        public void LogError(Exception ex, string message);
    }
}
