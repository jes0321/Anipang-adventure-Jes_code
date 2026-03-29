using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Work.JES.Code.Items
{
    [CreateAssetMenu(menuName = "SO/Items/ResourceListSO", fileName = "ResourceListSO")]
    public class ResourceListSO : ScriptableObject
    {
        [SerializeField] private List<ResourceDataSO> resourceItems;
        
        public List<ResourceDataSO> GetResourceItems()
        {
            return resourceItems.Where(data=>data.Type== ItemType.Resource).ToList();
        }
    }
}