using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    public class SpriteAnimator : IDisposable
    {
        private class Animation
        {
            public AnimTrack AnimTrack;
            public List<Sprite> Sprites;
            public float Speed = 10;
            public float Counter = 0;
            public bool Sleeps;
            public bool Loop = true;

            public void UpdateFrame()
            {
                if (Sleeps) return;
                Counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    if (Counter >= Sprites.Count)
                    {
                        Counter = 0;
                    }
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count - 1;
                    Sleeps = true;
                }
            }
        }

        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimator(SpriteAnimatorConfig animatorConfig)
        {
            _config = animatorConfig;
        }

        public void StartAnimation(SpriteRenderer renderer, AnimTrack animTrack, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(renderer, out Animation animation))
            {
                animation.Speed = speed;
                animation.Loop = loop;
                animation.Sleeps = false;

                if(animation.AnimTrack != animTrack)
                {
                    animation.AnimTrack = animTrack;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == animTrack).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimations.Add(renderer, new Animation
                {
                    AnimTrack = animTrack,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == animTrack).Sprites,
                    Speed = speed,
                    Loop = loop
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite)) {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.UpdateFrame();
                animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}
