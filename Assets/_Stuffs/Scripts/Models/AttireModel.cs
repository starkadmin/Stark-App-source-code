using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Stark.Controller;
using UnityEngine;

namespace Stark.Model
{
    [Serializable]
    public class AttireModel
    {
        [Serializable]
        public class AttireData
        {
            public AttireData() { }
            public AttireData(AttireClass _default)
            {
                bodytype = _default;
                headgear = _default;
                hair = _default;
                skin = _default;
                upperbody = _default;
                lowerbody = _default;
                footware = _default;
                accessory = _default;
                eyes_blendShapes = _default;
                nose_blendShapes = _default;
                lips_blendShapes = _default;
                hairColor = Color.black;
            }

            public AttireClass bodytype;
            public AttireClass hair;
            public AttireClass headgear;
            public AttireClass skin;
            public AttireClass upperbody;
            public AttireClass lowerbody;
            public AttireClass footware;
            public AttireClass accessory;

            public AttireClass eyes_blendShapes;
            public AttireClass nose_blendShapes;
            public AttireClass lips_blendShapes;
            
            public string headBlendShapesJson;
            public string eyelashesBlendShapesJson;
            public Color hairColor;
        }
        
        [Serializable]
        public class AttireClass
        {
            public AttireClass()
            {
                attireName = "default";
                attireIndex = 0;
            }
            public string attireName;
            public int attireIndex;
        }
        
        [Serializable]
        public class AttireCountData
        {
            public int bodytype;
            public int headgear;
            public int hair;
            public int skin;
            public int clothes;
            public int pants;
            public int footware;
            public int accessory;
            public BlendShapes blendShapes = new BlendShapes();
            
            [Serializable]
            public class BlendShapes
            {
                public Head headBlendShapes = new Head();
                
                [Serializable]
                public class Head
                {
                    public int totalEyes;
                    public int totalNose;
                    public int totalLips;

                    public int eyes_0;
                    public int eyes_1;
                    public int eyes_2;
                    public int eyes_3;
                    public int nose_0;
                    public int nose_1;
                    public int nose_2;
                    public int nose_3;
                    public int lips_0;
                    public int lips_1;
                    public int lips_2;
                    public int lips_3;
                }
            }
        }

        public static string BlendShapesDataToJson(Dictionary<string, int> blendShapes)
        {
            return JsonConvert.SerializeObject(blendShapes);
        }

        public static Dictionary<string, int> LoadBlendShapesFromJson(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, int>>(json);;
        }
        public static AttireClass SetAttire(string _name, int index)
        {
            return new AttireClass
            {
                attireName = _name, 
                attireIndex = index
            };
        }
    }
}
