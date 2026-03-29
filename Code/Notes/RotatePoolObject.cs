using System;
using Code.Animals;
using Code.CoreSystem;
using DG.Tweening;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;
using Work.JES.Code.Event;

namespace Work.JES.Code.Notes
{
    [Provide]
    public class RotatePoolObject : MonoBehaviour,IDependencyProvider
    {
        [Inject] private PoolManagerMono _poolManager;
        [SerializeField] private Transform objectTrm;
        [SerializeField] private float rotateTime;
        private GameObject _currentObj;

        public void DestroyObj()
        {
            if (_currentObj == null) return;
            _currentObj.transform.DOKill();
            Destroy(_currentObj);
            _currentObj = null;
        }

        public void SetObj(AnipangDataSO data)
        {
            DestroyObj();
         
            if (data == null) return;
            Animal animal = _poolManager.Pop<Animal>(data.PoolItemSO);
            animal.IsEnable = false;
            _currentObj = animal.gameObject;
            _currentObj.transform.SetPositionAndRotation(objectTrm.position, Quaternion.identity);
            _currentObj.transform.DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
            
        }
    }
}