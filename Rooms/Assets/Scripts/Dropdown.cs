using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] RectTransform Container;
	[SerializeField] GameObject ButtonPrefab;
	
	public bool IsOpen;
	public Text Label;
	public Image Arrow;
	public delegate void OnClick(int gameId);

	public OnClick onClick;
	
	private Button _button;
	private ColorBlock _color;
	
	private string[] _games;
	private Color _normalColor;
	private Color _pressedColor;

	void Awake () {
		_button = GetComponent<Button>();
		_color = GetComponent<Button>().colors;
		_normalColor = _color.normalColor;
		_pressedColor = _color.pressedColor;
	}
	// Use this for initialization
	void Start ()
	{
		Container = transform.Find("Container").GetComponent<RectTransform>();
		IsOpen = false;
		//GetItems();
	}

	public void FillDropDown(Dictionary<int, string> items)
	{
		RemoveChildren(Container);
		
		foreach (var item in items)
		{
			CreateItem(item.Key, item.Value);
		}
	}
	
	private void RemoveChildren(RectTransform container)
	{
		foreach(Transform child in container.transform) Destroy(child.gameObject);
	}

	private void CreateItem(int id, string name)
	{
		GameObject dropdownitem = (GameObject)Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity);
		dropdownitem.GetComponentInChildren<Text>().text = name;
		dropdownitem.name = id.ToString();
		dropdownitem.GetComponent<Button>().onClick.AddListener(
			() => ButtonClick(dropdownitem));
		//dropdownitem.transform.localScale = new Vector3(1,1,1);
		dropdownitem.transform.SetParent(Container.gameObject.transform, false);
	}
	
	private void ButtonClick(GameObject button)
	{
		Label.text = button.GetComponentInChildren<Text>().text;
		IsOpen = !IsOpen;
		Arrow.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180));
		onClick(Convert.ToInt32(button.name));
	}

	// Update is called once per frame
	void Update () {
		
		Vector3 scale = Container.localScale;
		scale.y = Mathf.Lerp(scale.y, IsOpen ? 1 : 0, Time.deltaTime * 12);
		Container.localScale = scale;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		IsOpen = !IsOpen;
		Arrow.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180));
		
		//change color
		_color.normalColor = IsOpen ?  _pressedColor : _normalColor;
		_button.colors = _color;
	}

}
