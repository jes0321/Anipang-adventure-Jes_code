using System;
using Code.CoreSystem;
using Code.Player;
using UnityEngine;
using Code.Inventories;
using JES.Code.SoundSystem;
using Work.JES.Code.Event;
using Work.JES.Code.Items;
using Work.JES.Code.Systems;
using Work.JES.Code.TutorialSystem;
using Work.JES.Code.UI.Component;

namespace Work.JES.Code.UI
{
    public class SellShopPanelUI : CanvasGroupUI
    {
        [SerializeField] private PlayerInputSO playerInputSO;
        [SerializeField] private CurrencySO currencySO;
        [SerializeField] private InventoryManagerSO inventoryManagerSO;
        [SerializeField] private DropItemListSO dropItemListSO;
        [SerializeField] private GameObject btnPrefab;

        private void Start()
        {
            foreach (var dropItemDataSO in dropItemListSO.DropItemDataSOs)
            {
                var btnObj = Instantiate(btnPrefab, transform);
                var shopBtnUI = btnObj.GetComponent<ShopBtnUI>();
                shopBtnUI.SetUp(dropItemDataSO);
                shopBtnUI.OnClickEvent.AddListener(HandleClick);
            }

            Bus<OnOffSellShopEvent>.OnEvent += HandleOnOff;
        }

        private void OnDestroy()
        {
            playerInputSO.OnESCPressedEvent -= HandleClose;
            Bus<OnOffSellShopEvent>.OnEvent -= HandleOnOff;
        }

        private async void HandleOnOff(OnOffSellShopEvent evt)
        {
            if (evt.IsOn)
            {
                playerInputSO.OnESCPressedEvent += HandleClose;
            }

            Bus<OnOffUIEvent>.Raise(new OnOffUIEvent(GetHashCode(),evt.IsOn));    
            CanvasOnOff(evt.IsOn);
            await Awaitable.NextFrameAsync();
        }

        public void HandleClose()
        {
            playerInputSO.OnESCPressedEvent -= HandleClose;
            Bus<OnOffSellShopEvent>.Raise(new OnOffSellShopEvent(false));
        }


        private void HandleClick(DropItemDataSO data)
        {
            if (inventoryManagerSO.GetItemCount(ItemType.DropItem, data) > 0)
            { 
                Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Success));
                Bus<TutorialActionEvent>.Raise(new TutorialActionEvent(TutorialType.SellItem));
                data.SellCnt++;
                inventoryManagerSO.RemoveItem(data);
                currencySO.Money.Value += data.SellPrice;
            }
            else
            {
                Bus<ErrorMessageEvent>.Raise(new ErrorMessageEvent("아이템이 부족합니다"));
                Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Fail));
            }
        }
    }
}