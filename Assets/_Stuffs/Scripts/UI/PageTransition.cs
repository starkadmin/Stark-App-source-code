using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DG.Tweening;
using Stark.Manager;
using Stark.Helper.Enum;

namespace Stark.UI{
    public class PageTransition : MonoBehaviour
    {
        [SerializeField]private float m_transitionTime = 2;
        public Image TransitionImage;
        public  IEnumerator TransitionRoutine(ScreenEnum.MainScreenType screenType){
            TransitionImage.raycastTarget = true;
            TransitionImage.DOFade(1,1).OnComplete(()=>{
                UIManager.PageTransitionEnded?.Invoke(screenType);
            });
            yield return new WaitForSeconds(m_transitionTime);
            TransitionImage.DOFade(0,1).OnComplete(()=>{
                TransitionImage.raycastTarget = false;
            });
        }
    }
}
