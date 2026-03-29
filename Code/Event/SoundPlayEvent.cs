using Code.CoreSystem;
using JES.Code.SoundSystem;
using UnityEngine;

namespace Work.JES.Code.Event
{
    public struct SoundPlayEvent : IEvent
    {
        public SoundSO Sound { get; }
        public Vector3 Position { get; }
        public SoundPlayEvent(SoundSO sound, Vector3 position)
        {
            Sound = sound;
            Position = position;
        }
        public SoundPlayEvent(SoundSO sound)
        {
            Sound = sound;
            Position = Vector3.zero;
        }
    }

    public struct PlayUISoundEvent : IEvent
    {
        public UISoundType SoundType { get; }
        public PlayUISoundEvent(UISoundType soundType)
        {
            SoundType = soundType;
        }
    }
}