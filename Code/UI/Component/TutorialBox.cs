using System;
using Code.CoreSystem;
using TMPro;
using UnityEngine;
using Work.JES.Code.Event;

namespace Work.JES.Code.UI.Component
{
    public class TutorialBox : CanvasGroupUI
    {
        [SerializeField] private TextMeshProUGUI messageText;
        protected override void Awake()
        {
            base.Awake();
            Bus<OnBoxEvent>.OnEvent+= HandleOnBoxEvent;
            Bus<OffBoxEvent>.OnEvent+= HandleOffBoxEvent;
        }
        private void OnDestroy()
        {
            Bus<OnBoxEvent>.OnEvent-= HandleOnBoxEvent;
            Bus<OffBoxEvent>.OnEvent-= HandleOffBoxEvent;
        }

        private void HandleOffBoxEvent(OffBoxEvent evt)
        {
            CanvasOnOff(false,false);
        }

        private void HandleOnBoxEvent(OnBoxEvent evt)
        {
            CanvasOnOff(true,false);
            messageText.text = evt.Message;
        }
    }
}