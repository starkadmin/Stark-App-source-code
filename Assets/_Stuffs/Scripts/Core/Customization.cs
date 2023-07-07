using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Stark.Controller;
using Stark.Helper.Enum;
using Stark.Manager;
using Stark.Model;
using UnityEngine;

namespace Stark.Core
{
    public class Customization : MonoBehaviour
    {
        [Space(10), UProperty(14f, UPropertyAttribute.EPropertyStyle.Header, "Body Type")]
        [SerializeField] private GameObject[] m_bodyTypeList;

        private float timeBetweenCliks = 0.3f;
        private int clickCount = 0;
        
        private AttireHolder attireHolder;
        private Texture[] skins;
        private GameObject[] lowerBodyAttires;
        private GameObject[] uppperbodyAttires;
        private GameObject[] hairAttires;
        private GameObject[] headgearAttires;
        private GameObject[] footwareAttires;
        private GameObject[] accessoryAttires;
        private SkinnedMeshRenderer headSkinnedMeshRenderer;
        private SkinnedMeshRenderer eyelashesSkinnedMeshRenderer;
        private Material skinMaterial;
        private Material hairMaterial;
        
        private RuntimeEnum.BodyType bodyTypeIndex;
        private int skinIndex;
        private int hairIndex;
        private int headgearIndex;
        private int clothesIndex;
        private int pantsIndex;
        private int footwareIndex;
        private int accessoryIndex;
        private Color hairColorIndex;

        private int eyesBlendIndex;
        private int noseBlendIndex;
        private int lipsBlendIndex;
        
        private AttireModel.AttireCountData attireIndexDatas = new AttireModel.AttireCountData();

        private Animator animator;
        private DataManager dataManager;
        
        private readonly int isCheckUpper = Animator.StringToHash("IsCheckUpper");
        private readonly int isCheckLower = Animator.StringToHash("IsCheckLower");
        private readonly int isGesture = Animator.StringToHash("isGesture");
        
        private Dictionary<string, int> headBlendShapeToIndex = new Dictionary<string, int>()
        {
            {"EYE_0", 0},
            {"EYE_1", 0},
            {"EYE_2", 0},
            {"EYE_3", 0},
            {"LIPS_0", 0},
            {"LIPS_1", 0},
            {"LIPS_2", 0},
            {"LIPS_3", 0},
            {"NOSE_0", 0},
            {"NOSE_1", 0},
            {"NOSE_2", 0},
            {"NOSE_3", 0},
        };
        
        private Dictionary<string, int> eyelashesBlendShapeToIndex = new Dictionary<string, int>()
        {
            {"EYELASHES_0", 0},
            {"EYELASHES_1", 0},
            {"EYELASHES_2", 0},
            {"EYELASHES_3", 0},
        };

        private void Start()
        {
            dataManager = DataManager.Instance;
        }

        private void OnMouseDown()
        {
            if(UIManager.instance.currentScreen != ScreenEnum.MainScreenType.Home) return;
            clickCount++;
            if (clickCount == 2)
            {
                var _rand = UnityEngine.Random.Range(0, 3);
                var _animPlay = _rand switch
                {
                    0 => isCheckUpper,
                    1 => isCheckLower,
                    2 => isGesture,
                    _ => 2
                };
                AnimationTrigger(_animPlay);
                clickCount = 0; 
            }
            else
            {
                StartCoroutine(ClickRoutine());
                IEnumerator ClickRoutine()
                {
                    yield return new WaitForSeconds(timeBetweenCliks);
                    clickCount = 0; 
                }
            }
        }

        #region Body Type

        private void AssignBodyType(int _id)
        {
            bodyTypeIndex = (RuntimeEnum.BodyType) _id;
            SetupBodyAttire();
        }

        private void AssignNextBodyType()
        {
            bodyTypeIndex += 1;
            if ((int) bodyTypeIndex > 2)
            {
                bodyTypeIndex = 0;
            }
            SetupBodyAttire();
        }
        
        private void AssignPrevBodyType()
        {
            bodyTypeIndex -= 1;
            if ((int) bodyTypeIndex < 0)
            {
                bodyTypeIndex = (RuntimeEnum.BodyType)2;
            }
            SetupBodyAttire();
        }

