using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Stark
{
    public class SplashHandler : MonoBehaviour
    {
        [SerializeField]private VideoPlayer m_videoPlayer;
        private void OnEnable() 
        {
            m_videoPlayer.loopPointReached += OnMVideoEnded;
        }
        private void OnDisable() 
        {
            m_videoPlayer.loopPointReached -= OnMVideoEnded;
        }
        
        private void OnMVideoEnded(VideoPlayer vp)
        {
            SceneManager.LoadScene("Main");
        }
        public void SkipSplash(){
            SceneManager.LoadScene("Main");
        }
    }
}
