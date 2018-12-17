using System;
using System.Xml.Serialization;

namespace wslyvh.Core.Samples.ServiceClient.Entities
{
    [XmlType(TypeName = "search")]
    public class EventfulData
    {
        [XmlElement(ElementName = "total_items")]
        public string TotalItems { get; set; }

        [XmlElement(ElementName = "page_size")]
        public string PageSize { get; set; }

        [XmlElement(ElementName = "page_count")]
        public string PageCount { get; set; }

        [XmlElement(ElementName = "page_number")]
        public string PageNumber { get; set; }

        [XmlElement(ElementName = "page_items")]
        public string PageItems { get; set; }

        [XmlElement(ElementName = "first_item")]
        public string FirstItem { get; set; }

        [XmlElement(ElementName = "last_item")]
        public string LastItem { get; set; }

        [XmlElement(ElementName = "search_time")]
        public string SearchTime { get; set; }

        [XmlArray(ElementName = "events")]
        public EventfulEvent[] Events { get; set; }
    }

    [Serializable, XmlType(TypeName = "event")]
    public class EventfulEvent
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "start_time")]
        public string StartTime { get; set; }

        [XmlElement(ElementName = "stop_time")]
        public string EndTime { get; set; }

        [XmlElement(ElementName = "venue_name")]
        public string Venue { get; set; }

        [XmlElement(ElementName = "city_name")]
        public string City { get; set; }

        [XmlElement(ElementName = "country_name")]
        public string Country { get; set; }
    }
}
