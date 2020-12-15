using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedFolderUpload
{
    public class FileDetailModel
    {
        public Guid Id { get; set; }
        public string DocumentName { get; set; }
        public string DocType { get; set; }
        public string DocUrl { get; set; }
    }
}
