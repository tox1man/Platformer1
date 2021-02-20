using UnityEngine;

namespace Mario
{
    class LevelObjectView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public Collider2D Collider2D;
        public Rigidbody2D Rigidbody2D;
        public Transform Transform;
        public float Speed;
        public float JumpForce;
        //add other useful refs
    }
}