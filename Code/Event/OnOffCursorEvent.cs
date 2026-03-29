using Code.CoreSystem;
using UnityEngine;

namespace Work.JES.Code.Event
{
    public struct OnOffCursorEvent :IEvent
    {
        public int Hash { get; }
        public bool IsOn { get; }
        public OnOffCursorEvent(int hash,bool isOn)
        {
            Hash = hash;
            IsOn = isOn;
        }
    }
    public struct PlayerMoveEvent : IEvent
    {
        public int Hash { get; }
        public bool CanMove { get; }
        public PlayerMoveEvent(int hash,bool canMove)
        {
            Hash = hash;
            CanMove = canMove;
        }
    }
    public struct OnOffUIEvent : IEvent
    {
        public int Hash { get; }
        public bool IsOn { get; }
        public OnOffUIEvent(int hash,bool isOn)
        {
            Hash = hash;
            IsOn = isOn;
        }
    }
    public struct ErrorMessageEvent : IEvent
    {
        public string Message { get; }
        public ErrorMessageEvent(string message)
        {
            Message = message;
        }
    }
}