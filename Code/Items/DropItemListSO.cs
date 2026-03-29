using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Work.JES.Code.Items
{
    [CreateAssetMenu(fileName = "DropItem List", menuName = "SO/Item/DropItemList", order = 0)]
    public class DropItemListSO : ScriptableObject
    {
        public List<DropItemDataSO> DropItemDataSOs = new List<DropItemDataSO>();
    }
}