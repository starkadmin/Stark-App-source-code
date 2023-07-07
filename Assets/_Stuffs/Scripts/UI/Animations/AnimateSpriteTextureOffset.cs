using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stark
{
    public class AnimateSpriteTextureOffset : MonoBehaviour
    {
        [SerializeField] private Vector2 _offsetMovementSpeed = Vector2.one;
        private Image _imageComponent;

        private void Start()
        {
            _imageComponent = GetComponent<Image>();

            // clone the image material so it won't affect all sprites with the default UI shader
            _imageComponent.material = new Material(_imageComponent.material);
        }

        void Update()
        {
            _imageComponent.material.mainTextureOffset += _offsetMovementSpeed * Time.deltaTime;
        }
    }
}
