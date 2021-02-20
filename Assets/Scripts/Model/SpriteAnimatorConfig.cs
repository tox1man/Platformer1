using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    [CreateAssetMenu(fileName = "SpriteAnimatorConfig", menuName = "Config/Animator", order = 0)]
    public class SpriteAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimTrack Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();

    }
}
