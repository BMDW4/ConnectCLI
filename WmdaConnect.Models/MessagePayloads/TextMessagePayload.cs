﻿using System;
using System.Collections.Generic;
using WmdaConnect.Models.IdTypes;

namespace WmdaConnect.Models.MessagePayloads
{
    public class TextMessagePayload : IHasAttachmentGuids
    {
        /// <summary>
        /// Patient identification P_ID Opt 17
        /// </summary>
        public PatientId Patient { get; set; }

        /// <summary>
        /// Global registration identifier for donor D_GRID Opt 19
        /// </summary>
        public DonorSelection Donor { get; set; }

        /// <summary>
        /// Text. Note that text over 1200 characters will be truncated if recipient is EMDIS
        /// </summary>
        /// <remarks>Note that text over 1200 characters will be truncated if recipient is EMDIS</remarks>
        public string Text { get; set; }

        public List<Guid> AttachmentGuids { get; set; }
    }
}
