using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SuperGameBlock : MonoBehaviour
{

	public ProgressBar ProgressBar;

	public Image CoinIcon;

	public Text Price;

	// Use this for initialization
	void Start () {
		
	}

	public void Setup(SuperGame superGame, Sprite coinIcon)
	{
		Price.text = superGame.Price.ToString();
		if (coinIcon != null)
			CoinIcon.gameObject.GetComponent<SpriteRenderer>().sprite = coinIcon;
		ProgressBar.UpdateProgressBar(superGame.Current, superGame.Max);
	}
	public void BtnClick()
	{
		//TODO
	}
	
}
