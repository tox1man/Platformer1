using System;
using System.Collections.Generic;
using UnityEngine;


namespace Mario
{
    public class ProtectedZone
    {
        #region Fields

        private readonly List<IProtector> _protectors;
        private readonly LevelObjectTrigger _view;

        #endregion

        #region Class life cycles

        public ProtectedZone(LevelObjectTrigger view, List<IProtector> protectors)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _protectors = protectors != null ? protectors : throw new ArgumentNullException(nameof(protectors)); ;
        }

        public void Init()
        {
            _view.TriggerEnter += OnContact;
            _view.TriggerExit += OnExit;
        }

        public void Deinit()
        {
            _view.TriggerEnter -= OnContact;
            _view.TriggerExit -= OnExit;
        }

        #endregion

        #region Methods

        private void OnContact(object sender, GameObject gameObject)
        {
            foreach (var protector in _protectors)
            {
                protector.StartProtection(gameObject);
            }
        }

        private void OnExit(object sender, GameObject gameObject)
        {
            foreach (var protector in _protectors)
            {
                protector.FinishProtection(gameObject);
            }
        }

        #endregion
    }
}
