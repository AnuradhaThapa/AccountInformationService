using System;
using System.Collections.Generic;
using System.Text;

namespace AccountInformationService.Core.Entities
{
    public class FileLogDetailsEntity
    {
        public virtual string FilePath { get; set; }
        public virtual string FolderPath { get; set; }
    }
}
