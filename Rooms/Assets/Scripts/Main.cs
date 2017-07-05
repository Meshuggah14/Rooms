using System.Collections.Generic;
using Presenter;
using UnityEngine;
using Utils;

public class Main : MonoBehaviour, ICoroutineExecutor
{
    [SerializeField] private Transform _rootUiObject;
    [SerializeField] public List<ObjectPool> Objectpools;

    private UiManager _uImanager;
    private NetModule _netModule;
    private RoomsPresenter _roomsPresenter;
    private ConfigModule _configModule;

    void Awake()
    {
        ObjectPoolManager.Instance.ObjectPools = Objectpools;

        _netModule = new NetModule(this);
        _uImanager = new UiManager(_rootUiObject);
        _configModule = new ConfigModule(_uImanager);
        _roomsPresenter = new RoomsPresenter(_uImanager, _netModule, _configModule);
        _uImanager.SetRoomsWindows(_roomsPresenter);
    }

    void Start()
    {
        _roomsPresenter.LoadMaindata();
    }
}
