using System;
using UnityEngine;
using View;
using Object = UnityEngine.Object;

namespace Presenter
{
    public class DialogPresenter : Presenter<DialogView>
    {
        public DialogPresenter()
        {
            LoadView();
        }

        public void ShowDialog(string Title, Action Ok, string buttonLable)
        {
            windows.SetSetting(Title, Ok, buttonLable);
            windows.Show();
        }

        public void HideDialog()
        {
            windows.Hide();
        }

        private void LoadView()
        {
            windows = Object.Instantiate(Resources.Load<GameObject>("ModalView").GetComponent<DialogView>());
        }
    }
}
