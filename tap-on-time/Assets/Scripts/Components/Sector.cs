using System;
using Items.Sector;
using UnityEngine;

namespace Components
{
    public class Sector : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private bool _move;
        private Action<Sector> _onTaped;
        
        public void Init(SectorItem item, float angle, bool canMove)
        {
            transform.RotateAround(Vector3.zero, Vector3.back, angle);
            _move = canMove;

            spriteRenderer.color = item.Color;
            spriteRenderer.material = item.Material;
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
        
        public Action<Sector> OnTaped
        {
            get => _onTaped;
            set => _onTaped = value;
        }
    }
}