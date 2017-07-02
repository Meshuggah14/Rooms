using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{

	public Dropdown DropdownGames;
	public RoomsScrollList RoomsScrollList;
	
	// Use this for initialization
	void Start ()
	{
		DataController.Instance.onParseFinished = FillGamesItems;
		DropdownGames.onClick = RoomsScrollList.Refresh;
		GetServerData();
		//StartCoroutine(Updater());
	}

	private void GetServerData()
	{
		StartCoroutine(DataController.Instance.Request());
	}

	public void FillGamesItems()
	{
		Dictionary<int, string> items = new Dictionary<int, string>();

		RoomsContainer rooms = DataController.Instance.Rooms;
		
		if (rooms != null && rooms.Games.Count > 0)
		{
			foreach (var game in rooms.Games)
			{
				if(ConfigController.Instance.Config.available.Contains(game.GameId))
					items.Add(game.GameId,game.Name);
			}
			
			DropdownGames.FillDropDown(items);
			
		}
		
	}
	
	private IEnumerator Updater()
	{
		while (true)
		{
			StartCoroutine(DataController.Instance.Request());
			yield return new WaitForSeconds(30);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
