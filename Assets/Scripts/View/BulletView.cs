using UnityEngine;

namespace Mario
{
    public class BulletView : LevelObjectView
    {
        [SerializeField]
        public TrailRenderer _trail;
        public Collider2D _collider2D;

        public void SetVisible(bool visible)
        {
            //if (_trail) _trail.enabled = visible;
            //if (_trail) _trail.Clear();
            SpriteRenderer.enabled = visible;
        }
    }
}