using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SuperGame {

	
	[XmlAttribute("current")]
	public int Current { get; set; }

	[XmlAttribute("max")]
	public int Max { get; set; }
	
	[XmlAttribute("price")]
	public decimal Price { get; set; }
}
