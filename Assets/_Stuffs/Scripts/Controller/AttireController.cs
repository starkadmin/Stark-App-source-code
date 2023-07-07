using Stark.Model;
using UnityEngine;

namespace Stark.Controller
{
    public class AttireController : MonoBehaviour
    {
        public delegate void ChangeAttireEvent();
        public delegate void ChangeBlendShapeEvent(string type);

        public delegate void AttireCountEvent(AttireModel.AttireCountData attire);

        public delegate void ChangeAttireColorEvent(Color color);

        public static ChangeAttireEvent OnSkinNext;
        public static ChangeAttireEvent OnHeadgearNext;
        public static ChangeAttireEvent OnHairNext;
        public static ChangeAttireEvent OnBodyTypeNext;
        public static ChangeAttireEvent OnUpperbodyNext;
        public static ChangeAttireEvent OnLowerbodyNext;
        public static ChangeAttireEvent OnFootwareNext;
        public static ChangeAttireEvent OnAccessoriesNext;

        public static ChangeAttireEvent OnSkinPrev;
        public static ChangeAttireEvent OnHeadgearPrev;
        public static ChangeAttireEvent OnHairPrev;
        public static ChangeAttireEvent OnBodyTypePrev;
        public static ChangeAttireEvent OnUpperbodyPrev;
        public static ChangeAttireEvent OnLowerbodyPrev;
        public static ChangeAttireEvent OnFootwarePrev;
        public static ChangeAttireEvent OnAccessoriesPrev;

        public static ChangeAttireEvent OnAttireSetupCount;
        public static AttireCountEvent OnAttireIndexCount;

        // public static ChangeAttireColorEvent OnHairColorChange;

        public static ChangeBlendShapeEvent OnNextHeadBlendShapeChange;
        public static ChangeBlendShapeEvent OnPrevHeadBlendShapeChange;

        // [SerializeField] private Slider m_redHairSlider;
        // [SerializeField] private Slider m_blueHairSlider;
        // [SerializeField] private Slider m_greenHairSlider;


        // public void ChangeHairColor()
        // {
        //     var _color = new Color(
        //         m_redHairSlider.value / 255,
        //         m_greenHairSlider.value / 255,
        //         m_blueHairSlider.value / 255);
        //
        //     OnHairColorChange?.Invoke(_color);
        // }

        public void ChangeSkin(bool _isNext)
        {
            var _callback = _isNext ? OnSkinNext : OnSkinPrev;
            _callback?.Invoke();
        }
        public void ChangeHeadgear(bool _isNext)
        {
            var _callback = _isNext ? OnHeadgearNext : OnHeadgearPrev;
            _callback?.Invoke();
        }

        public void ChangeHair(bool _isNext)
        {
            var _callback = _isNext ? OnHairNext : OnHairPrev;
            _callback?.Invoke();
        }
        
        public void ChangeBodyType(bool _isNext)
        {
            var _callback = _isNext ? OnBodyTypeNext : OnBodyTypePrev;
            _callback?.Invoke();
        }

        public void ChangeUpperbody(bool _isNext)
        {
            var _callback = _isNext ? OnUpperbodyNext : OnUpperbodyPrev;
            _callback?.Invoke();
        }

        public void ChangeLowerbody(bool _isNext)
        {
            var _callback = _isNext ? OnLowerbodyNext : OnLowerbodyPrev;
            _callback?.Invoke();
        }

        public void ChangeFootwork(bool _isNext)
        {
            var _callback = _isNext ? OnFootwareNext : OnFootwarePrev;
            _callback?.Invoke();
        }

        public void ChangeAccessories(bool _isNext)
        {
            var _callback = _isNext ? OnAccessoriesNext : OnAccessoriesPrev;
            _callback?.Invoke();
        }


        #region Head BlendShape

        public void ChangeEyeBlend(bool _isNext)
        {
            var _callback0 = _isNext ? OnNextHeadBlendShapeChange : OnPrevHeadBlendShapeChange;
            _callback0?.Invoke("EYE");
        }
        public void ChangeNoseBlend(bool _isNext)
        {
            var _callback0 = _isNext ? OnNextHeadBlendShapeChange : OnPrevHeadBlendShapeChange;
            _callback0?.Invoke("NOSE");
        }
        public void ChangeLipsBlend(bool _isNext)
        {
            var _callback0 = _isNext ? OnNextHeadBlendShapeChange : OnPrevHeadBlendShapeChange;
            _callback0?.Invoke("LIPS");
        }

        #endregion



    }

}
