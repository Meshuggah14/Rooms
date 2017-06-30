using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("data")]
public class RoomsContainer
{

	[XmlArray("roomlist")]
	[XmlArrayItem("game")] 
	public List<Game> Games { get; set; }

}
