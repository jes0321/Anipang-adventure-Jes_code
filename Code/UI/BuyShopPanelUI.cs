using System;
using Code.CoreSystem;
using Code.Player;
using UnityEngine;
using UnityEngine.UI;
using Code.Inventories;
using JES.Code.SoundSystem;
using TMPro;
using Work.JES.Code.Event;
using Work.JES.Code.Items;
using Work.JES.Code.Systems;
using Work.JES.Code.TutorialSystem;
using Work.JES.Code.UI.Component;

namespace Work.JES.Code.UI
{
    public class BuyShopPanelUI : CanvasGroupUI
    {
        [Header("REFERENCES")] [SerializeField]
        private PlayerInputSO inputSo;

        [SerializeField] private CurrencySO currencySo;
        [SerializeField] private InventoryManagerSO inventoryManagerSo;
        [SerializeField] private GachaSystem gachaSystemPrefab;

        [Header("UIs")] [SerializeField] private Button gatchaBtn;
        [SerializeField] private Button closeButton;
        [SerializeField] private ResourceListSO shopItemList;
        [SerializeField] private GameObject btnPrefab;
        [SerializeField] private RectTransform btnParent;

        [SerializeField] private TextMeshProUGUI gatchaPriceText;
        private int _gatchaPrice=300;
        private bool _isBuy = false;

        protected override void Awake()
        {
            base.Awake();
            gatchaBtn.onClick.AddListener(HandleBuyGatcha);
            Bus<OnOffBuyShopEvent>.OnEvent += HandleOnOffShop;
        }

        private void HandleBuyGatcha()
        {
            if(currencySo.Money.Value<_gatchaPrice)
            {
                Bus<ErrorMessageEvent>.Raise(new ErrorMessageEvent("돈이 부족합니다"));
                Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Fail));
                return;
            }
            //아야어여
            Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Success));
            currencySo.Money.Value -= _gatchaPrice;
            _gatchaPrice = (int)(_gatchaPrice * 1.3f);
            gatchaPriceText.text = _gatchaPrice.ToString();
            Instantiate(gachaSystemPrefab);
        }

        private void OnDestroy()
        {
            Bus<OnOffBuyShopEvent>.OnEvent -= HandleOnOffShop;
            gatchaBtn.onClick.RemoveAllListeners();
            inputSo.OnPausePressedEvent -= ClosePanel;
        }

        private void HandleOnOffShop(OnOffBuyShopEvent evt)
        {
            if (evt.IsOn)
            {
                OpenMethod();
            }
            else if (_isBuy)
            {
                Bus<TutorialActionEvent>.Raise(new TutorialActionEvent(TutorialType.BuyResource));
                _isBuy = false;
            }

            CanvasOnOff(evt.IsOn);
            Bus<OnOffUIEvent>.Raise(new OnOffUIEvent(GetHashCode(), evt.IsOn));
        }

        private void OpenMethod()
        {
            _isBuy = false;
            inputSo.OnPausePressedEvent += ClosePanel;
            closeButton.onClick.AddListener(ClosePanel);

            foreach (Transform child in btnParent)
            {
                Destroy(child.gameObject);
            }

            int btnHeight = 70;
            btnParent.sizeDelta =
                new Vector2(btnParent.sizeDelta.x, shopItemList.GetResourceItems().Count * btnHeight + 30);
            foreach (var item in shopItemList.GetResourceItems())
            {
                var btnObj = Instantiate(btnPrefab, btnParent);
                var buyBtnUI = btnObj.GetComponent<BuyShopBtnUI>();
                buyBtnUI.EnableFor(item, OnBuyItem);
            }
        }

        private void OnBuyItem(ResourceDataSO data, int count)
        {
            int price = data.BuyPrice * count;
            if (currencySo.Money.Value < price)
            {
                Bus<ErrorMessageEvent>.Raise(new ErrorMessageEvent("돈이 부족합니다"));
                Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Fail));
                return;
            }
            Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Success));
            _isBuy = true;
            currencySo.Money.Value -= price;
            inventoryManagerSo.AddItem(data, count);
        }

        private void ClosePanel()
        {
            CanvasOnOff(false);
            Bus<OnOffBuyShopEvent>.Raise(new OnOffBuyShopEvent(false));
            inputSo.OnPausePressedEvent -= ClosePanel;
            closeButton.onClick.RemoveAllListeners();
        }
    }
}