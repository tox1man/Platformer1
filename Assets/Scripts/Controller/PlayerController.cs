using System;
using UnityEngine;

namespace Mario
{
    public class PlayerController
    {
        private readonly string _verticalAxis = "Vertical";
        private readonly string _horizontalAxis = "Horizontal";

        private float _jumpThresh = 0.1f;
        private float _moveThresh = 0.1f;

        private bool _isWalking = false;
        private bool _doJump = false;
        private float _goSideways = 0.0f;

        private readonly LevelObjectView _view;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly ContactsPuller _contactsPuller;

        public PlayerController(LevelObjectView view, SpriteAnimator spriteAnimator, ContactsPuller contactsPuller)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
            _contactsPuller = contactsPuller;
        }

        public void FixedUpdate()
        {
            //Input and physics controls

            _doJump = Input.GetAxisRaw(_verticalAxis) > 0;
            _goSideways = Input.GetAxisRaw(_horizontalAxis);
            _isWalking = Math.Abs(_goSideways) > _moveThresh;

            float newVelocity = 0f;

            if (_isWalking && 
                (_goSideways > 0 || !_contactsPuller._hasLeftContact) &&
                (_goSideways < 0 || !_contactsPuller._hasRightContact))
            {
                newVelocity = (_goSideways < 0 ? -1 : 1) * Time.fixedDeltaTime * _view.Speed;
            }

            _view.Rigidbody2D.velocity = _view.Rigidbody2D.velocity.Change(x: newVelocity);

            if (_doJump && _contactsPuller._hasBottomContact && Math.Abs(_view.Rigidbody2D.velocity.y) < _jumpThresh)
            {
                _view.Rigidbody2D.AddForce(Vector2.up * _view.JumpForce, ForceMode2D.Impulse);
            }

            //Animation control

            AnimTrack animationTrack;

            if (_goSideways < 0)
            {
                _view.SpriteRenderer.flipX = true;
            }
            else if (_goSideways > 0)
            {
                _view.SpriteRenderer.flipX = false;
            }

            if (_contactsPuller._hasBottomContact)
            {
                animationTrack = _goSideways == 0 ? AnimTrack.Idle : AnimTrack.Run;
            }
            else
            {
                animationTrack = AnimTrack.Jump;
            }
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, animationTrack, true);
        }

    }
}
