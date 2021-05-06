using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Mario
{
    public class UIController
    {
        private UIView _UIView;
        private SpriteAnimator _UIAnimator;
        private UIAnimatorConfig _UISpriteConfig;
        private List<Sprite> spriteListFromConfig;
        private readonly int[] _scoreTable;
        public int _currentScoreIndex { get; private set; }
        private Image _figures;
        private Image _hundreds;

        private Sprite[] ScoreSprites;
        public UIController(UIView UIview, SpriteAnimator UIAnimator, UIAnimatorConfig UIConfig)
        {

            // 100 - 200 - 400 - 500 - 800 - 1000 - 2000 - 4000 - 5000 - 8000 - 1UP - 1UP - 1UP
            _scoreTable = new int[11]
            {
                0,          //0
                100,        //1
                200,        //2
                400,        //3
                500,        //4
                800,        //5
                1000,       //6
                2000,       //7
                4000,       //8
                5000,       //9
                8000        //10
            };

            _UIView = UIview;
            _UIAnimator = UIAnimator;
            _UISpriteConfig = UIConfig;

            _figures = _UIView.transform.Find("Score Display").GetComponent<Image>();
            _hundreds = _UIView.transform.Find("KiloScore Display").GetComponent<Image>();

            spriteListFromConfig = _UISpriteConfig.Sequences[0].Sprites;

            //    spriteListFromConfig[0]  // 00
            //    spriteListFromConfig[1], //  0
            //    spriteListFromConfig[2], // 10
            //    spriteListFromConfig[3], // 20
            //    spriteListFromConfig[4], // 40
            //    spriteListFromConfig[5], // 50
            //    spriteListFromConfig[6], // 80

            SetStartingScore();
        }

        private void SetStartingScore()
        {
            _currentScoreIndex = 0;
            DrawScore();
        }

        public void IncreaseScore()
        {
            if(_currentScoreIndex < _scoreTable.Length) { _currentScoreIndex++; }
            DrawScore();
        }

        private void DrawScore()
        {
            if (_scoreTable[_currentScoreIndex] == 0)    
            {
                _hundreds.sprite = spriteListFromConfig[0];                      
                _figures.sprite = spriteListFromConfig[0]; 
            }
            if (_scoreTable[_currentScoreIndex] >= 100 && _scoreTable[_currentScoreIndex] < 1000)  
            { 
                _hundreds.sprite = spriteListFromConfig[_currentScoreIndex + 1]; 
                _figures.sprite = spriteListFromConfig[1];
            }
            if (_scoreTable[_currentScoreIndex] >= 1000) 
            { 
                _hundreds.sprite = spriteListFromConfig[_currentScoreIndex - 4]; 
                _figures.sprite = spriteListFromConfig[0]; 
            }
        }
    }
}
