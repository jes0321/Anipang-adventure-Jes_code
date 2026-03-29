using Code.CoreSystem;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Work.JES.Code.Event
{
    public struct OnOffNoteEvent : IEvent
    {
        public bool isOn;

        public OnOffNoteEvent(bool isOn)
        {
            this.isOn = isOn;
        }
    }
    public struct OnRotatePoolObject:IEvent
    {
        public GameObject obj;
        public OnRotatePoolObject(GameObject obj)
        {
            this.obj = obj;
        }
    }
}