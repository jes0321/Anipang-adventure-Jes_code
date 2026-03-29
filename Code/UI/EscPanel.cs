using System;
using System.Collections.Generic;
using Code.Core.Events;
using Code.CoreSystem;
using Code.Player;
using Code.UI.Containers;
using JES.Code.SoundSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Work.JES.Code.Event;
using Work.JES.Code.UI.Component;

namespace Work.JES.Code.UI
{
    public enum EscPanelType
    {
        None,
        Map,
        Inventory,
        Note,
        Setting,

        //당장은 여기까지만 하면 될듯. 아래는 전투로 졸작때.
        Skill,
        Status,
    }

    public class EscPanel : CanvasGroupUI
    {
        private EscPanelType _currentPanel = EscPanelType.Setting;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private Transform btnParent;

        private PanelTypeBtn[] _panelTypeBtns;

        [Header("UI PANELS")] [SerializeField] private PangNoteUI pangNoteUI;
        [SerializeField] private SettingPanel settingPanel;
        [SerializeField] private InventoryUI inventoryUI;

        private List<int> _blockOwners = new List<int>();
        private bool _isBlockEsc => _blockOwners.Count > 0;
        private bool _isOpen = false;

        protected override void Awake()
        {
            base.Awake();
            Bus<BlockEscEvent>.OnEvent += HandleBlockEsc;
            playerInput.OnPausePressedEvent += HandleEscEvent;
            playerInput.OnInventoryKeyEvent += HandleTabEvent;

            _panelTypeBtns = btnParent.GetComponentsInChildren<PanelTypeBtn>();
            foreach (var btn in _panelTypeBtns)
            {
                btn.OnClickEvent.AddListener(HandlePanelBtnClick);
            }
        }

        private void OnDestroy()
        {
            Bus<BlockEscEvent>.OnEvent -= HandleBlockEsc;
            playerInput.OnPausePressedEvent -= HandleEscEvent;
            playerInput.OnInventoryKeyEvent -= HandleTabEvent;
        }

        private void HandleBlockEsc(BlockEscEvent evt)
        {
            if (evt.IsBlock)
            {
                if (!_blockOwners.Contains(evt.Hash))
                {
                    _blockOwners.Add(evt.Hash);
                }
            }
            else
            {
                if (_blockOwners.Contains(evt.Hash))
                {
                    _blockOwners.Remove(evt.Hash);
                }
            }
        }

        private void HandlePanelBtnClick(PanelTypeBtn btn)
        {
            Bus<PlayUISoundEvent>.Raise(new PlayUISoundEvent(UISoundType.Basic));
            if (btn.EscPanelType == _currentPanel)
            {
                return;
            }
            OnOffUI(btn.EscPanelType);
        }

        private void HandleTabEvent()
        {
            if (_isBlockEsc) return;
            
            HandleOpen(EscPanelType.Inventory);
        }

        private void HandleEscEvent()
        {
            if (_isBlockEsc) return;
            
            HandleOpen(_currentPanel);
        }

        private void HandleOpen(EscPanelType panelType)
        {
            _isOpen = !_isOpen;
            CanvasOnOff(_isOpen);
           // Bus<OnOffUIEvent>.Raise(new OnOffUIEvent(_isOpen));
           Bus<OnOffCursorEvent>.Raise(new OnOffCursorEvent(GetHashCode(),_isOpen));
           Bus<PlayerMoveEvent>.Raise(new PlayerMoveEvent(GetHashCode(),!_isOpen));
            if(_isOpen)
                OnOffUI(panelType);
        }

        private void OnOffUI(EscPanelType panelType)
        {
            AllDisable();
            switch (panelType)
            {
                case EscPanelType.Inventory:
                    OnInventory();
                    break;
                case EscPanelType.Note:
                    OnNoteUI();
                    break;
                case EscPanelType.Setting:
                    OnSetting();
                    break;
                case EscPanelType.Map:
                    OnMap();
                    break;
            }

            SettingBtn();
        }

        private void OnMap()
        {
            _currentPanel = EscPanelType.Setting;
            Bus<FadeInOutEvent>.Raise(new FadeInOutEvent(false,()=>SceneManager.LoadScene("Title")));
        }

        private void AllDisable()
        {
            inventoryUI.SetEnabled(false);
            settingPanel.Disable();
            pangNoteUI.OnOffNote(false);
        }

        private void OnInventory()
        {
            _currentPanel = EscPanelType.Inventory;
            inventoryUI.SetEnabled(true);
            //여긴 지원이 니가 채워라
            //OnNoteUI보고 해
        }

        private void OnSetting()
        {
            _currentPanel = EscPanelType.Setting;
            settingPanel.EnableFor();
        }

        private void OnNoteUI()
        {
            _currentPanel = EscPanelType.Note;
            pangNoteUI.OnOffNote(true);
        }


        private void SettingBtn()
        {
            foreach (var btn in _panelTypeBtns)
            {
                btn.OnOffBtn(btn.EscPanelType == _currentPanel);
            }
        }
    }
}