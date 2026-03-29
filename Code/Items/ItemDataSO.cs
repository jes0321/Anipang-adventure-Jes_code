using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Work.JES.Code.Items
{
    public enum ItemType
    {
        None = 0,
        // Tool,
        // Weapon,
        // Armor,
        DropItem,
        Resource,


        Max
    }

    [CreateAssetMenu(fileName = "ItemData", menuName = "SO/Item", order = 0)]
    public class ItemDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public PoolItemSO PoolItemSO { get; private set; }
        [field: SerializeField] public ItemType Type { get; private set; }
        
    }
}