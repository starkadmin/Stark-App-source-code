using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace Stark.UI
{
    public class PumpAnim : MonoBehaviour
    {
        [SerializeField]private GameObject m_normalArvis;
        [SerializeField]private GameObject m_glowingArvis;
        private bool _isAnimate;
        public void StartPumpAnim(){
            _isAnimate=true;
            StartCoroutine(PumpRoutine());
        }
        public void StopPumpAnim(){
            _isAnimate=false;
        }
        public IEnumerator PumpRoutine(){
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.05f,0.3f));
                 if(!_isAnimate) yield break;
                DoGlowingArvis(true);
                gameObject.transform.DOScale(new Vector3 (1.1f,1.1f,1.1f),0.2f).OnComplete(()=>{
                    gameObject.transform.DOScale(new Vector3 (1f,1f,1f),0.2f);
                    DoGlowingArvis(false);
                    return;
                });
            }
           
        }
        void OnDisable()
        {
            _isAnimate=false;
        }

        private void DoGlowingArvis(bool isGlow){
            if(isGlow){
                m_glowingArvis.SetActive(true);
                m_normalArvis.SetActive(false);
            }else{
                m_glowingArvis.SetActive(false);
                m_normalArvis.SetActive(true);
            }
        }

        
    }
}