        private void SetupBodyAttire()
        {
            attireHolder = m_bodyTypeList[(int) bodyTypeIndex].GetComponent<AttireHolder>();
            skins = attireHolder.Skins;
            lowerBodyAttires = attireHolder.Lowerbody;
            uppperbodyAttires = attireHolder.Upperbody;
            hairAttires = attireHolder.Hairs;
            headgearAttires = attireHolder.Headgear;
            footwareAttires = attireHolder.Footware;
            accessoryAttires = attireHolder.Accessory;
            skinMaterial = attireHolder.SkinMaterial;
            hairMaterial = attireHolder.HairMaterial;
            animator = attireHolder.AvatarAnimator;
            headSkinnedMeshRenderer = attireHolder.HeadBlendShapes;

  
            
            AssignSkin(skinIndex);
            AttireSetter((int) bodyTypeIndex, m_bodyTypeList);
            AssignUpperbodyAttires(clothesIndex);
            AssignLowerbodyAttires(pantsIndex);
            AssignFootware(footwareIndex);
            AssignHair(hairIndex);
            
            foreach (var heads in headBlendShapeToIndex.Keys.ToList())
            {
                AssignHeadBlendShape(heads, headBlendShapeToIndex[heads]);
            }

            if (attireHolder.EyeLashes == null) return;
            eyelashesSkinnedMeshRenderer = attireHolder.EyeLashes;
            AssignEyeLashesBlendShape(eyesBlendIndex, eyelashesBlendShapeToIndex[eyelashesBlendShapeToIndex.Keys.ToList()[eyesBlendIndex]]);
            // AssignHeadgear(headgearIndex);

        }
        #endregion
        
        #region Skin
        private void AssignSkin(int _id)
        {
            skinIndex = _id;
            skinMaterial.SetTexture("_BaseMap", skins[skinIndex]);
        }
        private void AssignNextSkin()
        {
            skinIndex += 1;
            if (skinIndex > skins.Length - 1)
            {
                skinIndex = 0;
            }

            skinMaterial.SetTexture("_BaseMap", skins[skinIndex]);
            AttireIndexSetter();
        }
        
        private void AssignPrevSkin()
        {
            skinIndex -= 1;
            if (skinIndex < 0)
            {
                skinIndex = skins.Length - 1;
            }
            skinMaterial.SetTexture("_BaseMap", skins[skinIndex]);
            AttireIndexSetter();
        }

        #endregion
        
        #region Head
        
        private void AssignHairColor(Color _color)
        {
            hairMaterial.color = _color;
            hairColorIndex = _color;
        }

        private void AssignHair(int _id)
        {
            hairIndex = _id;
            AttireSetter(hairIndex, hairAttires);
        }
        
        private void AssignNextHair()
        {
            hairIndex += 1;
            if (hairIndex > hairAttires.Length - 1)
            {
                hairIndex = 0;
            }
            AttireSetter(hairIndex, hairAttires);
        }
        
        private void AssignPrevHair()
        {
            hairIndex -= 1;
            if (hairIndex < 0)
            {
                hairIndex = hairAttires.Length - 1;
            }

            AttireSetter(hairIndex, hairAttires);
        }
        
        private void AssignHeadgear(int _id)
        {
            headgearIndex = _id;
            AttireSetter(headgearIndex, headgearAttires);
        }
        
        private void AssignNextHeadgear()
        {
            headgearIndex += 1;
            Debug.Log(headgearIndex);
            if (headgearIndex > headgearAttires.Length - 1)
            {
                headgearIndex = 0;
            }
            AttireSetter(headgearIndex, headgearAttires);
        }
        
        private void AssignPrevHeadgear()
        {
            headgearIndex -= 1;
            if (headgearIndex < 0)
            {
                headgearIndex = headgearAttires.Length - 1;
            }

            AttireSetter(headgearIndex, headgearAttires);
        }

        

        #endregion
        
        #region Upper Body

        private void AssignUpperbodyAttires(int _id)
        {
            clothesIndex = _id;
            AttireSetter(clothesIndex, uppperbodyAttires);
        }
        
