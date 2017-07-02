using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomBlock : MonoBehaviour
{

	public Image PlayerIcon;

	public Text PlayerAmount;

	public Text RoomName;

	public Button JoinButton;
	
	private ModalPanel _modalPanel;
	private UnityAction _btnJoinAction;
	private UnityAction _btnCancelAction;
	
	
	void Awake () {
		_modalPanel = ModalPanel.Instance ();

		_btnJoinAction = new UnityAction (JoinAction);
		_btnCancelAction = new UnityAction (CancelAction);
	}

	private void CancelAction()
	{
		
	}

	private void JoinAction()
	{
		
	}


	// Use this for initialization
	void Start () {
		
	}

	public void Setup(Room room, Sprite playerIcon)
	{
		RoomName.text = room.Name;
		PlayerAmount.text = room.Players.ToString();
		if(playerIcon != null)
			PlayerIcon.gameObject.GetComponent<SpriteRenderer>().sprite = playerIcon;
		if (room.MaxPlayers > room.Players)
			EnableBtn(JoinButton, room.Name);
		else
		{
			DisableBtn(JoinButton);
		}
	}

	private void EnableBtn(Button btn, string roomName)
	{
		btn.enabled = true;
		btn.GetComponent<Button>().interactable = true; 
		btn.GetComponentInChildren<Text>().text = "Join";
		btn.GetComponentInChildren<Text>().color = Color.black;
	
		JoinButton.onClick.AddListener (
			() => BtnClick(roomName));
	}

	void BtnClick(string roomName)
	{
		 _modalPanel.Choice ("Do you want to join " + roomName + "?", _btnJoinAction, _btnCancelAction);
	}

	private void DisableBtn(Button btn)
	{
		btn.enabled = false;
		btn.GetComponent<Button>().interactable = false; 
		btn.GetComponentInChildren<Text>().text = "FULL";
		btn.GetComponentInChildren<Text>().color = Color.white;
		//var colors = btn.GetComponent<Button> ().colors;
		//colors.disabledColor = Color.black;
		//btn.GetComponent<Button> ().colors = colors;
		
	}
	
}
