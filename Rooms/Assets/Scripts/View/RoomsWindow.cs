using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Data;
using Presenter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace View
{
    public class RoomsWindow : MonoBehaviour, IWindow
    {
        public Dropdown DropdownGames;
        public RectTransform Contanier;

        [SerializeField] private ScrollRect _scroll;
        [SerializeField] private GameObject _prefabRoomBock;
        [SerializeField] private GameObject _prefabSuperGameBlock;

        private ObjectPoolManager _objectPoolManager;
        private RoomsPresenter _presenter;
        private Texture2D _playerIcon;
        private Texture2D _coinIcon;

        public void InitPresenter(RoomsPresenter presenter)
        {
            _presenter = presenter;
            _objectPoolManager = ObjectPoolManager.Instance;

            DropdownGames.onValueChanged.AddListener(delegate
            {
                presenter.OnGameNameSelect(presenter.GetGameIdByIndex(DropdownGames.value));
            });
        }

        public void SetPlayerIcon(Icon icon)
        {
            _playerIcon = icon.Value;
        }

        public void SetCoinIcon(Icon icon)
        {
            _coinIcon = icon.Value;

            //todo bug - coroutine
            _presenter.ShowGameData(-1);
        }

        public void Show()
        {
        }

        public void Hide()
        {
        }

        public void UpdateGameNames(Dictionary<int, string> items)
        {
            FillDropDown(items);
        }

        private void FillDropDown(Dictionary<int, string> items)
        {
            DropdownGames.options.Clear();

            foreach (KeyValuePair<int, string> item in items)
            {
                DropdownGames.options.Add(new Dropdown.OptionData() {text = item.Value.ToUpper()});
            }

            DropdownGames.GetComponentInChildren<Text>().text = DropdownGames.options[0].text;
        }

        public void ShowRooms(string[] info)
        {

        }

        public void AddRoomBlock(Room room)
        {
            GameObject newitem = _objectPoolManager.AddItem(_prefabRoomBock);
            RoomBlock roomblock = newitem.GetComponent<RoomBlock>();
            roomblock.transform.SetParent(Contanier, false);
            roomblock.Setup(room, _playerIcon, _presenter.OnJoinClick);
        }

        public void AddSuperGameBlock(SuperGame superGame)
        {
            GameObject newitem = _objectPoolManager.AddItem(_prefabSuperGameBlock);
            SuperGameBlock supergameblock = newitem.GetComponent<SuperGameBlock>();
            supergameblock.transform.SetParent(Contanier, false);
            supergameblock.Setup(superGame, _coinIcon);
        }

        public void RemoveAllItems()
        {
            int childs = Contanier.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                _objectPoolManager.RemoveItem(Contanier.GetChild(i).gameObject);
            }
        }

        public void ScrollToTop()
        {
            Canvas.ForceUpdateCanvases();
            _scroll.StopMovement();
            _scroll.verticalNormalizedPosition = 1;
        }
    }
}