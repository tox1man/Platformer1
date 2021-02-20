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
        private GameObject _gumbos;
        [SerializeField]
        private GameObject _boxes;

        private SpriteAnimatorConfig _playerAnimatorConfig;
        private SpriteAnimatorConfig _gumboAnimatorConfig;
        private SpriteAnimatorConfig _boxAnimatorConfig;

        private SpriteAnimator _playerAnimator;
        private SpriteAnimator _gumboAnimator;
        private SpriteAnimator _boxAnimator;

        private Transform _playerTransform;
        private Vector2 gumboDir = Vector2.left;

        //[SerializeField]
        //private SomeView _someView;
        //add links to test views <1>

        //private SomeManager _someManager;
        //add links to some logic managers <2>

        private void Awake()
        {
            _playerView.Speed = 5;
            _playerView.JumpForce = 25;
            _playerTransform = _playerView.GetComponent<Transform>();

            _playerAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimatorConfig");
            _gumboAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("GumboAnimatorConfig");
            _boxAnimatorConfig = Resources.Load<SpriteAnimatorConfig>("BoxAnimatorConfig");

            _playerAnimator = new SpriteAnimator(_playerAnimatorConfig);
            _gumboAnimator = new SpriteAnimator(_gumboAnimatorConfig);
            _boxAnimator = new SpriteAnimator(_boxAnimatorConfig);

            _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimTrack.Run, true, 7);

            foreach (Transform gumbo in _gumbos.GetComponentInChildren<Transform>())
            {
                _gumboAnimator.StartAnimation(gumbo.gameObject.GetComponent<SpriteRenderer>(), AnimTrack.Run, true, 5);
            }

            foreach (Transform box in _boxes.GetComponentInChildren<Transform>())
            {
                _boxAnimator.StartAnimation(box.gameObject.GetComponent<SpriteRenderer>(), AnimTrack.Idle, true, 7);
            }

            //SomeConfig config = Resources.Load("SomeConfig", typeof(SomeConfig))as   SomeConfig;
            //load some configs here <3>

            //_someManager = new SomeManager(config);
            //create some logic managers here for tests <4>

        }

        private void Update()
        {
            _playerAnimator.Update();
            _gumboAnimator.Update();
            _boxAnimator.Update();

            if(Input.GetKey(KeyCode.A)) 
            {
                _playerTransform.Translate(Vector2.left * _playerView.Speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D)) 
            {
                _playerTransform.Translate(Vector2.right * _playerView.Speed * Time.deltaTime);
            }

            if (_playerTransform.position.x <= -9)
            {
                _playerTransform.position = new Vector2(-9, _playerTransform.position.y);
            }
            if (_playerTransform.position.x >= 9)
            {
                _playerTransform.position = new Vector2(9, _playerTransform.position.y);
            }

            foreach (Transform gumbo in _gumbos.GetComponentInChildren<Transform>())
            {
                gumbo.Translate(gumboDir * _playerView.Speed * Time.deltaTime);
                if (gumbo.position.x <= -9)
                {
                    gumbo.position = new Vector2(-9, gumbo.position.y);
                    gumboDir *= -1;
                }
                if (gumbo.position.x >= 9)
                {
                    gumbo.position = new Vector2(9, gumbo.position.y);
                    gumboDir *= -1;
                }
            }

            //_someManager.Update();
            //update logic managers here <5>
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.W) && Math.Abs(_playerView.GetComponent<Rigidbody2D>().velocity.y) < 0.2f)
            {
                _playerView.GetComponent<Rigidbody2D>().velocity = Vector2.up * _playerView.JumpForce;
            }
            //_someManager.FixedUpdate();
            //update logic managers here <6>
        }

        private void OnDestroy()
        {
            //_someManager.Dispose();
            //dispose logic managers here <7>
        }

    }
}
