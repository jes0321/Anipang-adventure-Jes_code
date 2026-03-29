using System;
using Code.CoreSystem;
using Code.UI;
using JES.Code.SoundSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Work.JES.Code.Event;
using Work.JES.Code.Notes;

namespace Work.JES.Code.UI.Component
{
    public class NoteNodeUI : MonoBehaviour,IUIElement<AnipangDataSO,UnityEvent<AnipangDataSO>>,IPointerDownHandler
    {
        [SerializeField] private TextMeshProUGUI nameTxt, numberTxt;
        [SerializeField] private Image backImg;
        [SerializeField] private Image iconImg;
        [SerializeField] private Color notCatchColor;
        [SerializeField] private Sprite notCatchSprite;

        private Sprite _defaultSprite;

        private UnityEvent<AnipangDataSO> _callBackEvent;
        private AnipangDataSO _noteData;

        private void Awake()
        {
            _defaultSprite = backImg.sprite;
        }

        public void EnableFor(AnipangDataSO item, UnityEvent<AnipangDataSO> callback)
        {
            _noteData = item;
            _callBackEvent = callback;
            numberTxt.SetText(item.Number.ToString("D3"));
            if (item.IsFind == false)
            {
                nameTxt.SetText("");
                iconImg.sprite = null;
                iconImg.color = Color.clear;
            }
            else
            {
                nameTxt.SetText(item.Name);
                iconImg.sprite = item.Icon;
            }
            iconImg.color = !item.IsCatch ? notCatchColor : Color.white;
            backImg.sprite = !item.IsCatch ? notCatchSprite : _defaultSprite;
        }

        public void Disable()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Basic));
            _callBackEvent?.Invoke(_noteData);
        }
    }
}