        private void AssignNextUpperbodyAttires()
        {
            clothesIndex += 1;
            if (clothesIndex > uppperbodyAttires.Length - 1)
            {
                clothesIndex = 0;
            }
            AttireSetter(clothesIndex, uppperbodyAttires);
            AnimationTrigger(isCheckUpper);
        }
        
        private void AssignPrevUpperbodyAttires()
        {
            clothesIndex -= 1;
            if (clothesIndex < 0)
            {
                clothesIndex = uppperbodyAttires.Length - 1;
            }

            AttireSetter(clothesIndex, uppperbodyAttires);
            AnimationTrigger(isCheckUpper);
        }


        #endregion
        
        #region Lower Body

        private void AssignLowerbodyAttires(int _id)
        {
            pantsIndex = _id;
            AttireSetter(pantsIndex, lowerBodyAttires);
        }
        
        private void AssignNextLowerbodyAttires()
        {
            pantsIndex += 1;
            if (pantsIndex > lowerBodyAttires.Length - 1)
            {
                pantsIndex = 0;
            }
            AttireSetter(pantsIndex, lowerBodyAttires);
            AnimationTrigger(isCheckLower);
        }
        
        private void AssignPrevLowerbodyAttires()
        {
            pantsIndex -= 1;
            if (pantsIndex < 0)
            {
                pantsIndex = lowerBodyAttires.Length - 1;
            }

            AttireSetter(pantsIndex, lowerBodyAttires);
            AnimationTrigger(isCheckLower);
        }
        
        private void AssignFootware(int _id)
        {
            footwareIndex = _id;
            AttireSetter(footwareIndex, footwareAttires);
        }
        
        private void AssignNextAssignFootware()
        {
            footwareIndex += 1;
            if (footwareIndex > footwareAttires.Length - 1)
            {
                footwareIndex = 0;
            }
            AttireSetter(footwareIndex, footwareAttires);
            AnimationTrigger(isCheckLower);
        }
        
        private void AssignPrevAssignFootware()
        {
            footwareIndex -= 1;
            if (footwareIndex < 0)
            {
                footwareIndex = footwareAttires.Length - 1;
            }

            AttireSetter(footwareIndex, footwareAttires);
            AnimationTrigger(isCheckLower);
        }
        

        #endregion

        #region Accessories

        private void AssignAccessories(int _id)
        {
            accessoryIndex = _id;
            AttireSetter(accessoryIndex, accessoryAttires);
        }
        
        private void AssignNextAccessories()
        {
            accessoryIndex += 1;
            if (accessoryIndex > accessoryAttires.Length - 1)
            {
                accessoryIndex = 0;
            }
            AttireSetter(accessoryIndex, accessoryAttires);
        }
        
        private void AssignPrevAccessories()
        {
            accessoryIndex -= 1;
            if (accessoryIndex < 0)
            {
                accessoryIndex = accessoryAttires.Length - 1;
            }

            AttireSetter(accessoryIndex, accessoryAttires);
        }
        

        #endregion

        private void AssignHeadBlendShape(string _type, int _id)
        {
            headBlendShapeToIndex[_type] = _id;
            BlendShapeSetter(headSkinnedMeshRenderer, headBlendShapeToIndex.Keys.ToList().IndexOf(_type), _id);
        }


        private void AssignNextHeadBlendShapes(string _type)
        {
            var _key = headBlendShapeToIndex.Keys;
            var _bsList = _key.Where(x => x.Contains(_type.ToUpper())).ToList();
            foreach (var part in _bsList)
            {
                headBlendShapeToIndex[part] = 0;
                BlendShapeSetter(headSkinnedMeshRenderer, _key.ToList().IndexOf(part),
                   0);
            }
            var _index = _type switch
            {
                "EYE" => eyesBlendIndex  + _key.ToList().IndexOf(_bsList[0]),
                "NOSE" => noseBlendIndex + _key.ToList().IndexOf(_bsList[0]),
                "LIPS" => lipsBlendIndex + _key.ToList().IndexOf(_bsList[0]),
                _ => 0
            };
            if (_index == 0)
            {
                _index = _key.ToList().IndexOf(_bsList[0]);
            }
            _index += 1;
            headBlendShapeToIndex[_key.ElementAt(_index)] = 100;
            var _activeKey = _key.ToList().IndexOf(_key.ElementAt(_index));
            switch (_type)
            {
                case "EYE":
                    eyesBlendIndex = _bsList.IndexOf(_key.ElementAt(_index));
                    AssignEyeLashesBlendShape(eyesBlendIndex, 100);
                    break;
                case "NOSE":
                    noseBlendIndex = _bsList.IndexOf(_key.ElementAt(_index));
                    break;
                case "LIPS":
                    lipsBlendIndex = _bsList.IndexOf(_key.ElementAt(_index));
                    break;
            }
            BlendShapeSetter(headSkinnedMeshRenderer, _activeKey, 100);
        }
        
