using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    public class Coin : IDisposable
    {
        private const float _animationSpeed = 5;

        private LevelObjectView _playerView;
        private SpriteAnimator _coinSpriteAnimator;
        private List<LevelObjectView> _coinsView;

        public Coin(LevelObjectView playerView, List<LevelObjectView> coinsView, SpriteAnimator coinSpriteAnimator)
        {
            _playerView = playerView;
            _coinSpriteAnimator = coinSpriteAnimator;
            _coinsView = coinsView;

            playerView.OnLevelObjectContact += OnLevelObjectContact;
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_coinsView.Contains(contactView))
            {
                _coinSpriteAnimator.StopAnimation(contactView.SpriteRenderer);
                GameObject.Destroy(contactView.gameObject);
            }
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}
