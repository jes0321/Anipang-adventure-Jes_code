using System;
using System.Collections.Generic;
using UnityEngine;
using Work.JES.Code.Items;

namespace Work.JES.Code.Notes
{
    /// <summary>
    /// 획득 지역 이름을 추가 해야함
    /// </summary>
    public enum DropOrgin
    {
        None,
        Ground,
        Craft,
        Drop,
        Quest,
        Shop
    }

    public abstract class NoteDataSo : ItemDataSO
    {
        [field: SerializeField] public int Number { get; set; }
       
        [SerializeField] private DropOrgin dropOrgin;
        [field: SerializeField] public bool IsFind { get; set; } = false;
        [field: SerializeField] public bool IsCatch { get; set; } = false;

        public string GetDropOrgin()
        {
            // 플래그별 한글 매핑
            var nameMap = new Dictionary<DropOrgin, string>
            {
                { DropOrgin.Ground, "필드" },
                { DropOrgin.Craft, "제작" },
                { DropOrgin.Drop, "애니팡 드랍" },
                { DropOrgin.Quest, "퀘스트" },
                { DropOrgin.Shop, "상점" }
            };

            var result = new List<string>();
            foreach (var kvp in nameMap)
            {
                if (dropOrgin == kvp.Key)
                    result.Add(kvp.Value);
            }

            return string.Join(", ", result);
        }
    }
}