using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
namespace Stark.UI
{
    public class PanelScaler : MonoBehaviour
    {
        public DOTweenAnimation animations;
        public void MaximizePanel(){
            animations.DORestart();
        }
        public void MinimizePanel(){
            animations.DOPlayBackwards();
            
        }

    }
}
