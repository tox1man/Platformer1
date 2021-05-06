using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    [CreateAssetMenu(fileName = "UISpriteAnimatorConfig", menuName = "Config/UI Sprite Animator")]
    public class UIAnimatorConfig : SpriteAnimatorConfig
    {
        [Serializable]
        new public sealed class SpriteSequence
        {
            public List<Sprite> Sprites = new List<Sprite>();
        }
    }
}
