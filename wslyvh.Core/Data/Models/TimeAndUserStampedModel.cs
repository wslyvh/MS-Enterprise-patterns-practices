using System;

namespace wslyvh.Core.Data.Models
{
    [Serializable]
    public abstract class TimeAndUserStampedModel : TimeStampedModel
    {
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
