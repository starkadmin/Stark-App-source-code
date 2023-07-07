using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
namespace Stark.UI
{
    public class TypeWriter : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_tmpProText;
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Settings")]
        [SerializeField] private float m_delayBeforeStart = 0f;
        [SerializeField] private float m_timeBtwChars = 0.1f;
        [SerializeField] private string m_leadingChar = "";
        [SerializeField] private bool m_leadingCharBeforeDelay = false;
        [Space(10)] [SerializeField] private bool m_startOnEnable = false;
        [SerializeField] private bool clearAtStart = false;
        private string _writer;

        [SerializeField] private AudioSource _typingAudio;

         [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Event")]
         public UnityEvent OnTypingStarted = new UnityEvent();
         public UnityEvent OnTypingEnded = new UnityEvent();
        // Use this for initialization
        void Awake()
        {
            if (m_tmpProText != null)_writer = m_tmpProText.text;
        }

        void Start()
        {
            if (!clearAtStart ) return;            
            if (m_tmpProText != null) m_tmpProText.text = "";
        }

        private void OnEnable()
        {
            if(m_startOnEnable) StartTypewriter();
        }
        private void StartTypewriter()
        {
            StopAllCoroutines();
            if (m_tmpProText != null)
            {
                m_tmpProText.text = "";
                StartCoroutine("TypeWriterTMP");
            }
        }

        private void OnDisable()
        {
            _typingAudio.Stop();
            StopAllCoroutines();
        }

        IEnumerator TypeWriterTMP()
        {
            m_tmpProText.text = m_leadingCharBeforeDelay ? m_leadingChar : "";
            yield return new WaitForSeconds(m_delayBeforeStart);
            OnTypingStarted.Invoke();
            foreach (char c in _writer)
            {
                if (m_tmpProText.text.Length > 0)
                {
                    m_tmpProText.text = m_tmpProText.text.Substring(0, m_tmpProText.text.Length - m_leadingChar.Length);
                }
                m_tmpProText.text += c;
                m_tmpProText.text += m_leadingChar;
                _typingAudio.Play();
               
                yield return new WaitForSeconds(m_timeBtwChars);
            }
            OnTypingEnded.Invoke();
            if (m_leadingChar != "")
            {
                m_tmpProText.text = m_tmpProText.text.Substring(0, m_tmpProText.text.Length - m_leadingChar.Length);
            }
        }
    }  
}

