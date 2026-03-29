using System;
using Code.CoreSystem;
using GondrLib.Dependencies;
using UnityEngine;
using Work.JES.Code.Event;
using Work.JES.Code.Notes;
using Work.JES.Code.UI.Container;

namespace Work.JES.Code.UI
{
    public class PangNoteUI : CanvasGroupUI
    {
        [SerializeField] private NoteDataListSO noteDataListSO;
        [SerializeField] private NoteNodeContainer noteNodeContainer;
        [SerializeField] private NodeInfoContainer nodeInfoContainer;
        private int _currentIndex = 0;

        public void OnOffNote(bool isOn)
        {
            CanvasOnOff(isOn, false);
            if (isOn)
            {
                noteNodeContainer.EnableFor(noteDataListSO);
                nodeInfoContainer.EnableFor(noteDataListSO.NoteDataList[_currentIndex]);
                noteNodeContainer.OnClickNodeEvent.AddListener(HandleClickNode);
            }
            else
            {
                noteNodeContainer.OnClickNodeEvent.RemoveAllListeners();
                nodeInfoContainer.Disable();
                noteNodeContainer.Disable();
            }
        }

        private void HandleClickNode(AnipangDataSO arg0)
        {
            nodeInfoContainer.EnableFor(arg0);
            _currentIndex = noteDataListSO.NoteDataList.IndexOf(arg0);
        }
    }
}