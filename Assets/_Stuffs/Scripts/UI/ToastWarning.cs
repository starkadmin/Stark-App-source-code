using System;
using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Collections;


namespace Stark.UI
{
    public class ToastWarning : MonoBehaviour
    {
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Settings")]
        public float displayTime = 2;
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Reference")]
        [SerializeField]private TextMeshProUGUI m_messageTxt;
        [SerializeField]private DOTweenAnimation m_toastAnim;

        public delegate void WarningEvent(string message);

        public static WarningEvent OnWarning;

        private void OnEnable()
        {
            OnWarning += DisplayWarning;
        }

        private void OnDisable()
        {
            OnWarning -= DisplayWarning;
        }

        public void DisplayWarning(string message){
            m_messageTxt.text = message;
            StartCoroutine(AnimRoutine());
        }

        public IEnumerator AnimRoutine(){
            m_toastAnim.DORestart();
            yield return new WaitForSeconds(displayTime);
            m_toastAnim.DOPlayBackwards();
        }

    }
}
