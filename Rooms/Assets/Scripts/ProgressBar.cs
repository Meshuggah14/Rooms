using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

	public Text BarText;
	public Image Line;

//	private float _current;
//	private float _max;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateProgressBar(float current, float max)
	{
		float percent = current / max;
		Line.rectTransform.localScale = new Vector3(percent, 1, 1);
		BarText.text = current + "/" + max;
	}
}