        private void AssignPrevHeadBlendShapes(string _type)
        {
            var _key = headBlendShapeToIndex.Keys;
            var _bsList = _key.Where(x => x.Contains(_type.ToUpper())).ToList();
            
            foreach (var part in _bsList)
            {
                headBlendShapeToIndex[part] = 0;
                BlendShapeSetter(headSkinnedMeshRenderer, _key.ToList().IndexOf(part),
                    0);
            }
            var _index = _type switch
            {
                "EYE" => eyesBlendIndex  + _key.ToList().IndexOf(_bsList[0]),
                "NOSE" => noseBlendIndex + _key.ToList().IndexOf(_bsList[0]),
                "LIPS" => lipsBlendIndex + _key.ToList().IndexOf(_bsList[0]),
                _ => 0
            };
            _index -= 1;
            headBlendShapeToIndex[_key.ElementAt(_index)] = 100;
            var _activeKey = _key.ToList().IndexOf(_key.ElementAt(_index));
            switch (_type)
            {
                case "EYE":
                    eyesBlendIndex = _bsList.IndexOf(_key.ElementAt(_index));
                    AssignEyeLashesBlendShape(eyesBlendIndex, 100);
                    break;
                case "NOSE":
                    noseBlendIndex = _bsList.IndexOf(_key.ElementAt(_index));
                    break;
                case "LIPS":
                    lipsBlendIndex = _bsList.IndexOf(_key.ElementAt(_index));
                    break;
            }
            BlendShapeSetter(headSkinnedMeshRenderer, _activeKey, 100);
        }
        
        private void AssignEyeLashesBlendShape(int index, int value)
        {
            if (eyelashesSkinnedMeshRenderer == null) return;
            var _keys = eyelashesBlendShapeToIndex.Keys.ToList();
            
            foreach (var part in _keys)
            {
                eyelashesBlendShapeToIndex[part] = 0;
                BlendShapeSetter(eyelashesSkinnedMeshRenderer, _keys.IndexOf(part),
                    0);
            }
            eyelashesBlendShapeToIndex[_keys.ElementAt(index)] = value;
            BlendShapeSetter(eyelashesSkinnedMeshRenderer, index, value);
        }
          
        
        #region Public Function

        public void SaveAvatarData()
        {
            SaveData();
        }
        public void DoneAnimation()
        {
            if (animator != null)
            {
                animator.SetTrigger(isGesture);
            }
        }


        #endregion

        #region Data Serialization

        /// <summary>
        /// Attire setter on player data load
        /// </summary>
        public void SetupOnLoad(AvatarModel avatar)
        {
            var _attireData = avatar.attireData;
            hairIndex = _attireData.hair.attireIndex;
            clothesIndex = _attireData.upperbody.attireIndex;
            pantsIndex = _attireData.lowerbody.attireIndex;
            skinIndex = _attireData.skin.attireIndex;
            footwareIndex = _attireData.footware.attireIndex;
            eyesBlendIndex = _attireData.eyes_blendShapes.attireIndex;
            noseBlendIndex = _attireData.nose_blendShapes.attireIndex;
            lipsBlendIndex = _attireData.lips_blendShapes.attireIndex;
            
            AssignBodyType(_attireData.bodytype.attireIndex);
            gameObject.SetActive(true);
            
            
            if (_attireData.headBlendShapesJson.IsNullOrEmpty()) return;
            var _headBlends = AttireModel.LoadBlendShapesFromJson(_attireData.headBlendShapesJson);

            foreach (var blends in _headBlends.Keys.Where(blends => headBlendShapeToIndex.ContainsKey(blends)))
            {
                AssignHeadBlendShape(blends, _headBlends[blends]);
            }
            
            if (!_attireData.eyelashesBlendShapesJson.IsNullOrEmpty())
            {
                var _eyelashesBlend = AttireModel.LoadBlendShapesFromJson(_attireData.eyelashesBlendShapesJson);
                AssignEyeLashesBlendShape(eyesBlendIndex, _eyelashesBlend[_eyelashesBlend.Keys.ToList()[eyesBlendIndex]]);
            }

        }
        
