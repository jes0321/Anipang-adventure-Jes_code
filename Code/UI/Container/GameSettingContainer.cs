using Code.UI;
using UnityEngine;
using Work.JES.Code.Systems;
using Work.JES.Code.UI.Component;

namespace Work.JES.Code.UI.Container
{
    public class GameSettingContainer : CanvasGroupUI,IUIElement
    {
        [SerializeField] private CameraSettingUI cameraSettingUI;
        public void EnableFor()
        {
            CanvasOnOff(true,false);
            cameraSettingUI.EnableFor();
        }

        public void Disable()
        {
            cameraSettingUI.Disable();
            CanvasOnOff(false,false);
        }
    }
}