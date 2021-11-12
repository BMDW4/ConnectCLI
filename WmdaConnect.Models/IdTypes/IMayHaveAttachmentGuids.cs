using System;
using System.Collections.Generic;

namespace WmdaConnect.Models.IdTypes
{
    public interface IMayHaveAttachmentGuids
    {
        public List<Guid> AttachmentGuids { get; set; }
    }
}
