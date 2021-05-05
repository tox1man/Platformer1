using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private SpriteRenderer _back;
        [SerializeField]
        private LevelObjectView _playerView;
        [SerializeField]
        private GunView _gunView;
        [SerializeField]
        private LevelObjectView _gumboView;
        [SerializeField]
        private BulletView _bulletView;
        [SerializeField]
        private LevelObjectView _flagView;
        [SerializeField]
        private GameObject _gumbos;
        [SerializeField]
        private GameObject _boxes;
        [SerializeField]
        private GameObject _guns; 
        [SerializeField]
        private GameObject _coins;

        private SpriteAnimatorConfig _playerAnimatorConfig;
        private SpriteAnimatorConfig _gumboAnimatorConfig;
        private SpriteAnimatorConfig _boxAnimatorConfig;
        private SpriteAnimatorConfig _coinAnimatorConfig;

        private SpriteAnimator _playerAnimator;
        private SpriteAnimator _gumboAnimator;
        private SpriteAnimator _boxAnimator;
        private SpriteAnimator _coinAnimator;

        private PlayerController _playerController;
        private Gun _gunController;
        private Coin _coinController;
        private Flag _flagController;

        private ContactsPuller _playerContactsPuller;
        private List<LevelObjectView> _coinsView;

        private void Awake()
        {
            _playerAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            _gumboAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("GumboAnimatorConfig");
            _boxAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("BoxAnimatorConfig");
            _coinAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("CoinAnimatorConfig");

            _playerAnimator = new SpriteAnimator(_playerAnimatorConfig);
            _gumboAnimator = new SpriteAnimator(_gumboAnimatorConfig);
            _boxAnimator = new SpriteAnimator(_boxAnimatorConfig);
            _coinAnimator = new SpriteAnimator(_coinAnimatorConfig);

            _playerContactsPuller = new ContactsPuller(_playerView.Collider2D);
            _playerController = new PlayerController(_playerView, _playerAnimator, _playerContactsPuller);
            _gunController = new Gun(_gunView, _bulletView, _playerView);
            _flagController = new Flag(_playerView, _flagView);

            foreach (Transform gumbo in _gumbos.GetComponentInChildren<Transform>())
            {
                _gumboAnimator.StartAnimation(gumbo.gameObject.GetComponent<SpriteRenderer>(), AnimTrack.Run, true, 5);
            }

            foreach (Transform box in _boxes.GetComponentInChildren<Transform>())
            {
                _boxAnimator.StartAnimation(box.gameObject.GetComponent<SpriteRenderer>(), AnimTrack.Idle, true);
            }

            _coinsView = new List<LevelObjectView>(_coins.transform.childCount);
            foreach (Transform coin in _coins.GetComponentInChildren<Transform>())
            {
                _coinsView.Add(coin.gameObject.GetComponent<LevelObjectView>());
                _coinAnimator.StartAnimation(coin.gameObject.GetComponent<SpriteRenderer>(), AnimTrack.Idle, true, 5);
            }
            _coinController = new Coin(_playerView, _coinsView, _coinAnimator);

            
            
            //gun.Awake();

            //ANIMATION FOR GUN

            //SomeConfig config = Resources.Load("SomeConfig", typeof(SomeConfig))as   SomeConfig;
            //load some configs here <3>

            //_someManager = new SomeManager(config);
            //create some logic managers here for tests <4>

        }

        private void Update()
        {
            _gunController.Update();

            _playerAnimator.Update();
            _gumboAnimator.Update();
            _boxAnimator.Update();
            _coinAnimator.Update();

            _playerContactsPuller.Update();

            //_someManager.Update();
            //update logic managers here <5>
        }

        private void FixedUpdate()
        {
            _playerController.FixedUpdate();
        }

        private void OnDestroy()
        {
            //_someManager.Dispose();
            //dispose logic managers here <7>
        }
    }
}
