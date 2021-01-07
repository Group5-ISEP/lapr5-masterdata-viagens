using System.Xml.Serialization;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.ImportData.XML
{
    [XmlRoot("GlDocumentInfo", Namespace = "Opt.GL.BusinessLogic")]
    public class Document
    {
        [XmlElement(ElementName = "world")]
        public World World { get; set; }
    }

    public class World
    {
        [XmlElement(ElementName = "GlDocument")]
        public GlDocument GlDocument { get; set; }
    }

    public class GlDocument
    {
        [XmlElement(ElementName = "GlDocumentSchedule")]
        public GlDocumentSchedule GlDocumentSchedule { get; set; }
    }

    public class GlDocumentSchedule
    {
        [XmlElement(ElementName = "Schedule")]
        public Schedule Schedule { get; set; }
    }

    public class Schedule
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Trip")]
        public List<DocTrip> Trips { get; set; }
    }

    public class DocTrip
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAttribute("Orientation")]
        public string Orientation { get; set; }
        [XmlAttribute("Line")]
        public string Line { get; set; }
        [XmlAttribute("Path")]
        public string Path { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "PassingTime")]
        public List<DocPassingTime> PassingTimes { get; set; }
    }

    public class DocPassingTime
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("Time")]
        public int Time { get; set; }

        [XmlAttribute("Node")]
        public string Node { get; set; }
    }
}