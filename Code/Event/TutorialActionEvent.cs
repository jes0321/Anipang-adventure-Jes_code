using Code.CoreSystem;
using Work.JES.Code.TutorialSystem;

namespace Work.JES.Code.Event
{
    public struct TutorialActionEvent : IEvent
    {
        public TutorialType TutorialType { get; }
        public TutorialActionEvent(TutorialType tutorialType)
        {
            TutorialType = tutorialType;
        }
    }

    public struct OnDialogEvent : IEvent
    {
        public string Message { get; }
        public OnDialogEvent(string message)
        {
            Message = message;
        }
    }
    public struct OffDialogEvent : IEvent
    {
    }
    public struct OnBoxEvent : IEvent
    {
        public string Message { get; }
        public OnBoxEvent(string message)
        {
            Message = message;
        }
    }
    public struct OffBoxEvent : IEvent{}
}