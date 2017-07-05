using Presenter;
using UnityEngine;

namespace Utils
{
    public class UiManager
    {
        private readonly Transform _rootUiObject;
        private readonly DialogPresenter _dialogPresenter;

        public DialogPresenter DialogPresenter
        {
            get { return _dialogPresenter; }
        }

        public UiManager(Transform rootUiObject)
        {
            _rootUiObject = rootUiObject;

            _dialogPresenter = new DialogPresenter();
            _dialogPresenter.Windows.transform.SetParent(_rootUiObject, false);
            _dialogPresenter.HideDialog();
        }

        public void SetRoomsWindows(RoomsPresenter roomsPresenter)
        {
            roomsPresenter.Windows.transform.SetParent(_rootUiObject, false);
        }
    }
}