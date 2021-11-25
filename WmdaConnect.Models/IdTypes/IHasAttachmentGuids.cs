using System;
using System.Collections.Generic;

namespace WmdaConnect.Models.IdTypes
{
    public interface IHasAttachmentGuids
    {
        public List<Guid> AttachmentGuids { get; set; }
    }
}
