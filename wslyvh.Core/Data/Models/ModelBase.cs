using System;
using System.ComponentModel.DataAnnotations;

namespace wslyvh.Core.Data.Models
{
    [Serializable]
    public abstract class ModelBase
    {
        public ModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
    }
}
