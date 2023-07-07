using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stark.Controller
{
    public class GlowEffectController : MonoBehaviour
    {
        [SerializeField]private GameObject m_glowEffect;
        public void EnableGlowEffect(bool isEnable){
           m_glowEffect.gameObject.SetActive(isEnable);
        }
    }
}
