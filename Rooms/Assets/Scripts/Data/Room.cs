using System.Xml.Serialization;

namespace Data
{
	public class Room  {

		[XmlText]
		public string Name { get; set; }

		[XmlAttribute("players")]
		public int Players { get; set; }

		[XmlAttribute("maxPlayers")]
		public int MaxPlayers { get; set; }
	
		[XmlAttribute("price")]
		public decimal Price { get; set; }
	}
}
