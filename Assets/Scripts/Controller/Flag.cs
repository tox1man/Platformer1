using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    public class Flag : IDisposable
    {
        private LevelObjectView _playerView;
        private LevelObjectView _flagView;

        public Flag(LevelObjectView playerView, LevelObjectView flagView)
        {
            _playerView = playerView;
            _flagView = flagView;

            _playerView.OnLevelObjectContact += OnLevelObjectContact;
        }

        public void OnLevelObjectContact(LevelObjectView contactView)
        {
            if(contactView == _flagView)
            {

                Debug.Log("endgame");
            }
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}
