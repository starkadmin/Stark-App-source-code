using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Stark
{
    public class PromptAnim : MonoBehaviour
    {
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Settings")]
        [SerializeField] private float m_transitionInTime = 1;
        [SerializeField] private float m_transitionInOut = 0.5f;
        [SerializeField]private CanvasGroup m_canvasGroup;
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Events")]
        public UnityEvent OnOpened = new UnityEvent();
        public UnityEvent OnClosed = new UnityEvent();
        void OnEnable()
        {
            DisplayPrompt();
        }
        public void DisplayPrompt(){
            gameObject.transform.localScale = Vector3.zero;
            m_canvasGroup.alpha = 0;
            gameObject.transform.DOScale(new Vector3(1,1,1),m_transitionInTime).SetEase(Ease.OutElastic).OnComplete(()=>{
                OnOpened.Invoke();
                DOTween.Kill(gameObject);
            });
            m_canvasGroup.DOFade(1,m_transitionInTime);
        }

        public void ClosePrompt(){
            gameObject.transform.DOScale(new Vector3(0,0,0),m_transitionInOut).SetEase(Ease.InBack);
            m_canvasGroup.DOFade(1,m_transitionInOut).OnComplete(()=>{
                transform.parent.gameObject.SetActive(false);
                DOTween.Kill(gameObject);
                OnClosed.Invoke();
            });
        }
    }
}
