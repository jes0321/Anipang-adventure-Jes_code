using System;
using Code.UI;
using UnityEngine;
using UnityEngine.Events;
using Work.JES.Code.Notes;
using Work.JES.Code.UI.Component;

namespace Work.JES.Code.UI.Container
{
    public class NoteNodeContainer : MonoBehaviour,IUIElement<NoteDataListSO>
    {
        [SerializeField] private GameObject nodeUiPrefab;
        [SerializeField] private RectTransform contentTrm;
        
        public UnityEvent<AnipangDataSO> OnClickNodeEvent;
        private NoteNodeUI _currentNode;
        public void EnableFor(NoteDataListSO item)
        {
            var size = contentTrm.sizeDelta;
            size.y = item.NoteDataList.Count * 50 + 200;
            contentTrm.sizeDelta = size;
            foreach (var noteData in item.NoteDataList)
            {
                var nodeUi = Instantiate(nodeUiPrefab, contentTrm).GetComponent<NoteNodeUI>();
                nodeUi.EnableFor(noteData, OnClickNodeEvent);
            }
        }
        public void Disable()
        {
            var child = contentTrm.GetComponentsInChildren<NoteNodeUI>();
            foreach (var node in child)
            {
                Destroy(node.gameObject);
            }
        }
    }
}