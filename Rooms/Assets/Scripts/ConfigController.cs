using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ConfigController: MonoBehaviour
{
    public ConfigData Config;
    
    private static ConfigController _configController;
    private string[] _commandLineArguments;

    private ConfigController(){}
    private void Start()
    {
//        GetCommandLineData();
//        LoadConfigData();
    }

    public static ConfigController Instance
    {
        get
        {
            if (_configController == null)
            {
                var dc = new GameObject("ConfigController");
                dc.AddComponent<ConfigController>();
            }

            return _configController;
        }
    }
    
    private void Awake()
    {
        _configController = this;
        
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
            Debug.LogError("Config file doesn't exist");
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
            return val;
        }
        else
        {
            return Path.Combine(Application.dataPath, "Data/config.json"); //default
        }
    }

   
    public string GetCommandLineValue(string param)
    {

        int indx = Array.IndexOf(_commandLineArguments, param);
		
        if (_commandLineArguments != null && indx > -1)
        {
            return _commandLineArguments[indx].Replace(param, "").Trim();
        }
        return null;

    }

}
