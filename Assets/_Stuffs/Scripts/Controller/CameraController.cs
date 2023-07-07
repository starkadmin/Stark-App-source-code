using UnityEngine;
using Stark.Helper.Enum;
using DG.Tweening;
namespace Stark.Controller
{
    public class CameraController : MonoBehaviour
    {
        public delegate void CameraEvent(CameraTargetType camera);
        public delegate void StartCameraEvent();

        public static CameraEvent OnCameraChange;
        public static StartCameraEvent OnStarMoveCamera;
        
        [SerializeField]private Transform[] m_camTargetPositions;

        public Ease EaseType;
        public float Duration=1;
        private CameraTargetType cameraTarget;
        public void SetCameraPosition(CameraTargetType targetType)
        {
            cameraTarget = targetType;
            OnStarMoveCamera?.Invoke();
            switch (targetType)
            {   
                case CameraTargetType.Default:
                    transform.DOMove(m_camTargetPositions[0].position,Duration).SetEase(EaseType).OnComplete(OnCameraArrived);
                break;
                case CameraTargetType.Head:
                    transform.DOMove(m_camTargetPositions[1].position,Duration).SetEase(EaseType).OnComplete(OnCameraArrived);
                break;
                case CameraTargetType.UpperBody:
                    transform.DOMove(m_camTargetPositions[2].position,Duration).SetEase(EaseType).OnComplete(OnCameraArrived);
                break;
                case CameraTargetType.LowerBody:
                    transform.DOMove(m_camTargetPositions[3].position,Duration).SetEase(EaseType).OnComplete(OnCameraArrived);
                break;
                case CameraTargetType.Footwear:
                    transform.DOMove(m_camTargetPositions[4].position,Duration).SetEase(EaseType).OnComplete(OnCameraArrived);
                break;
            }
        }
        private void OnCameraArrived()
        {
            OnCameraChange?.Invoke(cameraTarget);
        }

    }   
}
