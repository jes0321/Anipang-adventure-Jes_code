using System;
using System.Collections.Generic;
using Code.CoreSystem;
using UnityEngine;
using Work.JES.Code.Event;
using Work.JES.Code.UI;

namespace Work.JES.Code.Systems
{
    public class CursorManager : CanvasGroupUI
    {
        private List<int> _onOwners = new List<int>();

        protected override void Awake()
        {
            base.Awake();
            Bus<OnOffCursorEvent>.OnEvent += HandleOnOffCursor;
        }

        private void Start()
        {
            Bus<OnOffCursorEvent>.Raise(new OnOffCursorEvent(GetHashCode(), !awakeOn));
        }

        private void OnDestroy()
        {
            Bus<OnOffCursorEvent>.OnEvent -= HandleOnOffCursor;
        }

        private void HandleOnOffCursor(OnOffCursorEvent evt)
        {
            if (evt.IsOn)
            {
                if (!_onOwners.Contains(evt.Hash))
                {
                    _onOwners.Add(evt.Hash);
                    if (_onOwners.Count == 1)
                    {
                        CanvasOnOff(false, false);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
            else
            {
                if (_onOwners.Contains(evt.Hash))
                {
                    _onOwners.Remove(evt.Hash);
                    if (_onOwners.Count == 0)
                    {
                        CanvasOnOff(true, false);
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
        }
    }
}