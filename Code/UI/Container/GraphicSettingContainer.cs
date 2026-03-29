using System.Linq;
using Code.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.JES.Code.UI.Component;

namespace Work.JES.Code.UI.Container
{
    public class GraphicSettingContainer : CanvasGroupUI,IUIElement
    {
        [SerializeField] private FullScreenSetting fullScreenSetting;
        [SerializeField] private ResolutionSetting resolutionSetting;
        
        public void EnableFor()
        {
            CanvasOnOff(true, false);
            resolutionSetting.EnableFor();
            fullScreenSetting.EnableFor();
        }

        public void Disable()
        {
            CanvasOnOff(false, false);
            fullScreenSetting.Disable();
            resolutionSetting.Disable();
        }
    }
}