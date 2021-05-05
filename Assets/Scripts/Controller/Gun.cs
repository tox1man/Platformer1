using System;
using UnityEngine;
using System.Collections.Generic;

namespace Mario
{
    public class Gun
    {
        private int _maxDepth = 5;
        private LevelObjectView _playerView;
        private Dictionary<Vector2, Vector2> _rays;

        private float _timeTillShot;
        private Vector2 _shootDirection;
        private float _shootInterval;
        private int _bulletIndex;

        private GunView _gunView;
        private Transform _gunTransform;

        private List<Bullet> _bullets;

        public Gun(GunView gunView, BulletView bulletView, LevelObjectView playerView, float shootInterval = 5)
        {
            _playerView = playerView;
            _gunView = gunView;
            _shootInterval = shootInterval;
            _bullets = new List<Bullet>();
            _gunTransform = _gunView.transform;

            SpawnBullets();
        }

        private void SpawnBullets()
        {
            foreach(var bulletView in _gunView._bulletsView)
            {
                _bullets.Add(new Bullet(bulletView));
                bulletView.transform.SetParent(_gunTransform);
                bulletView.gameObject.SetActive(false);
            }
        }
        
        private bool ScanForTarget()
        {
            float angle;
            Vector2 direction;

            for (angle = 120; angle < 235; angle += 5f)
            {
                direction.x = Mathf.Cos(Mathf.Deg2Rad * angle);
                direction.y = Mathf.Sin(Mathf.Deg2Rad * angle);

                if (TakeAim(direction))
                    {
                        _shootDirection = direction;
                        return true;
                    }
            }
            return false;
        }

        private bool TakeAim(Vector2 direction)
        {
            RaycastHit2D hit;
            Vector2 newOrigin = _gunTransform.position;
            Vector2 newDirection = direction;

            _rays = new Dictionary<Vector2, Vector2>();

            for (int i = 0; i < _maxDepth; i++)
            {
                if (hit = Physics2D.Raycast(newOrigin, newDirection))
                {
                    if (hit.transform == _playerView.transform)
                    {
                        _rays.Add(newOrigin, hit.centroid);

                        //hit
                        return true;
                    }
                    _rays.Add(newOrigin, hit.centroid);

                    newDirection = ReflectRay(newDirection, hit.normal);
                    newOrigin = hit.point;
                }
            }
            //miss
            return false;
        }

        private Vector2 ReflectRay(Vector2 direction, Vector2 normal)
        {
            return Vector2.Reflect(direction, normal);
        }
        private void DrawAimLines()
        {
            foreach (KeyValuePair<Vector2, Vector2> kvp in _rays)
            {
                Debug.DrawLine(kvp.Key, kvp.Value);
            }
        }
        
        public void Update()
        {
            if (_timeTillShot > 0)
            {
                _bullets[_bulletIndex].Active(false);
                _timeTillShot -= Time.deltaTime;
            }
            else
            {
                if (ScanForTarget())
                {
                    _bullets[_bulletIndex].Shoot(_gunTransform.position, _shootDirection);
                    _bulletIndex++;
                    _timeTillShot = _shootInterval;

                    if (_bulletIndex >= _bullets.Count)
                    {
                        _bulletIndex = 0;
                    }
                }
            }
            _bullets.ForEach(bullet => bullet.Update());

            DrawAimLines();
        }

    }
}
