using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomBlock : MonoBehaviour
{

	public Image PlayerIcon;

	public Text PlayerAmount;

	public Text RoomName;

	public Button JoinButton;
	
	// Use this for initialization
	void Start () {
		JoinButton.onClick.AddListener (BtnClick);
	}

	public void Setup(Room room, Sprite playerIcon)
	{
		RoomName.text = room.Name;
		PlayerAmount.text = room.Players.ToString();
		if(playerIcon != null)
			PlayerIcon.gameObject.GetComponent<SpriteRenderer>().sprite = playerIcon;
	}
	public void BtnClick()
	{
		//TODO
	}
	
}
