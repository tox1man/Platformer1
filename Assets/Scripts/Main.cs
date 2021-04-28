using System;
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
        private GameObject _gumbos;
        [SerializeField]
        private GameObject _boxes;
        [SerializeField]
        private GameObject _guns;

        private SpriteAnimatorConfig _playerAnimatorConfig;
        private SpriteAnimatorConfig _gumboAnimatorConfig;
        private SpriteAnimatorConfig _boxAnimatorConfig;
        private SpriteAnimatorConfig _gunAnimatorConfig;

        private SpriteAnimator _playerAnimator;
        private SpriteAnimator _gumboAnimator;
        private SpriteAnimator _boxAnimator;
        private SpriteAnimator _gunAnimator;

        private Transform _playerTransform;
        private Vector2 gumboDir = Vector2.left;

        private PlayerController _playerController;
        private Gun _gunController;

        private ContactsPuller _contactsPuller;


        //[SerializeField]
        //private SomeView _someView;
        //add links to test views <1>

        //private SomeManager _someManager;
        //add links to some logic managers <2>

        private void Awake()
        {
            _playerTransform = _playerView.GetComponent<Transform>();

            _playerAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            _gumboAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("GumboAnimatorConfig");
            _boxAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("BoxAnimatorConfig");
            _gunAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("GunAnimatorConfig");

            _playerAnimator = new SpriteAnimator(_playerAnimatorConfig);
            _gumboAnimator = new SpriteAnimator(_gumboAnimatorConfig);
            _boxAnimator = new SpriteAnimator(_boxAnimatorConfig);
            _gunAnimator = new SpriteAnimator(_gunAnimatorConfig);
            _contactsPuller = new ContactsPuller(_playerView.Collider2D);

            _playerController = new PlayerController(_playerView, _playerAnimator, _contactsPuller);

            _gunController = new Gun(_gunView, _bulletView, _playerView);

            _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimTrack.Run, true);

            foreach (Transform gumbo in _gumbos.GetComponentInChildren<Transform>())
            {
                _gumboAnimator.StartAnimation(gumbo.gameObject.GetComponent<SpriteRenderer>(), AnimTrack.Run, true, 5);
            }

            foreach (Transform box in _boxes.GetComponentInChildren<Transform>())
            {
                _boxAnimator.StartAnimation(box.gameObject.GetComponent<SpriteRenderer>(), AnimTrack.Idle, true);
            }

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

            _contactsPuller.Update();

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

        //private void OnDrawGizmos()
        //{
        //    gun.Update();
        //}
    }
}
