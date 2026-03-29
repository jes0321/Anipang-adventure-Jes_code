using System;
using Code.CoreSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Work.JES.Code.Event;

namespace Work.JES.Code.UI
{
    public class ErrorMessageUI : CanvasGroupUI
    {
        [SerializeField] private TextMeshProUGUI errorMessageText;

        protected override void Awake()
        {
            base.Awake();
            Bus<ErrorMessageEvent>.OnEvent += HandleErrorMessage;
        }
        private void OnDestroy()
        {
            Bus<ErrorMessageEvent>.OnEvent -= HandleErrorMessage;
        }

        private void HandleErrorMessage(ErrorMessageEvent evt)
        {
            DOTween.KillAll();
            errorMessageText.text = evt.Message;
            CanvasOnOff(true,false);
            DOVirtual.DelayedCall(2f, () => CanvasOnOff(false));
        }
    }
}