using Code.CoreSystem;

namespace Work.JES.Code.Event
{
    public struct CameraFovEvent :IEvent
    {
        public int Fov { get; }
        public CameraFovEvent(int fov)
        {
            Fov = fov;
        }
    }
    public struct CameraSensitivityEvent : IEvent
    {
        public float Sensitivity { get; }
        public CameraSensitivityEvent(float sensitivity)
        {
            Sensitivity = sensitivity;
        }
    }
}