using System;
using UnityEngine;

namespace Mario
{
    public class LevelObjectView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public Collider2D Collider2D;
        public Rigidbody2D Rigidbody2D;
        public Transform Transform;
        public float Speed;
        public float JumpForce;
        public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            var levelObject = collider.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }

    }
}