        /// <summary>
        /// Save Attire Data
        /// </summary>
        private void SaveData()
        {
            if (dataManager == null) return;
            var _player = dataManager.AvatarData;

            _player.attireData.bodytype = AttireModel.SetAttire(m_bodyTypeList[(int) bodyTypeIndex].name, (int) bodyTypeIndex);
            _player.attireData.hair = AttireModel.SetAttire(hairAttires[hairIndex].name, hairIndex);
            _player.attireData.skin = AttireModel.SetAttire(skins[skinIndex].name, skinIndex);
            _player.attireData.upperbody = AttireModel.SetAttire(uppperbodyAttires[clothesIndex].name, clothesIndex);
            _player.attireData.lowerbody = AttireModel.SetAttire(lowerBodyAttires[pantsIndex].name, pantsIndex);
            _player.attireData.footware = AttireModel.SetAttire(footwareAttires[footwareIndex].name, footwareIndex);
            
            _player.attireData.eyes_blendShapes = AttireModel.SetAttire("eyes_blend", eyesBlendIndex);
            _player.attireData.nose_blendShapes = AttireModel.SetAttire("nose_blend", noseBlendIndex);
            _player.attireData.lips_blendShapes = AttireModel.SetAttire("lips_blend", lipsBlendIndex);

            _player.attireData.headBlendShapesJson = AttireModel.BlendShapesDataToJson(headBlendShapeToIndex);
            _player.attireData.eyelashesBlendShapesJson = AttireModel.BlendShapesDataToJson(eyelashesBlendShapeToIndex);
            dataManager.AvatarData = _player;
            dataManager.SaveAvatarData();

            Debug.Log("<color=green>SAVE AVATAR DATA</color>");
        }

        #endregion


        private void AnimationTrigger(int hash)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Idle"))
            {
                animator.SetTrigger(hash);
            }
        }

        private void AttireIndexSetter()
        {
            attireIndexDatas.bodytype = (int) bodyTypeIndex;
            attireIndexDatas.headgear = headgearIndex;
            attireIndexDatas.skin = skinIndex;
            attireIndexDatas.hair = hairIndex;
            attireIndexDatas.clothes = clothesIndex;
            attireIndexDatas.pants = pantsIndex;
            attireIndexDatas.footware = footwareIndex;
            attireIndexDatas.accessory = accessoryIndex;

            attireIndexDatas.blendShapes.headBlendShapes.totalEyes = eyesBlendIndex;
            attireIndexDatas.blendShapes.headBlendShapes.totalNose = noseBlendIndex;
            attireIndexDatas.blendShapes.headBlendShapes.totalLips = lipsBlendIndex;
            
            attireIndexDatas.blendShapes.headBlendShapes.eyes_0 = headBlendShapeToIndex["EYE_0"];
            attireIndexDatas.blendShapes.headBlendShapes.eyes_1 = headBlendShapeToIndex["EYE_1"];
            attireIndexDatas.blendShapes.headBlendShapes.eyes_2 = headBlendShapeToIndex["EYE_2"];
            attireIndexDatas.blendShapes.headBlendShapes.eyes_3 = headBlendShapeToIndex["EYE_3"];
            attireIndexDatas.blendShapes.headBlendShapes.nose_0 = headBlendShapeToIndex["NOSE_0"];
            attireIndexDatas.blendShapes.headBlendShapes.nose_1 = headBlendShapeToIndex["NOSE_1"];
            attireIndexDatas.blendShapes.headBlendShapes.nose_2 = headBlendShapeToIndex["NOSE_2"];
            attireIndexDatas.blendShapes.headBlendShapes.nose_3 = headBlendShapeToIndex["NOSE_3"];
            attireIndexDatas.blendShapes.headBlendShapes.lips_0 = headBlendShapeToIndex["LIPS_0"];
            attireIndexDatas.blendShapes.headBlendShapes.lips_1 = headBlendShapeToIndex["LIPS_1"];
            attireIndexDatas.blendShapes.headBlendShapes.lips_2 = headBlendShapeToIndex["LIPS_1"];
            attireIndexDatas.blendShapes.headBlendShapes.lips_3 = headBlendShapeToIndex["LIPS_3"];
            
            AttireController.OnAttireIndexCount?.Invoke(attireIndexDatas);
        }
        
