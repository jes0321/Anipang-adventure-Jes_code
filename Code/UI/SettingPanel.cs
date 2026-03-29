using System;
using Code.UI;
using UnityEngine;
using Work.JES.Code.UI.Component;
using Work.JES.Code.UI.Container;

namespace Work.JES.Code.UI
{
    public class SettingPanel : CanvasGroupUI,IUIElement
    {
        private SettingType _currentType = SettingType.Sound;
        [SerializeField] private Transform btnTrm;

        [SerializeField] private SoundSettingContainer soundSettingContainer;
        [SerializeField] private GraphicSettingContainer graphicSettingContainer;
        [SerializeField] private GameSettingContainer gameSettingContainer;
        protected override void Awake()
        {
            base.Awake();
            var btns = btnTrm.GetComponentsInChildren<SettingTypeBtn>();
            foreach (var btn in btns)
            {
                btn.OnClickEvent.AddListener(HandleClickBtn);
            }
        }

        private void HandleClickBtn(SettingType type)
        {
            _currentType = type;
            OnOff();
        }

        public void EnableFor()
        {
            CanvasOnOff(true,false);
            OnOff();
        }

        private void OnOff()
        {
            AllDisable();
            switch (_currentType)
            {
                case SettingType.Sound:
                    soundSettingContainer.EnableFor();
                    break;
                case SettingType.Graphic:
                    graphicSettingContainer.EnableFor();
                    break;
                case SettingType.Control:
                    gameSettingContainer.EnableFor();
                    break;
            }
        }

        private void AllDisable()
        {
            soundSettingContainer.Disable();
            graphicSettingContainer.Disable();
            gameSettingContainer.Disable();
        }
        public void Disable()
        {
            AllDisable();
            CanvasOnOff(false,false);
        }
    }
}