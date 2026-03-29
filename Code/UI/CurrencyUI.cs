using System;
using TMPro;
using UnityEngine;
using Work.JES.Code.Systems;

namespace Work.JES.Code.UI
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private CurrencySO currencySO;

        private void Awake()
        {
            UpdateMoneyText(0, currencySO.Money.Value);
            currencySO.Money.OnValueChanged += UpdateMoneyText;
        }
        private void OnDestroy()
        {
            currencySO.Money.OnValueChanged -= UpdateMoneyText;
        }

        private void UpdateMoneyText(int prev, int next)
        {
            moneyText.text = next.ToString();
        }
    }
}