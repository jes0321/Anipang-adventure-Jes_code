using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Work.JES.Code.Items;

namespace Work.JES.Code.UI.Component
{
    public class ShopBtnUI : MonoBehaviour,IPointerDownHandler
    {
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image arrowImage;
        [SerializeField] private Sprite up, down, center;
        private DropItemDataSO _dropItemData;
        public UnityEvent<DropItemDataSO> OnClickEvent;
        public void SetUp(DropItemDataSO dropItemDataSo)
        {
            _dropItemData = dropItemDataSo;
            iconImage.sprite = _dropItemData.Icon;
            HandleChange(_dropItemData.SellPrice);
            _dropItemData.OnPriceChanged += HandleChange;
        }

        private void OnDestroy()
        {
            _dropItemData.OnPriceChanged -= HandleChange;
        }

        private void HandleChange(int obj)
        {
            priceText.text = obj.ToString();
            switch (_dropItemData.SellPriceState)
            {
                case PriceState.Increase:
                    arrowImage.sprite = up;
                    break;
                case PriceState.Decrease:
                    arrowImage.sprite = down;
                    break;
                case PriceState.Stable:
                    arrowImage.sprite = center;
                    break;
            }
        }

        private void HandleClick()
        {
            OnClickEvent?.Invoke(_dropItemData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Click");
            HandleClick();
        }
    }
}