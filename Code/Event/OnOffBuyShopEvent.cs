using Code.CoreSystem;

namespace Work.JES.Code.Event
{
    public struct OnOffSellShopEvent : IEvent
    {
        public bool IsOn { get; }
        public OnOffSellShopEvent(bool isOn)
        {
            IsOn = isOn;
        }
    }
    public struct OnOffBuyShopEvent : IEvent
    {
        public bool IsOn { get; }
        public OnOffBuyShopEvent(bool isOn)
        {
            IsOn = isOn;
        }
    }
}