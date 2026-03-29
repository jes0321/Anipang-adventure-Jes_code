using System;
using Code.CoreSystem;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;
using Work.JES.Code.Event;

namespace JES.Code.SoundSystem
{
    [Serializable]
    public struct UISoundData
    {
        public UISoundType SoundType;
        public SoundSO SoundSO;
    }

    public class CreateManager : MonoBehaviour
    {
        [Inject] private PoolManagerMono _poolManager;
        [SerializeField] private PoolItemSO soundPlayerSO;
        [SerializeField] private UISoundData[] uiSoundData;

        private void Awake()
        {
            Bus<SoundPlayEvent>.OnEvent += HandlePlaySound;
            Bus<PlayUISoundEvent>.OnEvent += HandlePlayUI;
        }

        private void OnDestroy()
        {
            Bus<SoundPlayEvent>.OnEvent -= HandlePlaySound;
            Bus<PlayUISoundEvent>.OnEvent -= HandlePlayUI;
        }

        private void HandlePlayUI(PlayUISoundEvent evt)
        {
            foreach (var data in uiSoundData)
            {
                if (data.SoundType == evt.SoundType)
                {
                    PlaySound(data.SoundSO, Vector3.zero);
                    return;
                }
            }
        }

        private void PlaySound(SoundSO soundSO, Vector3 position)
        {
            SoundPlayer player = _poolManager.Pop<SoundPlayer>(soundPlayerSO);
            player.transform.position = position;
            player.PlaySound(soundSO);
        }
        private void HandlePlaySound(SoundPlayEvent evt)
        {
            PlaySound(evt.Sound, evt.Position);
        }
    }
}