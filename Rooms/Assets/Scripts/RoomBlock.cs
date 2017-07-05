using System;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class RoomBlock : MonoBehaviour
{
	[SerializeField] private RawImage _playerIcon;
	[SerializeField] private Text _playerAmount;
	[SerializeField] private Text _roomName;
	[SerializeField] private Button _joinButton;

	public void Setup(Room room, Texture2D playerIcon, Action<string> onClick)
	{
		_roomName.text = room.Name;
		_playerAmount.text = room.Players.ToString();

		if (playerIcon != null)
			_playerIcon.GetComponent<RawImage>().texture = playerIcon;

		if (room.MaxPlayers > room.Players)
		{
			EnableBtn(_joinButton, onClick);
		}
		else
		{
			DisableBtn(_joinButton);
		}
	}

	private void EnableBtn(Button btn, Action<string> onClick)
	{
		btn.enabled = true;
		btn.GetComponent<Button>().interactable = true; 
		btn.GetComponentInChildren<Text>().text = "Join";
		btn.GetComponentInChildren<Text>().color = Color.black;
		
		_joinButton.onClick.RemoveAllListeners();
		_joinButton.onClick.AddListener(() => { onClick(_roomName.text); });
	}

	private void DisableBtn(Button btn)
	{
		btn.enabled = false;
		btn.GetComponent<Button>().interactable = false;
		btn.GetComponentInChildren<Text>().text = "FULL";
		btn.GetComponentInChildren<Text>().color = Color.white;

		var colorBlock = btn.colors;
		colorBlock.disabledColor = Color.black;
		btn.colors = colorBlock;
	}
}
