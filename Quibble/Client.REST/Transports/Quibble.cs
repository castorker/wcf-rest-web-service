using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client.REST.Transports
{
    [XmlRoot(Namespace = "http://schemas.datacontract.org/2004/07/Data")]
    public class Quibble
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }
}
