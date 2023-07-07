using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ScrollSnaps;
namespace Stark.Handler
{
    public class AdsHandler : MonoBehaviour
    {
        [SerializeField]private float m_displayTime=30;
        [SerializeField]private DirectionalScrollSnap m_scrollSnap;

        void OnEnable()
        {
            StartCoroutine(AdsCarouselRoutine());
        }

        IEnumerator AdsCarouselRoutine(){
            while (true)
            {
                yield return new WaitForSeconds(m_displayTime);
                m_scrollSnap.OnForward();
                yield return null;
            }
        }
    }
}
