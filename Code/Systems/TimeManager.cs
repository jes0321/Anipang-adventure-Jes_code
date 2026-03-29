using System;
using Code.CoreSystem;
using Core.CoreSystem.SaveSystem;
using PolyverseSkiesAsset;
using UnityEngine;
using Work.JES.Code.Event;

namespace Work.JES.Code.Systems
{
    public class TimeManager : MonoBehaviour
    {
        private int _currentDate = 1;
        private float _currentTime;
        [SerializeField] private int dayTimePerMin = 5;
        private TimeOfDay _currentTimeOfDay;
        [SerializeField] private PolyverseSkies skyBox;
        [SerializeField] private Light directionalLight;
        
        private float _normalizedDayTime;
        
        private void Awake()
        {
            _currentTime = 0;
            _currentTimeOfDay = TimeOfDay.Day;
            Bus<DateChangeEvent>.Raise(new DateChangeEvent(_currentDate));
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            float dayTime = dayTimePerMin * 60f;

            float normalized = (_currentTime % dayTime) / dayTime;

            _normalizedDayTime = (Mathf.Sin(normalized * Mathf.PI * 2 - Mathf.PI / 2f) + 1f) / 2f;

            skyBox.timeOfDay = _normalizedDayTime;
            directionalLight.intensity = (1 - _normalizedDayTime) * 1.25f;

            if (normalized >= 0.5f && _currentTimeOfDay == TimeOfDay.Day)
            {
                _currentTimeOfDay = TimeOfDay.Night;
                Bus<DayNightChangeEvent>.Raise(new DayNightChangeEvent(_currentTimeOfDay));
            }
            else if (normalized < 0.5f && _currentTimeOfDay == TimeOfDay.Night)
            {
                _currentTimeOfDay = TimeOfDay.Day;
                Bus<DayNightChangeEvent>.Raise(new DayNightChangeEvent(_currentTimeOfDay));
            }

            if (_currentTime >= dayTime)
            {
                _currentDate++;
                _currentTime = 0;
                Bus<DateChangeEvent>.Raise(new DateChangeEvent(_currentDate));
                Bus<SaveGameEvent>.Raise(new SaveGameEvent(false));
            }
        }

        [ContextMenu("Test")]
        public void Test()
        {
            _currentDate++;
            _currentTime = 0;
            Bus<DateChangeEvent>.Raise(new DateChangeEvent(_currentDate));
        }
    }
}