using Code.CoreSystem;
using UnityEngine;

namespace Work.JES.Code.Systems
{
    [CreateAssetMenu(fileName = "Currency SO", menuName = "SO/System/Currency", order = 0)]
    public class CurrencySO : ScriptableObject
    {
        public NotifyValue<int> Money;
        
        public void Clear(int value)
        {
            Money.Value = value;
        }
    }
}