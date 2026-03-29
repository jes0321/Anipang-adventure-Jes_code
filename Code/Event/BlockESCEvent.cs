using Code.CoreSystem;
using UnityEngine;

namespace Work.JES.Code.Event
{
    public struct BlockEscEvent : IEvent
    {
        public bool IsBlock { get; }
        public int Hash { get; }
        public BlockEscEvent(int hash,bool isBlock)
        {
            Hash = hash;
            IsBlock = isBlock;
        }
    }
}