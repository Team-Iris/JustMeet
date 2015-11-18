using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustMeet.Common.Constants
{
    public class GoogleDriveConstants
    {
        // https://drive.google.com/uc?export=view&id={fileId}
        // https://drive.google.com/uc?id=FILE-ID
        public const string PremanentLinkPrefix = @"https://drive.google.com/uc?export=view&id=";
        public const int DefaultListFilesIDsCount = 1000;
    }
}
