using System;
using System.Runtime.Serialization;

namespace wslyvh.Core.Samples.Service
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime Created { get; set; }

        [DataMember]
        public OrderStatus Status { get; set; }
    }
}
