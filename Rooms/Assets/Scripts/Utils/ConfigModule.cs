using System;
using System.IO;
using Data;
using UnityEngine;

namespace Utils
{
    public class ConfigModule
    {
        public ConfigData Config;

        private string[] _commandLineArguments;
        private UiManager _uiManager;

        public ConfigModule(UiManager uImanager)
        {
            _uiManager = uImanager;
            GetCommandLineData();
            LoadConfigData();
        }

        private void LoadConfigData()
        {
            string filePath = GetPathConfigData();
            if (File.Exists(filePath))
            {
                string jsondata = File.ReadAllText(filePath);
                Config = JsonUtility.FromJson<ConfigData>(jsondata);
            }
            else
            {
                _uiManager.DialogPresenter.ShowDialog("Can't find config file-" + Environment.NewLine + filePath,
                    () => _uiManager.DialogPresenter.HideDialog(), "Ok");
            }
        }

        private void GetCommandLineData()
        {
            _commandLineArguments = Environment.GetCommandLineArgs();
        }

        private string GetPathConfigData()
        {
            string val = GetCommandLineValue("-config");

            if (val != null)
            {
                return Path.Combine(Application.dataPath, val);
            }

            return Path.Combine(Application.dataPath, "Data/config.json"); //default
        }

        public string GetCommandLineValue(string param)
        {
            var args = _commandLineArguments;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == param && args.Length > i + 1)
                {
                    return args[i + 1];
                }
            }
            return null;
        }
    }
}