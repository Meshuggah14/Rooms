using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class RoomsScrollList : MonoBehaviour {

	public Transform ContentPanel;
	public GameObject PrefabRoomBock;
	public GameObject PrefabSuperGameBlock;
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void Refresh(int gameId)
	{
		RemoveItems ();
		Game game = DataController.Instance.Rooms.Games.FirstOrDefault(g => g.GameId == gameId);
		AddItems(game);
	}

	private void AddItems(Game game)
	{
		foreach (var superGame in game.SuperGames)
		{
			AddSuperGameBlock(superGame);
			foreach (var room in game.Rooms.Where(i=> i.Price == superGame.Price).ToList())
			{
				AddRoomBlock(room);
			}
		
		}
	}

	private void AddRoomBlock(Room room)
	{
		GameObject newblock = PutToPool(PrefabRoomBock);
		RoomBlock roomblock = newblock.GetComponent<RoomBlock>();
		roomblock.Setup(room, null);	
	}
	
	private void AddSuperGameBlock(SuperGame superGame)
	{
		GameObject newblock = PutToPool(PrefabSuperGameBlock);
		SuperGameBlock supergameBlock = newblock.GetComponent<SuperGameBlock>();
		supergameBlock.Setup(superGame, null);	
	}
	
	private GameObject PutToPool(GameObject prefab)
	{
		
		GameObject newblock = ObjectPool.Instance.GetObject(prefab);
        newblock.transform.SetParent(ContentPanel, false);
		return newblock;
	}


	private void RemoveItems()
	{
		for (int i = 0; i < ContentPanel.childCount; i++)
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
            ObjectPool.Instance.ReturnObject(toRemove);
		}	
//		while (ContentPanel.childCount > 0)
//		{
//			//TODO check loop
//			GameObject toRemove = transform.GetChild(0).gameObject;
//			ObjectPool.Instance.ReturnObject(toRemove);
//		}
	}
}
