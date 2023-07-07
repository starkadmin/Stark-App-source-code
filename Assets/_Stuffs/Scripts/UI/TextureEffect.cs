using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Stark
{
    public class TextureEffect : MonoBehaviour
    {
        public Material material;
        void Start()
        {
            material = GetComponent<Material>();
        }

     
    }
}
