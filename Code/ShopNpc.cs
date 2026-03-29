using Code.Core.Interacts;
using Code.CoreSystem;
using UnityEngine;
using Work.JES.Code.Event;

namespace Work.JES.Code
{
    public class ShopNpc : MonoBehaviour,IInteractable
    {
        enum ShopType
        {
            Buy,
            Sell
        }
        [SerializeField] private ShopType shopType;
        public void Interact()
        {
            if (shopType == ShopType.Sell)
            {
                Bus<OnOffSellShopEvent>.Raise(new OnOffSellShopEvent(true));
                return;
            }
            Bus<OnOffBuyShopEvent>.Raise(new OnOffBuyShopEvent(true));
        }
    }
}