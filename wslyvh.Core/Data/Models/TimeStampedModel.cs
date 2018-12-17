using System;

namespace wslyvh.Core.Data.Models
{
    [Serializable]
    public abstract class TimeStampedModel : ModelBase
    {
        protected TimeStampedModel()
        {
            Created = DateTime.Now;
        }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
