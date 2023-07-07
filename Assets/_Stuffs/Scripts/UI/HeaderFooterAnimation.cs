using UnityEngine.Events;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Stark.UI{
    public class HeaderFooterAnimation : MonoBehaviour
    {
        [SerializeField]private DOTweenAnimation m_header;
        [SerializeField]private DOTweenAnimation m_footer;

        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Events")]
        public UnityEvent OnTransitionIn;
        public UnityEvent OnTransitionOut;
        public void DoTransitionIn()
        {
            m_header.DORestart();
            m_footer.DORestart();
        }
        public void DoTransitionOut()
        {
            m_header.DOPlayBackwards();
            m_footer.DOPlayBackwards();
        }
        public void TransitionIn()=> OnTransitionIn?.Invoke();
        public void TransitionOut()=> OnTransitionOut?.Invoke();
    }
    
}