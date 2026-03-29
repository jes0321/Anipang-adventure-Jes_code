using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Work.JES.Code.Items;

namespace Work.JES.Code.Notes
{
    [CreateAssetMenu(fileName = "NoteDataListSO", menuName = "SO/NoteList", order = 0)]
    public class NoteDataListSO : ScriptableObject
    {
        [field: SerializeField] public GachaDataSO dataSO { get; private set; }
        public List<AnipangDataSO> NoteDataList = new List<AnipangDataSO>();
        [SerializeField] private DropItemListSO dropItemListSO;
        private void OnEnable()
        {
            if (dataSO == null || dataSO.AnimalPrefabs == null)
                return;

            NoteDataList = new List<AnipangDataSO>();
            dropItemListSO.DropItemDataSOs = new List<DropItemDataSO>();
            // 희귀도 순서대로 정렬 (Common → Legendary)
            var sorted = dataSO.AnimalPrefabs
                .OrderByDescending(x => x.Rarity)
                .ToList();

            int number = 0;
            foreach (var item in sorted)
            { 
                // AnimalPrefab이 NoteDataSo를 상속한다고 가정
                item.AnimalPrefab.Data.Number = number;
                NoteDataList.Add(item.AnimalPrefab.Data);
                dropItemListSO.DropItemDataSOs.Add(item.AnimalPrefab.Data.dropItemData);
                number++;
            }
        }
    }
}