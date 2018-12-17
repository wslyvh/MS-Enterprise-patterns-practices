using System.Runtime.Serialization;

namespace wslyvh.Core.Samples.Service
{
    [DataContract]
    public enum OrderStatus
    {
        [EnumMember]
        New = 0,

        [EnumMember]
        InProcess = 1,

        [EnumMember]
        Done = 2
    }
}
