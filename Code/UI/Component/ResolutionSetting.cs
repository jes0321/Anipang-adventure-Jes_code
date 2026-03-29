using System;
using System.Linq;
using Code.UI;
using Core.CoreSystem.SaveSystem;
using TMPro;
using UnityEngine;

namespace Work.JES.Code.UI.Component
{
    [Serializable]
    public struct IntWrapper
    {
        public int Value;
    }
    public class ResolutionSetting : MonoBehaviour,IUIElement,ISavable
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [field:SerializeField] public SaveIdSO SaveID { get; private set; }
        
        private Resolution[] _resolutions;
        private int _currentResolutionIndex = 0;

        private void Start()
        {
            InitResolutionDropdown();
            SetResolution(_currentResolutionIndex);
        }

        public void EnableFor()
        {
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        public void Disable()
        {
            resolutionDropdown.onValueChanged.RemoveListener(SetResolution);
        }
        
        private void InitResolutionDropdown()
        {
            // 16:9 비율의 해상도만 필터링하고, 주사율이 다른 중복 항목을 제거합니다.
            _resolutions = Screen.resolutions
                .Where(resolution => Mathf.Approximately(resolution.width / (float)resolution.height, 16f / 9f))
                .Select(resolution => new { resolution.width, resolution.height })
                .Distinct()
                .Select(res => new Resolution { width = res.width, height = res.height })
                .ToArray();

            resolutionDropdown.ClearOptions();

            // 드롭다운 옵션에 주사율을 표시하지 않습니다.
            var options = _resolutions.Select(r => $"{r.width} x {r.height}").ToList();
            resolutionDropdown.AddOptions(options);

            int currentIndex = 0;

            // 현재 해상도와 일치하는 인덱스를 찾습니다 (주사율 비교 제외).
            for (int i = 0; i < _resolutions.Length; i++)
            {
                if (Screen.currentResolution.width == _resolutions[i].width &&
                    Screen.currentResolution.height == _resolutions[i].height)
                {
                    currentIndex = i;
                    break;
                }
            }


            resolutionDropdown.value = currentIndex;
            resolutionDropdown.RefreshShownValue();
            resolutionDropdown.onValueChanged.AddListener(SetResolution);

            // 최초 실행 시 해상도 적용
            SetResolution(currentIndex);
        }
        private void SetResolution(int index)
        {
            var res = _resolutions[index];
            // 해상도 설정 시 주사율을 전달하지 않아 현재 주사율을 유지하도록 합니다.
            Screen.SetResolution(res.width, res.height,Screen.fullScreen);
        }

        public string GetSaveData()
        {
            var wrapper = new IntWrapper {Value = resolutionDropdown.value};
            return JsonUtility.ToJson(wrapper);
        }

        public void RestoreData(string loadedData)
        {
            if (string.IsNullOrEmpty(loadedData)) return;
            _currentResolutionIndex= JsonUtility.FromJson<IntWrapper>(loadedData).Value;
        }
    }
}