using System;
using UnityEngine;

namespace Mario
{
    public class SimplePatrolAI
    {
        #region Fields

        private readonly LevelObjectView _view;
        private readonly SimplePatrolModel _model;

        #endregion


        #region Class life cycles

        public SimplePatrolAI(LevelObjectView view, SimplePatrolModel model)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        }

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * Time.fixedDeltaTime;
            _view.Rigidbody2D.velocity = newVelocity;
        }

        #endregion

    }
}
