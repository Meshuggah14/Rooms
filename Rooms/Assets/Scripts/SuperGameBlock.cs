using System.Globalization;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class SuperGameBlock : MonoBehaviour
{
	[SerializeField] private Slider _progressBar;
	[SerializeField] private RawImage _coinIcon;
	[SerializeField] private Text _price;

	public void Setup(SuperGame superGame, Texture2D coinIcon)
	{
		_price.text = superGame.Price.ToString(CultureInfo.InvariantCulture);

		if (coinIcon != null)
			_coinIcon.gameObject.GetComponent<RawImage>().texture = coinIcon;

		_progressBar.maxValue = superGame.Max;
		_progressBar.minValue = 0;
		_progressBar.value = superGame.Current;
		_progressBar.GetComponentInChildren<Text>().text = superGame.Current + "/" + superGame.Max;
	}
}
