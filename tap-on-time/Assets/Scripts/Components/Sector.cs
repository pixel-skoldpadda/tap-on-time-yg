using System;
using DG.Tweening;
using Items.Sector;
using ModestTree;
using UnityEngine;

namespace Components
{
    public class Sector : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private bool _move;
        private Action<Sector> _onTaped;
        private int _health;
        private SectorItem _item;

        private Sequence _scaleSequence;
        
        public void Init(SectorItem item, float angle, bool canMove)
        {
            _item = item;
            
            transform.RotateAround(Vector3.zero, Vector3.back, angle);
            _move = canMove;

            spriteRenderer.color = item.Color;
            spriteRenderer.sprite = item.Sprite;

            _health = item.Health;
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
            if (_scaleSequence != null)
            {
                _scaleSequence.Kill();
                _scaleSequence = null;
            }
        }

        public void Tap()
        {
            _health--;

            Sprite[] sprites = _item.CrackSprites;
            if (_health != 0 && !sprites.IsEmpty())
            {
                spriteRenderer.sprite = sprites[_health - 1];
                if (_scaleSequence == null)
                {
                    _scaleSequence = DOTween.Sequence()
                        .Append(transform.DOScale(new Vector3(1.2f, 1.2f), 0.12f))
                        .Append(transform.DOScale(Vector3.one, 0.12f))
                        .SetEase(Ease.OutSine)
                        .OnComplete(() => _scaleSequence = null);
                }
            }
            
            _onTaped?.Invoke(this);
        }
        
        public Action<Sector> OnTaped
        {
            get => _onTaped;
            set => _onTaped = value;
        }

        public int Health => _health;
        public SectorType Type => _item.Type;
    }
}