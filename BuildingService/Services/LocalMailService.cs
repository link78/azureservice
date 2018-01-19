using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingService.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }


    public class LocalMailService:IMailService
    {
        public string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
            //"admin@company.com";
        public string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];
        //"noreply@mycompany.com";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail From {_mailFrom} to {_mailTo}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }


    public class CloudMailService : IMailService
    {
        public string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        //"admin@company.com";
        public string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail From {_mailFrom} to {_mailTo}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
