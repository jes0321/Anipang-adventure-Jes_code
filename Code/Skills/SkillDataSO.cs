using UnityEngine;
using Work.JES.Code.Items;

namespace Work.JES.Code.Skills
{
    public enum SkillType
    {
        Active,
        Passive
    }
    [CreateAssetMenu(fileName = "SKill Data", menuName = "SO/Skill", order = 0)]
    public class SkillDataSO : ItemDataSO
    {
        [field:SerializeField] public SkillType SkillType { get; private set; }
    }
}