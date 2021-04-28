using System;
using UnityEngine;

namespace Mario
{
    public class Bullet
    {
        private Vector2 _velocity;
        private BulletView _bulletView;

        public Bullet(BulletView bulletView)
        {
            _bulletView = bulletView;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _bulletView.transform.rotation = Quaternion.AngleAxis(angle, axis);
        }

        public void Shoot(Vector2 position, Vector2 velocity)
        {
            _bulletView.SetVisible(false);
            _bulletView.gameObject.SetActive(true);

            _bulletView.transform.position = position;
            _bulletView.Rigidbody2D.AddForce(Vector2.zero);
            _bulletView.Rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
            _bulletView.Rigidbody2D.angularVelocity = 0;

            _bulletView.SetVisible(true);
        }
        public void Active(bool enabled)
        {
            _bulletView._trail.enabled = enabled;
            _bulletView.gameObject.SetActive(enabled);
        }

        public void Update()
        {
        }
    }
}
