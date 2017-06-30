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

	private static DataController _datacontroller;
	private ConfigController _configController;


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
		_configController = gameObject.AddComponent<ConfigController>();
	}

	public IEnumerator Request()
	{
		string url = _configController.Config.urldata;
		WWW www = new WWW(url);
		
		yield return www;

		if (www.error != null)
		{
			print("Error getting the data from server: " + www.error);
			yield break;
		}

		Rooms = ParaseData(www.text);

	}

	private RoomsContainer ParaseData(string text)
	{
		var serializer = new XmlSerializer(typeof(RoomsContainer));
		return serializer.Deserialize(new StringReader(text)) as RoomsContainer;
	}
	
}
