using System;
using Code.CoreSystem;
using Code.UI;
using Core.CoreSystem.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.JES.Code.Event;

namespace Work.JES.Code.UI.Component
{
    [Serializable]
    public struct CameraSettingData
    {
        public float sensitivity;
        public int fov;
    }

    public class CameraSettingUI : MonoBehaviour, IUIElement, ISavable
    {
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private Slider fovSlider;
        [SerializeField] private TMP_InputField sensitivityValueText;
        [SerializeField] private TMP_InputField fovValueText;
        [field: SerializeField] public SaveIdSO SaveID { get; private set; }

        public void EnableFor()
        {
            sensitivityValueText.onEndEdit.AddListener(OnSensitivityInputChanged);
            fovValueText.onEndEdit.AddListener(OnFovInputChanged);
            sensitivitySlider.onValueChanged.AddListener(HandleSensitivity);
            fovSlider.onValueChanged.AddListener(HandleFov);
        }

        private void HandleFov(float value)
        {
            int intValue = Mathf.RoundToInt(value);
            fovValueText.text = intValue.ToString();
            if (fovSlider.value != intValue)
                fovSlider.value = intValue;
            Bus<CameraFovEvent>.Raise(new CameraFovEvent(intValue));
        }

        private void HandleSensitivity(float value)
        {
            float rounded = (float)Math.Round(value, 2);
            sensitivityValueText.text = rounded.ToString("F2");
            if (Math.Abs(sensitivitySlider.value - rounded) > 0.001f)
                sensitivitySlider.value = rounded;
            Bus<CameraSensitivityEvent>.Raise(new CameraSensitivityEvent(rounded));
        }

        private void OnSensitivityInputChanged(string input)
        {
            if (float.TryParse(input, out float value))
            {
                float rounded = (float)Math.Round(value, 2);
                sensitivitySlider.value = rounded;
                sensitivityValueText.text = rounded.ToString("F2");
                Bus<CameraSensitivityEvent>.Raise(new CameraSensitivityEvent(rounded));
            }
        }

        private void OnFovInputChanged(string input)
        {
            if (int.TryParse(input, out int value))
            {
                fovSlider.value = value;
                fovValueText.text = value.ToString();
                Bus<CameraFovEvent>.Raise(new CameraFovEvent(value));
            }
        }

        public void Disable()
        {
            sensitivitySlider.onValueChanged.RemoveListener(HandleSensitivity);
            fovSlider.onValueChanged.RemoveListener(HandleFov);
            sensitivityValueText.onEndEdit.RemoveListener(OnSensitivityInputChanged);
            fovValueText.onEndEdit.RemoveListener(OnFovInputChanged);
        }

        public string GetSaveData()
        {
            CameraSettingData data = new CameraSettingData
            {
                sensitivity = sensitivitySlider.value,
                fov = Mathf.RoundToInt(fovSlider.value)
            };
            return JsonUtility.ToJson(data);
        }

        public void RestoreData(string loadedData)
        {
            CameraSettingData data = JsonUtility.FromJson<CameraSettingData>(loadedData);

            float rounded = (float)Math.Round(data.sensitivity, 2);
            sensitivitySlider.value = rounded;
            sensitivityValueText.text = rounded.ToString("F2");
            Bus<CameraSensitivityEvent>.Raise(new CameraSensitivityEvent(rounded));


            fovSlider.value = data.fov;
            fovValueText.text = data.fov.ToString();
            Bus<CameraFovEvent>.Raise(new CameraFovEvent(data.fov));
        }
    }
}