using System;
using Code.CoreSystem;
using Code.Player;
using JES.Code.SoundSystem;
using UnityEngine;
using Work.JES.Code.Event;

namespace Work.JES.Code.Systems
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SoundSO backgroundMusic;
        private void Awake()
        {
            Bus<OnOffUIEvent>.OnEvent += HandleOnOffUI;
        }

        private void Start()
        {
            if(backgroundMusic != null)
                Bus<SoundPlayEvent>.Raise(new SoundPlayEvent(backgroundMusic));
        }

        private void OnDestroy()
        {
            Bus<OnOffUIEvent>.OnEvent -= HandleOnOffUI;
        }

        private async void HandleOnOffUI(OnOffUIEvent evt)
        {
            Bus<OnOffCursorEvent>.Raise(new OnOffCursorEvent(evt.Hash,evt.IsOn));
            Bus<PlayerMoveEvent>.Raise(new PlayerMoveEvent(evt.Hash,!evt.IsOn));
            if (!evt.IsOn)
                await Awaitable.NextFrameAsync();
            Bus<BlockEscEvent>.Raise(new BlockEscEvent(evt.Hash,evt.IsOn));
        }
    }
}