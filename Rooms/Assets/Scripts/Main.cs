using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(Updater());
		//RoomsContainer rc = GameController.Instance.Load();
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
