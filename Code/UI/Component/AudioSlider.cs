using System;
using Code.UI;
using Core.CoreSystem.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Work.JES.Code.UI.Component
{
    [Serializable]
    public class FloatWrapper
    {
        public float value;
    }
    public class AudioSlider : MonoBehaviour, IUIElement, ISavable
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider slider;
        [field: SerializeField] public SaveIdSO SaveID { get; private set; }

        public void EnableFor()
        {
            slider.onValueChanged.AddListener(HandleChange);
        }

        private void HandleChange(float arg0)
        {
            Debug.Log(SaveID.saveName+arg0);
            audioMixer.SetFloat(SaveID.saveName, Mathf.Log10(arg0) * 20);
        }

        public void Disable()
        {
            slider.onValueChanged.RemoveAllListeners();
        }

        public string GetSaveData()
        {
            var wrapper = new FloatWrapper { value = slider.value };
            return JsonUtility.ToJson(wrapper);
        }

        public void RestoreData(string loadedData)
        {
            if (string.IsNullOrEmpty(loadedData)) return;
            var wrapper = JsonUtility.FromJson<FloatWrapper>(loadedData);
            slider.value = wrapper.value;
            HandleChange(wrapper.value);
        }
    }
}