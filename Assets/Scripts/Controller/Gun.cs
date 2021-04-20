using System;
using UnityEngine;

namespace Mario
{
    public class Gun
    {
        private bool _sleeps = false;
        private int _maxDepth = 5;
        private int _maxBullets = 1;
        private int _shootInterval = 1;
        private int _currentHitIndex = 0;
        private float _timeStep = 0;

        private Transform _player;
        private Transform _gun;

        private Collider2D _bulletCollider;
        private Collider2D _playerCollider;

        private GameObject[] _bullets;
        private RaycastHit2D[] _hits;

        private Ray[] _rays;


        public Gun(Transform player, Transform gun, int shootInterval)
        {
            _player = player;
            _gun = gun;
            _shootInterval = shootInterval;
            _hits = new RaycastHit2D[_maxDepth];
            _bullets = new GameObject[_maxBullets];
            _rays = new Ray[_hits.Length];
            _playerCollider = _player.GetComponent<BoxCollider2D>();

            for (int i = 0; i < _bullets.Length; i++)
            {
                _bullets[i] = GameObject.Instantiate(Resources.Load<GameObject>("Bullet"));
                _bullets[i].transform.SetParent(GameObject.Find("Bullets").transform);
                _bullets[i].SetActive(false);

            }
        }

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
            Vector2 origin = _gun.position;
            RaycastHit2D lastHit;

            if (_hits[0] = Physics2D.Raycast(origin, direction))
            {
                lastHit = _hits[0];
            
                for (int i = 1; i < _maxDepth; i++)
                {
                    if (lastHit.transform == _player)
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

        private GameObject SpawnBullet ()
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
                startPosition = _gun.position;
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
            return bullet.GetComponent<CapsuleCollider2D>().IsTouching(_playerCollider);
        }

        private void ResetHitData()
        {
            _hits = new RaycastHit2D[_maxDepth];
            _currentHitIndex = 0;
        }

        private void DrawAimLines()
        {
        }

        public void Update()
        {
            DrawAimLines();
           
            if (!_sleeps)
            {
                ScanForTarget();
            }
            foreach (var bullet in _bullets)
            {
                if (bullet.activeSelf)
                {
                    ShootBullet(bullet, 1f);
                    _sleeps = true;
                }
            }
        }
    }
}
