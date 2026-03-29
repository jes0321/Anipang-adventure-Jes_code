using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Work.JES.Code.UI.Component
{
    public enum SettingType
    {
        Sound,
        Graphic,
        Control
    }
    public class SettingTypeBtn : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SettingType settingType;
        
        public UnityEvent<SettingType> OnClickEvent;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnClickEvent?.Invoke(settingType);
        }
    }
}