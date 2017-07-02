using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class Game
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("players")]
    public int Players { get; set; }

    [XmlAttribute("gameId")]
    public int GameId { get; set; }
    
    [XmlElement("supergame", typeof(SuperGame))] 
     public List<SuperGame> SuperGames { get; set; }
    
    [XmlElement("room")] 
    public List<Room> Rooms { get; set; }
}
