using Code.UI;
using UnityEngine;
using UnityEngine.Audio;
using Work.JES.Code.UI.Component;

namespace Work.JES.Code.UI.Container
{
    public class SoundSettingContainer : CanvasGroupUI,IUIElement
    {
        [SerializeField] private AudioSlider[] sliders;
        
        public void EnableFor()
        {
            foreach (var slider in sliders)
            {
                slider.EnableFor();
            }
            CanvasOnOff(true, false);
        }
        
        public void Disable()
        {
            CanvasOnOff(false, false);
            foreach (var slider in sliders)
            {
                slider.Disable();
            }
        }
    }
}