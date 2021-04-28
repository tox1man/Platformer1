using System;
using UnityEngine;
using System.Collections.Generic;

namespace Mario
{
    public class Gun
    {
        /*
        private bool _sleeps = false;
        private int _maxDepth = 5;
        private int _currentHitIndex = 0;
        private float _timeStep = 0;
        private LevelObjectView _playerView;
        private RaycastHit2D[] _hits;
        private Ray[] _rays; 
        private int _maxBullets = 5;
        */

        private float _timeTillShot;
        private float _shootInterval;
        private int _bulletIndex;

        private GunView _gunView;
        private Transform _gunTransform;

        private List<Bullet> _bullets;

        public Gun(GunView gunView, BulletView bulletView, LevelObjectView playerView, float shootInterval = 1)
        {
            /*
            _playerView = playerView;
            _hits = new RaycastHit2D[_maxDepth];
            _rays = new Ray[_hits.Length];
            */

            _gunView = gunView;
            _shootInterval = shootInterval;
            _bullets = new List<Bullet>();
            _gunTransform = _gunView.gameObject.transform;

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
        /*
                private bool ScanForTarget()
                {
                    float angle;
                    Vector2 direction;

                    for (angle = 90; angle < 270; angle += 0.1f)
                    {
                        direction.x = Mathf.Cos(Mathf.Deg2Rad * angle);
                        direction.y = Mathf.Sin(Mathf.Deg2Rad * angle);

                        if (TakeAim(direction))
                            {
                                return true;
                            }
                    }
                    return false;
                }

                private bool TakeAim(Vector2 direction)
                {
                    _hits = new RaycastHit2D[_maxDepth];
                    Vector2 origin = _gunTransform.position;
                    RaycastHit2D lastHit;

                    if (_hits[0] = Physics2D.Raycast(origin, direction))
                    {
                        lastHit = _hits[0];

                        for (int i = 1; i < _maxDepth; i++)
                        {
                            if (lastHit.transform == _playerView.transform)
                            {
                                //Debug.DrawLine(origin, _hits[0].point, Color.blue);

                                for (int j = 0; j <= _hits.Length - 2; j++)
                                {
                                    if (_hits[j + 1].collider != null)
                                    {
                                        //Debug.DrawLine(_hits[j].point, _hits[j + 1].point, Color.blue);
                                    }
                                }
                                return true;
                            }
                            direction = ReflectRay(direction, lastHit.normal);
                            _hits[i] = Physics2D.Raycast(lastHit.point, direction);

                            if(_hits[i].collider == null || i == _maxDepth - 1)
                            {
                                return false;
                            }
                            lastHit = _hits[i];
                        }
                    }
                    return false;
                }

                private Vector2 ReflectRay(Vector2 direction, Vector2 normal)
                {
                    return Vector2.Reflect(direction, normal);
                }

                private GameObject SpawnBullet()
                {
                    foreach (var bullet in _bullets)
                    {
                        if (!bullet.activeSelf)
                        {
                            bullet.transform.position = _gun.position;
                            bullet.SetActive(true);

                            return bullet;
                        }
                    }
                    return null;
                }

                private void ShootBullet(GameObject bullet, float speed)
                {
                    Vector2 direction;
                    Vector2 startPosition;
                    Vector2 targetPosition;
                    _timeStep += Time.deltaTime;

                    if (HasBulletHitPlayer(bullet))
                    {
                        Debug.Log("PLAYER HIT");
                        bullet.SetActive(false);
                        _sleeps = false;
                        ResetHitData();
                        return;
                    }

                    if (_currentHitIndex == 0)
                    { 
                        startPosition = _gunTransform.position;
                        targetPosition = _hits[0].point;
                    }
                    else 
                    { 
                        startPosition = _hits[_currentHitIndex - 1].point;
                        targetPosition = _hits[_currentHitIndex].point;
                    }

                    direction = targetPosition - startPosition;
                    float angle = Mathf.Acos(direction.normalized.y) * Mathf.Rad2Deg;

                    if (direction.x > 0)
                    {
                        angle = 360 - angle;
                    }

                    if (Vector2.Distance(bullet.transform.position, _hits[_currentHitIndex].point) <= 0.01f)
                    {
                        if (_currentHitIndex < _hits.Length - 1 && _hits[_currentHitIndex + 1].collider != null)
                        {
                            _currentHitIndex++;
                            _timeStep = 0;
                            return;
                        }
                        else
                        {
                            Debug.Log("MISS");
                            bullet.SetActive(false);
                            _sleeps = false;
                            ResetHitData();
                            return;
                        }
                    }
                        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                        bullet.transform.position = Vector2.Lerp(startPosition, targetPosition, speed * _timeStep);
                }

                private bool HasBulletHitPlayer(GameObject bullet)
                {
                    return bullet.GetComponent<CapsuleCollider2D>().IsTouching(_playerView.Collider2D);
                }

                private void ResetHitData()
                {
                    _hits = new RaycastHit2D[_maxDepth];
                    _currentHitIndex = 0;
                }
        */

        public void Update()
        {
            if (_timeTillShot > 0)
            {
                _bullets[_bulletIndex].Active(false);
                _timeTillShot -= Time.deltaTime;
            }
            else
            {
                _bullets[_bulletIndex].Shoot(_gunTransform.position, Vector2.left * 10);
                _bulletIndex++;
                _timeTillShot = _shootInterval;

                if (_bulletIndex >= _bullets.Count)
                {
                    _bulletIndex = 0;
                }

                _bullets.ForEach(bullet => bullet.Update());
            }
        }
    }
}
