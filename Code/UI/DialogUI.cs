using Code.CoreSystem;
using Core.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Work.JES.Code.Event;

namespace Work.JES.Code.UI
{
    public class DialogUI : CanvasGroupUI
    {
        [SerializeField] private float textSpeed = 0.08f;
        [SerializeField] private TextMeshProUGUI mainText;
        private bool _isOn = false;
        private bool _isTexting = false;
        private Coroutine _animationCoroutine;
        private string _msg;
        protected override void Awake()
        {
            base.Awake();
            Bus<OnDialogEvent>.OnEvent += HandleOnDialogEvent;
        }

        private void OnDestroy()
        {
            Bus<OnDialogEvent>.OnEvent -= HandleOnDialogEvent;
        }

        private void HandleOnDialogEvent(OnDialogEvent evt)
        {
            Bus<OnOffUIEvent>.Raise(new OnOffUIEvent(GetHashCode(),true));
            _isOn = true;
            _msg = evt.Message;
            _isTexting = true;
            _animationCoroutine = StartCoroutine(mainText.TextAnimation(_msg, textSpeed));
            CanvasOnOff(true);
        }
        private void Update()
        {
            if (!_isOn) return;
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                if (_isTexting)
                { 
                    StopCoroutine(_animationCoroutine);
                    mainText.maxVisibleCharacters = _msg.Length + 1;
                    _isTexting = false;
                    return;
                }
                Bus<OnOffUIEvent>.Raise(new OnOffUIEvent(GetHashCode(),false));
                _isOn = false;
                CanvasOnOff(false);
                Bus<OffDialogEvent>.Raise(new OffDialogEvent());
            }
        }
    }
}