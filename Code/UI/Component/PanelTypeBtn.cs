using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Work.JES.Code.UI.Component
{
    public class PanelTypeBtn : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private EscPanelType escPanelType;
        [SerializeField] private Sprite enableSprite;

        public EscPanelType EscPanelType => escPanelType;
        public UnityEvent<PanelTypeBtn> OnClickEvent;

        private Sprite _defaultSprite;
        
        private void Awake()
        {
            _defaultSprite = image.sprite;
            button.onClick.AddListener(OnClick);
        }

        public void OnOffBtn(bool isOn)
        {
            Debug.Log(isOn);
            image.sprite = isOn ? enableSprite : _defaultSprite;
        }
        public void OnClick()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}