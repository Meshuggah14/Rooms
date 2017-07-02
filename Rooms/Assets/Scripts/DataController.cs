using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;


public class DataController : MonoBehaviour
{
	public RoomsContainer Rooms;

	public delegate void OnParseFinished();

	public OnParseFinished onParseFinished;
	
	private static DataController _datacontroller;
	//private ConfigController _configController;


	public static DataController Instance
	{
		get
		{
			if (_datacontroller == null)
			{
				var dc = new GameObject("DataController");
				dc.AddComponent<DataController>();
			}

			return _datacontroller;
		}
	}

	private void Awake()
	{
		_datacontroller = this;
		//_configController = ConfigController.Instance;// gameObject.AddComponent<ConfigController>();
	}

	public IEnumerator Request()
	{
		string url = ConfigController.Instance.Config.urldata; // _configController.Config.urldata;
		WWW www = new WWW(url);
		
		yield return www;

		if (www.error != null)
		{
			print("Error getting the data from server: " + www.error);
			yield break;
		}

		Rooms = ParaseData(www.text);
		SortGamesByPlayers();
		SortSuperGamesByPrice();
		SortRoomsByName();
		onParseFinished();
		
	}

	private RoomsContainer ParaseData(string text)
	{
		var serializer = new XmlSerializer(typeof(RoomsContainer));
		return serializer.Deserialize(new StringReader(text)) as RoomsContainer;
	}

	private void SortGamesByPlayers()
	{
		Instance.Rooms.Games = Instance.Rooms.Games.OrderByDescending(g => g.Players).ToList();
	}

	private void SortSuperGamesByPrice()
	{
		foreach (var game in Instance.Rooms.Games)
		{
			game.SuperGames = game.SuperGames.OrderByDescending(sg => sg.Price).ToList();
		}
	}

	private void SortRoomsByName()
	{
		foreach (var game in Instance.Rooms.Games)
		{
			game.Rooms = game.Rooms.OrderByDescending(rm => rm.Name).ToList();
		}
	}
	
}
