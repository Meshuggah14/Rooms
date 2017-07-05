using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Data
{
    [XmlRoot("data")]
    public class RoomsContainer : BaseData
    {
        [XmlArray("settings")]
        [XmlArrayItem("param")]
        public List<Param> Settings { get; set; }

        [XmlArray("roomlist")]
        [XmlArrayItem("game")]
        public List<Game> Games { get; set; }

        public void SortGamesByPlayers()
        {
            Games = Games.OrderByDescending(g => g.Players).ToList();
        }

        public void SortSuperGamesByPrice()
        {
            foreach (var game in Games)
            {
                game.SuperGames = game.SuperGames.OrderByDescending(sg => sg.Price).ToList();
            }
        }

        public void SortRoomsByName()
        {
            foreach (var game in Games)
            {
                game.Rooms = game.Rooms.OrderByDescending(rm => rm.Name).ToList();
            }
        }

        public Dictionary<int, string> GetAvaliableGameNames(int[] availables)
        {
            Dictionary<int, string> items = new Dictionary<int, string>();

            if (Games != null && Games.Count > 0)
            {
                foreach (var game in Games)
                {
                    if (availables.Contains(game.GameId))
                        items.Add(game.GameId, game.Name);
                }
            }

            return items;
        }

        public Game GetGameById(int gameId)
        {
            return Games.FirstOrDefault(g => g.GameId == gameId);
        }

        public void ApplyCommonSort()
        {
            SortGamesByPlayers();
            SortSuperGamesByPrice();
            SortRoomsByName();
        }
    }

    public class Param
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
