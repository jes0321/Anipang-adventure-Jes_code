using System;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;
using Work.JES.Code.Items;
using Work.JES.Code.Skills;
using Random = UnityEngine.Random;

namespace Work.JES.Code.Notes
{
    [CreateAssetMenu(fileName = "Anipang data", menuName = "SO/Anipang/Data", order = 0)]
    public class AnipangDataSO : NoteDataSo
    {
        [field:SerializeField] public SkillDataSO SkillData { get; private set; }
        [field:SerializeField] public GameObject Visual { get;private set; }

        [Header("AnipangPetrolData")] 
        public float petrolTime;
        public float petrolTimeWindow;
        public float petrolDistance;
        public float chaseDistance;
        
        [Space(5)]
        
        [Header("DropItemData")]
        public DropItemDataSO dropItemData;

        public float dropDelay => Random.Range(35f, 45f);
    }
}