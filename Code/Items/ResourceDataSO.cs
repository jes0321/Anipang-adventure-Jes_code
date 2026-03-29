using UnityEngine;

namespace Work.JES.Code.Items
{
    [CreateAssetMenu(menuName = "SO/Item/ResourceData", fileName = "ResourceDataSO")]
    public class ResourceDataSO : ItemDataSO
    {
        [field: SerializeField] public int BuyPrice { get; private set; }
    }
}