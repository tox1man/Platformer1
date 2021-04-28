using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    public class ContactsPuller
    {
        private const float _collisionThresh = 0.5f;

        private List<ContactPoint2D> _contactsList = new List<ContactPoint2D>();
        private readonly Collider2D _collider2D;
        private int _contactsCount;

        public bool _hasLeftContact { get; private set; }
        public bool _hasRightContact { get; private set; }
        public bool _hasBottomContact { get; private set; }
        public bool _hasUpperContact { get; private set; }

        public ContactsPuller(Collider2D collider2D)
        {
            _collider2D = collider2D;
        }

        public void Update()
        {
            _hasLeftContact = false;
            _hasRightContact = false;
            _hasBottomContact = false;
            _hasUpperContact = false;

            _contactsCount = _collider2D.GetContacts(_contactsList);

            for (int i = 0; i < _contactsCount; i++)
            {
                var normal = _contactsList[i].normal;
                var rigidBody = _contactsList[i].rigidbody;

                if (normal.x >  _collisionThresh && rigidBody == null) { _hasLeftContact   = true; }
                if (normal.x < -_collisionThresh && rigidBody == null) { _hasRightContact  = true; }
                if (normal.y >  _collisionThresh) { _hasBottomContact = true; }
                if (normal.y < -_collisionThresh) { _hasUpperContact  = true; }
            }
        }
    }
}