        private void AttireSetter(int index, GameObject[] _objects)
        {
            foreach (var item in _objects)
            {
                item.SetActive(false);
            }
            _objects[index].SetActive(true);
            AttireIndexSetter();
        }

        private void BlendShapeSetter(SkinnedMeshRenderer _skm, int _type, float _value)
        {
            _skm.SetBlendShapeWeight(_type, _value);
            AttireIndexSetter();
        }

        private void OnEnable()
        {
            AttireController.OnSkinNext += AssignNextSkin;
            AttireController.OnHeadgearNext += AssignNextHeadgear;
            AttireController.OnHairNext += AssignNextHair;
            AttireController.OnBodyTypeNext += AssignNextBodyType;
            AttireController.OnUpperbodyNext += AssignNextUpperbodyAttires;
            AttireController.OnLowerbodyNext += AssignNextLowerbodyAttires;
            AttireController.OnFootwareNext += AssignNextAssignFootware;
            AttireController.OnAccessoriesNext += AssignNextAccessories;
            
            AttireController.OnSkinPrev += AssignPrevSkin;
            AttireController.OnHeadgearPrev += AssignPrevHeadgear;
            AttireController.OnHairPrev += AssignPrevHair;
            AttireController.OnBodyTypePrev += AssignPrevBodyType;
            AttireController.OnUpperbodyPrev += AssignPrevUpperbodyAttires;
            AttireController.OnLowerbodyPrev += AssignPrevLowerbodyAttires;
            AttireController.OnFootwarePrev += AssignPrevAssignFootware;
            AttireController.OnAccessoriesPrev += AssignPrevAccessories;
            
            AttireController.OnNextHeadBlendShapeChange += AssignNextHeadBlendShapes;
            AttireController.OnPrevHeadBlendShapeChange += AssignPrevHeadBlendShapes;


            AttireController.OnAttireSetupCount += AttireIndexSetter;
        }
        
        private void OnDisable()
        {
            AttireController.OnSkinNext -= AssignNextSkin;
            AttireController.OnHeadgearNext -= AssignNextHeadgear;
            AttireController.OnHairNext -= AssignNextHair;
            AttireController.OnBodyTypeNext -= AssignNextBodyType;
            AttireController.OnUpperbodyNext -= AssignNextUpperbodyAttires;
            AttireController.OnLowerbodyNext -= AssignNextLowerbodyAttires;
            AttireController.OnFootwareNext -= AssignNextAssignFootware;
            AttireController.OnAccessoriesNext -= AssignNextAccessories; 
            
            AttireController.OnSkinPrev -= AssignPrevSkin;
            AttireController.OnHeadgearPrev -= AssignPrevHeadgear;
            AttireController.OnHairPrev -= AssignPrevHair;
            AttireController.OnBodyTypePrev -= AssignPrevBodyType;
            AttireController.OnUpperbodyPrev -= AssignPrevUpperbodyAttires;
            AttireController.OnLowerbodyPrev -= AssignPrevLowerbodyAttires;
            AttireController.OnFootwarePrev -= AssignPrevAssignFootware;
            AttireController.OnAccessoriesPrev -= AssignPrevAccessories;
            
            AttireController.OnNextHeadBlendShapeChange -= AssignNextHeadBlendShapes;
            AttireController.OnPrevHeadBlendShapeChange -= AssignPrevHeadBlendShapes;
            
            
            AttireController.OnAttireSetupCount -= AttireIndexSetter;
        }

    }
}

