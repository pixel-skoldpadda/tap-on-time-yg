using System;
using UnityEngine;

namespace Components
{
    public class Sector : MonoBehaviour
    {
        private bool _move;

        private Action<Sector> _onTaped;

        private Vector3 defaultPosition;
        private Quaternion defaultRotation;
        
        private void Awake()
        {
            Transform sectorTransform = transform;
            defaultPosition = sectorTransform.position;
            defaultRotation = sectorTransform.rotation;
        }

        private void Update()
        {
            if (_move)
            {
                transform.RotateAround(Vector3.zero, Vector3.back, 30 * Time.deltaTime);   
            }
        }

        private void OnDestroy()
        {
            _onTaped = null;
        }

        public void Tap()
        {
            _onTaped?.Invoke(this);
        }

        public void Reset()
        {
            Transform sectorTransform = transform;
            sectorTransform.position = defaultPosition;
            sectorTransform.rotation = defaultRotation;
            _move = false;
            gameObject.SetActive(false);
        }

        public bool Move
        {
            set => _move = value;
        }

        public Action<Sector> OnTaped
        {
            get => _onTaped;
            set => _onTaped = value;
        }
    }
}