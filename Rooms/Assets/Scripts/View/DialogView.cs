using System;
using Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class DialogView : MonoBehaviour, IWindow
    {
        [SerializeField] private Button _okButton;
        [SerializeField] private Text _title;
        [SerializeField] private Text _buttonText;

        public void Show()
        {
            gameObject.SetActive(true);
            gameObject.transform.SetAsLastSibling();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetSetting(string title, Action ok, string buttonLabel)
        {
            _title.text = title;
            _buttonText.text = buttonLabel;
            _okButton.onClick.RemoveAllListeners();
            _okButton.onClick.AddListener(() =>
            {
                if (ok != null)
                {
                    ok();
                }
            });
        }
    }
}