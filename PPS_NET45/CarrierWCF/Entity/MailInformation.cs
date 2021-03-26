using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Entity
{
    public class MailInformation
    {
        public AttachFile[] AttachFiles;
        public string Body;
        public int CodePage;
        public MailFrom From;
        public bool IsBodyHtml;
        public MailTo Object;
        public string Subject;
    }

    public class AttachFile
    {
        public string Base64Content;
        public string FileName;
        public bool Inline;
    }

    public class MailFrom
    {
        public string Address;
        public string Name;
    }

    public class MailTo
    {
        public string[] BccAddresses;
        public string[] CcAddresses;
        public string[] ToAddresses;
    }
}
