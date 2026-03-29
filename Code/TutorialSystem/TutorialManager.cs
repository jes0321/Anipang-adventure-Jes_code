using System;
using Code.CoreSystem;
using UnityEngine;
using Work.JES.Code.Event;

namespace Work.JES.Code.TutorialSystem
{
    public enum TutorialType
    {
        None,   
        BuyResource,
        PlaceFence,
        BuyGatcha,
        PlaceAnipang,
        GetDropItem,
        SellItem,
    }

    [Serializable]
    public struct TutorialData
    {
        public TutorialType TutorialType;
        [TextArea] public string OrderText;
        public string BoxTitleText;
    }

    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private TutorialData[] tutorialDatas;
        [SerializeField] [TextArea] private string[] orders;
        private int _currentIndex = 0;
        private bool _isTutorialComplete = false;
        //private readonly string _currentSave = "CurrentTutorialIndex";
        //private readonly string _saveKey = "TutorialCompleted";
        private int _orderIndex = 0;

        private void Awake()
        {
            Bus<TutorialActionEvent>.OnEvent += HandleTutorialActionEvent;
            Bus<OffDialogEvent>.OnEvent += HandleOffDiaglog;
            // if (PlayerPrefs.GetInt(_saveKey, 0) == 1)
            // {
            //     _isTutorialComplete = true;
            //     Destroy(gameObject);
            // }
        }

        private void Start()
        {
            //_currentIndex = PlayerPrefs.GetInt(_currentSave, 0);
            Bus<OnDialogEvent>.Raise(new OnDialogEvent(tutorialDatas[_currentIndex].OrderText));
        }

        private void OnDestroy()
        {
            Bus<TutorialActionEvent>.OnEvent -= HandleTutorialActionEvent;
            Bus<OffDialogEvent>.OnEvent -= HandleOffDiaglog;

            //PlayerPrefs.SetInt(_currentSave, _currentIndex);
            //PlayerPrefs.SetInt(_saveKey, _isTutorialComplete ? 1 : 0);
            //PlayerPrefs.Save();
        }

        private void HandleOffDiaglog(OffDialogEvent evt)
        {
            if (_isTutorialComplete)
            {
                _orderIndex++;
                if (_orderIndex > orders.Length - 1)
                {
                    Destroy(gameObject);
                    return;
                }
                Bus<OnDialogEvent>.Raise(new OnDialogEvent(orders[_orderIndex]));
            }
            else
            {
                Bus<OnBoxEvent>.Raise(new OnBoxEvent(tutorialDatas[_currentIndex].BoxTitleText));
            }
        }

        private void HandleTutorialActionEvent(TutorialActionEvent evt)
        {
            if (_currentIndex > tutorialDatas.Length) return;
            Debug.Log($"{tutorialDatas[_currentIndex].TutorialType} : {evt.TutorialType}");
            if (tutorialDatas[_currentIndex].TutorialType != evt.TutorialType) return;
            Bus<OffBoxEvent>.Raise(new OffBoxEvent());
            _currentIndex++;
            if (_currentIndex > tutorialDatas.Length - 1)
            {
                Bus<OnDialogEvent>.Raise(new OnDialogEvent(orders[_orderIndex]));
                _isTutorialComplete = true;
                return;
            }
            Bus<OnDialogEvent>.Raise(new OnDialogEvent(tutorialDatas[_currentIndex].OrderText));    
        }
    }
}