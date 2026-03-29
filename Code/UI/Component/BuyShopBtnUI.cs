using System;
using Code.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.JES.Code.Items;

namespace Work.JES.Code.UI.Component
{
    public class BuyShopBtnUI : MonoBehaviour, IUIElement<ResourceDataSO, Action<ResourceDataSO, int>>
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText, priceText;
        [SerializeField] private TMP_InputField cntInputField;
        [SerializeField] private Button buyButton;

        private ResourceDataSO _itemData;
        private Action<ResourceDataSO, int> _callbackAction;

        private int _buyPrice;
        private int _count = 1;
        public void EnableFor(ResourceDataSO item, Action<ResourceDataSO, int> callback)
        {
            _itemData = item;
            _callbackAction = callback;
            iconImage.sprite = item.Icon;
            nameText.text = item.Name;
            _buyPrice= item.BuyPrice;
            priceText.text = _buyPrice.ToString(); // TODO: 가격 설정
            cntInputField.text = "1";

            _count = 1;
            cntInputField.onEndEdit.AddListener(OnInputEndEdit);
            buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnBuyButtonClicked()
        {
            _callbackAction?.Invoke(_itemData,_count);
        }

        private void OnInputEndEdit(string arg0)
        {
            if (int.TryParse(arg0, out _count))
            {
                if (_count < 1)
                {
                    _count = 1;
                    cntInputField.text = "1";
                }

                _buyPrice = _itemData.BuyPrice * _count;
                priceText.text = _buyPrice.ToString();
            }
            else
            {
                _count = 1;
                cntInputField.text = "1";
                _buyPrice = _itemData.BuyPrice;
                priceText.text = _buyPrice.ToString();
            }
        }

        public void Disable()
        {
            Destroy(gameObject);
        }
    }
}