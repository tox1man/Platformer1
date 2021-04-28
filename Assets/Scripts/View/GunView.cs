using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    public class GunView : LevelObjectView
    {
        [SerializeField]
        public List<BulletView> _bulletsView;
    }
}
