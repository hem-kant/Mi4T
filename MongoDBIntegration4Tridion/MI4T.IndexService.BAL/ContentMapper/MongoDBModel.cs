using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Xml.Serialization;

namespace MI4T.IndexService.BAL.ContentMapper
{
    [Serializable()]
    [XmlRoot(ElementName = "Content")]
    public class MongoDBModel
    {
         
        [XmlElement("title")]
        public string title { get; set; }

        [XmlElement("imageurl")]
        public string imageurl { get; set; }

        [XmlElement("description")]
        public string description { get; set; }

        [XmlElement("ItemURI")]
        public string ItemURI { get; set; }

        [XmlElement("publicationID")]
        public string publicationID { get; set; }

    }

 
}
