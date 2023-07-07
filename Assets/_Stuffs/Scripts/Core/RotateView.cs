using Stark.Controller;
using Stark.Helper.Enum;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Stark.Core
{
    public class RotateView : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private float m_sensitvity = 0.3f;
        [SerializeField] private float m_releaseSpeed = 3.0f;
        
        private Vector3 m_rotationAxis = Vector3.up;
        private float m_fRotationSpeed;
        private float m_fLastMouseX;

        private bool canRotate = true;
        private bool isHit;
        private Quaternion originalRot;
        private void Start()
        {
            originalRot = transform.rotation;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
        public void OnDrag(PointerEventData eventData)
        {
            if(!canRotate) return;
            if (!(Input.mousePosition.y > 100)) return;
            var v3Angles = m_rotationAxis * -((eventData.position.x - m_fLastMouseX) * m_sensitvity);
            transform.Rotate(v3Angles, Space.Self);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!(Input.mousePosition.y > 100) || !canRotate) 
                return;

            m_fRotationSpeed = -(eventData.position.x - m_fLastMouseX) * m_releaseSpeed;
        }

        private void Update()
        {
            if(!canRotate) return;

            var v3Angles = m_rotationAxis * (m_fRotationSpeed * Time.deltaTime);
            transform.Rotate(v3Angles, Space.Self);
            m_fLastMouseX = Input.mousePosition.x;
        }
        
        private void OnCameraChange(CameraTargetType cameraTargetType)
        {
            var _isDefault = cameraTargetType == CameraTargetType.Default;
            canRotate = _isDefault;
        }
        private void ResetValue()
        {
            canRotate = false;
            transform.rotation = originalRot;
            m_fRotationSpeed = 0;
            m_fLastMouseX = 0;
        }

        private void OnEnable()
        {
            CameraController.OnStarMoveCamera += ResetValue;
            CameraController.OnCameraChange += OnCameraChange;
        }
        
        private void OnDisable()
        {
            CameraController.OnStarMoveCamera -= ResetValue;
            CameraController.OnCameraChange -= OnCameraChange;
        }



    }
}

