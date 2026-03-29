using DG.Tweening;
using UnityEngine;

namespace Work.JES.Code.UI
{
    public class CanvasGroupUI : MonoBehaviour
    {
        [Header("CanvasGroup")] 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.2f;
        [SerializeField] protected bool awakeOn = false;

        protected virtual void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    Debug.LogError("CanvasGroup is not assigned or found on the GameObject.");
                    return;
                }
            }
            CanvasOnOff(awakeOn,false);
        }

        /// <summary>
        /// 캔버스 그룹을 껐다 키기,
        /// </summary>
        /// <param name="isOn">끌지 켤지 정하기</param>
        protected virtual void CanvasOnOff(bool isOn, bool isFade = true)
        {
            if (canvasGroup == null)
            {
                Debug.LogError("CanvasGroup is not assigned or found on the GameObject.");
                return;
            }

            canvasGroup.interactable = isOn;
            canvasGroup.blocksRaycasts = isOn;

            float alpha = isOn ? 1f : 0f;
            if (isFade)
            {
                canvasGroup.DOFade(alpha, fadeDuration)
                    .OnComplete(() => OnOffEndMethod(isOn));
            }
            else
            {
                canvasGroup.alpha = alpha;
                OnOffEndMethod(isOn);
            }
        }

        /// <summary>
        /// 캔버스 On/Off가 끝났을 때 호출되는 메서드.
        /// </summary>
        /// <param name="isOn"> 캔버스가 켜진건지 꺼진건지 알 수 있음</param>
        protected virtual void OnOffEndMethod(bool isOn)
        {
        }
    }
}