using UnityEngine;
using Work.JES.Code.Event;
using Code.CoreSystem;
using Unity.Cinemachine;

namespace Work.JES.Code.Systems
{
    [DefaultExecutionOrder(-1000)]
    public class CameraFovCompo : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera virtualCamera;

        private void Awake()
        {
            Bus<CameraFovEvent>.OnEvent += OnFovChanged;
        }

        private void OnDestroy()
        {
            Bus<CameraFovEvent>.OnEvent -= OnFovChanged;
        }

        private void OnFovChanged(CameraFovEvent evt)
        {
            if (virtualCamera != null)
            {
                virtualCamera.Lens.FieldOfView = evt.Fov;
            }
        }
    }
}