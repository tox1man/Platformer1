using System;
using UnityEngine;

namespace Mario

{
    public class LevelObjectTrigger : MonoBehaviour
    {
        #region Events

        public event EventHandler<GameObject> TriggerEnter;
        public event EventHandler<GameObject> TriggerExit;

        #endregion


        #region Unity methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(this, other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit?.Invoke(this, other.gameObject);
        }

        #endregion
    }
}

