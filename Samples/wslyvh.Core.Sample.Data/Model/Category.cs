using System;
using wslyvh.Core.Data.Models;

namespace wslyvh.Core.Sample.Data.Model
{
    [Serializable]
    public class Category : TimeAndUserStampedModel
    {
        public string Name { get; set; }
    }
}
