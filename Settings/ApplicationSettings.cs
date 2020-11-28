using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Settings
{
    public class ApplicationSettings
    {
        public FileUploadSettings FileUploadSettings { get; set; }
        public EmailSettings EmailSettings { get; set; }
    }

    public class FileUploadSettings
    {
        public string FileUploadFolder { get; set; }
    }

    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailAccount { get; set; }
        public string EmailPassword { get; set; }
        public string EmailName { get; set; }
    }
}
