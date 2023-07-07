using UnityEngine;

namespace Stark.Core
{
    public class AttireHolder : MonoBehaviour
    {
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Head")]
        [SerializeField] private GameObject[] m_hairs;
        [SerializeField] private GameObject[] m_headgear;
        [SerializeField] private Material m_hairMaterial;
        [SerializeField] private SkinnedMeshRenderer m_headSkinnedMesh;
        [SerializeField] private SkinnedMeshRenderer m_eyeLashesSkinnedMesh;
        
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Skin")]
        [SerializeField] private Material m_skinMaterial;
        [SerializeField] private Texture[] m_skins;
        
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Cloths")]
        [SerializeField] private GameObject[] m_upperbodyList;
        [SerializeField] private GameObject[] m_lowerbodyList;
        
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Footware")]
        [SerializeField] private GameObject[] m_footwares;
        
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Accessory")]
        [SerializeField] private GameObject[] m_accessory;

        public Animator AvatarAnimator => gameObject.GetComponent<Animator>();
        public Material HairMaterial => m_hairMaterial;
        public Material SkinMaterial => m_skinMaterial;
        public Texture[] Skins => m_skins;
        public GameObject[] Hairs => m_hairs;
        public GameObject[] Headgear => m_headgear;
        public GameObject[] Upperbody => m_upperbodyList;
        public GameObject[] Lowerbody => m_lowerbodyList;
        public GameObject[] Footware => m_footwares;
        public GameObject[] Accessory => m_accessory;

        public SkinnedMeshRenderer HeadBlendShapes => m_headSkinnedMesh;
        public SkinnedMeshRenderer EyeLashes => m_eyeLashesSkinnedMesh;

        private void OnDisable()
        {
            for (var i = 0; i < m_headSkinnedMesh.sharedMesh.blendShapeCount; i++)
            {
                m_headSkinnedMesh.SetBlendShapeWeight(i, 0);
            }

        }
    }
}
