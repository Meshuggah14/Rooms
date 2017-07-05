using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Data;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using View;
using NetModule = Utils.NetModule;
using Object = UnityEngine.Object;

namespace Presenter
{
    public class RoomsPresenter : Presenter<RoomsWindow>
    {
        private readonly UiManager _uimanager;
        private readonly NetModule _netModule;
        private readonly ConfigModule _configModule;
        private RoomsContainer _model;
        private readonly NetModule.BasewwwRequest<RoomsContainer> _loadXmlRequest;

        public RoomsPresenter(UiManager uimanager, NetModule dataController, ConfigModule configModule)
        {
            if (string.IsNullOrEmpty(configModule.Config.urldata))
            {
                _uimanager.DialogPresenter.ShowDialog("can't find config file",
                    () => _uimanager.DialogPresenter.HideDialog(), "Ok");
            }
            else
            {
                LoadView();

                windows.InitPresenter(this);
                _uimanager = uimanager;
                _netModule = dataController;
                _configModule = configModule;
                _loadXmlRequest = new NetModule.BasewwwRequest<RoomsContainer>(ShowWindowData, OnLoadError,
                    configModule.Config.urldata);
            }
        }

        private void OnLoadError(string s, string url)
        {
            _uimanager.DialogPresenter.ShowDialog("can't load from url" + Environment.NewLine + url,
                () => _uimanager.DialogPresenter.HideDialog(), "Ok");
        }

        private void ShowWindowData(RoomsContainer roomsContainer)
        {
            _model = roomsContainer;

            _model.ApplyCommonSort();
            if (_configModule.Config.availables != null && _configModule.Config.availables.Length > 0)
            {
                UpdateGamesNames();
                LoadIcons();
                //ShowGameData(_model.Games[0].GameId);
            }
            else
            {
                _uimanager.DialogPresenter.ShowDialog("no available games",
                    () => _uimanager.DialogPresenter.HideDialog(), "Ok");
            }
        }

        public void LoadIcons()
        {
            _netModule.LoadRawImg(new NetModule.BasewwwRequest<Icon>(windows.SetPlayerIcon, OnLoadError,
                GetParamValue("PLAYERS_ICON")));
            _netModule.LoadRawImg(new NetModule.BasewwwRequest<Icon>(windows.SetCoinIcon, OnLoadError,
                GetParamValue("COIN_ICON")));
        }

        private String GetParamValue(string paramName)
        {
            var firstOrDefault = _model.Settings.FirstOrDefault(i => i.Name == paramName);

            if (firstOrDefault != null)
                return firstOrDefault.Value;
            return "";
        }

        private void UpdateGamesNames()
        {
            windows.UpdateGameNames(_model.GetAvaliableGameNames(_configModule.Config.availables));
        }

        public void ShowGameData(int gameId)
        {
            if (gameId == -1)
            {
                gameId = _model.Games[0].GameId;
            }

            var game = _model.GetGameById(gameId);

            windows.RemoveAllItems();

            windows.ScrollToTop();

            foreach (var superGame in game.SuperGames)
            {
                windows.AddSuperGameBlock(superGame);
                foreach (var room in game.Rooms.Where(i => i.Price == superGame.Price).ToList())
                {
                    windows.AddRoomBlock(room);
                }
            }
        }

        private void LoadView()
        {
            windows = Object.Instantiate(Resources.Load<GameObject>("RoomsWindow").GetComponent<RoomsWindow>());
        }

        public void LoadMaindata()
        {
            _netModule.LoadDoc(_loadXmlRequest);
        }

        public void OnGameNameSelect(int gameId)
        {
            ShowGameData(gameId);
        }

        public int GetGameIdByIndex(int dropdownGamesValue)
        {
            return _model.Games[dropdownGamesValue].GameId;
        }

        public void OnJoinClick(string roomName)
        {
            _uimanager.DialogPresenter.ShowDialog("You joined to " + Environment.NewLine + roomName,
                () => _uimanager.DialogPresenter.HideDialog(), "Ok");
        }
    }
}