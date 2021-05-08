using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Mario
{
    public class StalkerAIModel
    {
        #region Fields

        private readonly AIConfig _config;
        private Path _path;
        private int _currentPointIndex;

        #endregion


        #region Class life cycles

        public StalkerAIModel(AIConfig config)
        {
            _config = config;
        }

        #endregion


        #region Methods

        public void UpdatePath(Path p)
        {
            _path = p;
            _currentPointIndex = 0;
        }

        public Vector2 CalculateVelocity(Vector2 fromPosition)
        {
            if (_path == null) return Vector2.zero;
            if (_currentPointIndex >= _path.vectorPath.Count) return Vector2.zero;

            var direction = ((Vector2)_path.vectorPath[_currentPointIndex] - fromPosition).normalized;
            var result = _config.speed * direction;
            var sqrDistance = Vector2.SqrMagnitude((Vector2)_path.vectorPath[_currentPointIndex] - fromPosition);
            if (sqrDistance <= _config.minSqrDistanceToTarget)
            {
                _currentPointIndex++;
            }
            return result;
        }

        #endregion
    }

}
