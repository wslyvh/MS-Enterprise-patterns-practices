namespace wslyvh.Core.Service
{
    /// <summary>
    /// Constants for ServiceContract Namespaces.
    /// </summary>
    internal static class Namespaces
    {
        // e.g. http://wslyvh.core.com/v1/OrderService/GetOrders
        public const string BaseNamespace = "http://wslyvh.core.com/";
        public const string v1 = "v1/";
        public const string v2 = "v2/";

        public const string ServiceV1 = BaseNamespace + v1 + "IService/";
        public const string ServiceV2 = BaseNamespace + v2 + "IService/";
    }
}
