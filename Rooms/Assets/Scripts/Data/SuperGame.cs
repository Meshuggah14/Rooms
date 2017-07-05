using System.Xml.Serialization;

namespace Data
{
	public class SuperGame {
		[XmlAttribute("current")]
		public int Current { get; set; }

		[XmlAttribute("max")]
		public int Max { get; set; }
	
		[XmlAttribute("price")]
		public decimal Price { get; set; }
	}
}
