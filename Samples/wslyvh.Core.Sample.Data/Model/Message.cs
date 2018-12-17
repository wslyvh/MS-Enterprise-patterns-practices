using System;
using wslyvh.Core.Data.Models;

namespace wslyvh.Core.Sample.Data.Model
{
    [Serializable]
    public class Message : TimeAndUserStampedModel
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public virtual Category Category { get; set; } 
    }
}
