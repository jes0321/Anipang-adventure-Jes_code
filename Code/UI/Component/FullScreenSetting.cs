using System;
using Code.UI;
using Core.CoreSystem.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Work.JES.Code.UI.Component
{
    [Serializable]
    public struct BoolWrapper
    {
        public bool Value;
    }
    public class FullScreenSetting : MonoBehaviour,IUIElement,ISavable
    {
        [SerializeField] private Toggle fullScreenToggle;
        [field:SerializeField] public SaveIdSO SaveID { get; private set; }


        public void EnableFor()
        {
            fullScreenToggle.onValueChanged.AddListener(HandleChange);
        }

        private void HandleChange(bool arg0)
        {
            Screen.fullScreen = arg0;
        }

        public void Disable()
        {
            fullScreenToggle.onValueChanged.RemoveAllListeners();
        }

        public string GetSaveData()
        {
            var wrapper = new BoolWrapper {Value = fullScreenToggle.isOn};
            return JsonUtility.ToJson(wrapper);
        }

        public void RestoreData(string loadedData)
        {
            var isFullScreen = JsonUtility.FromJson<BoolWrapper>(loadedData);
            fullScreenToggle.isOn = isFullScreen.Value;
            Screen.fullScreen = isFullScreen.Value;
        }
    }
}