using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Settings
{
    public class ApplicationSettings
    {
        public FileUploadSettings FileUploadSettings { get; set; }
    }

    public class FileUploadSettings
    {
        public string FileUploadFolder { get; set; }
    }
}
