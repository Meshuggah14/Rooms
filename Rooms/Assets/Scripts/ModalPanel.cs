using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalPanel : MonoBehaviour {

	public Text Question;
	public Image IconImage;
	public Button JoinButton;
	public Button CancelButton;
	public GameObject ModalPanelObject;
    
	
	private static ModalPanel _modalPanel;
    
	public static ModalPanel Instance () {
		if (!_modalPanel) {
			_modalPanel = FindObjectOfType(typeof (ModalPanel)) as ModalPanel;
			if (!_modalPanel)
				Debug.LogError ("Only one ModalPanel in your scene.");
		}
        
		return _modalPanel;
	}

	public void Choice (string question, UnityAction joinEvent,  UnityAction cancelEvent) {
		ModalPanelObject.SetActive (true);
        
		JoinButton.onClick.RemoveAllListeners();
		JoinButton.onClick.AddListener (joinEvent);
		JoinButton.onClick.AddListener (ClosePanel);
        
		CancelButton.onClick.RemoveAllListeners();
		CancelButton.onClick.AddListener (cancelEvent);
		CancelButton.onClick.AddListener (ClosePanel);

		Question.text = question;

//		IconImage.gameObject.SetActive (true);
//		JoinButton.gameObject.SetActive (true);
//		CancelButton.gameObject.SetActive (true);
	}
	
	void ClosePanel()
	{
		ModalPanelObject.SetActive(false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
