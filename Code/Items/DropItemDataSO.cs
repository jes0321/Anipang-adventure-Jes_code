using System;
using Code.CoreSystem;
using UnityEngine;
using Work.JES.Code.Event;
using Random = UnityEngine.Random;

namespace Work.JES.Code.Items
{
    public enum PriceState
    {
        Increase,
        Decrease,
        Stable
    }

    [CreateAssetMenu(fileName = "item Data", menuName = "SO/Item/DropItem", order = 0)]
    public class DropItemDataSO : ItemDataSO
    {
        public bool AnipangCatched = false;
        [SerializeField] private int basePrice;

        public PriceState SellPriceState { get; private set; }
        private int MinPrice => (int)(basePrice * 0.3f);
        private int MaxPrice => (int)(basePrice * 1.8f);

        private int _sellPrice;

        public int SellPrice
        {
            get
            {
                if (_sellPrice == 0) _sellPrice = basePrice;
                return _sellPrice;
            }
            set => _sellPrice = Mathf.Clamp(value, MinPrice, MaxPrice);
        }

        public event Action<int> OnPriceChanged;
        public int SellCnt { get; set; }

        [SerializeField] private int pointCnt = 10;

        private void OnEnable()
        {
            Bus<DateChangeEvent>.OnEvent += HandleStoreUpdated;
        }

        private void OnDisable()
        {
            Bus<DateChangeEvent>.OnEvent -= HandleStoreUpdated;
        }

        private void HandleStoreUpdated(DateChangeEvent evt)
        {
            if (AnipangCatched == false)
            {
                SellPriceState = PriceState.Stable;
                return;
            }

            /*
             자. 판매한 수량에 따른 가격 변동 시스템이에요.
             */
            if (SellCnt == 0)
            {
                // 한개도 안 팔았다면 8~15% 가격 인상
                IncreasePrice(0.08f, 0.15f); // 상한선은 MaxPrice에서 이미 제한됨
                if (SellPrice < basePrice)
                {
                    // basePrice보다 낮으면 20~30% 추가 인상, 상한선은 basePrice의 110%
                    IncreasePrice(0.2f, 0.3f, 1.1f);
                }
            }
            else if (SellCnt <= pointCnt)
            {
                // 임의의 값보다 적게 팔았고, 현재 가격이 basePrice보다 낮으면 인상
                if (SellPrice < basePrice)
                {
                    IncreasePrice(0.2f, 0.3f, 1.1f);
                }
                else
                {
                    SellPriceState = PriceState.Stable;
                }
            }
            else
            {
                float percent = Mathf.Clamp01((SellCnt - pointCnt) * 0.03f); // 판매개수 초과분 * 3% (최대 100%)
                SellPrice -= (int)(SellPrice * percent);
                SellPriceState = PriceState.Decrease;
            }

            SellCnt = 0;
            OnPriceChanged?.Invoke(SellPrice);
        }

        private void IncreasePrice(float minPercent, float maxPercent, float maxBasePercent = 1.8f)
        {
            SellPriceState = PriceState.Increase;
            float percent = Random.Range(minPercent, maxPercent);
            SellPrice += (int)(SellPrice * percent);
            SellPrice = Mathf.Min(SellPrice, (int)(basePrice * maxBasePercent));
        }
    }
}