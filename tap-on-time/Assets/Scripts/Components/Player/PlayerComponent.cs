using System;
using Infrastructure.Services.Items;
using Items;
using UnityEngine;
using YG;

namespace Components.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private MoveAroundComponent _moveAroundComponent;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private bool _collision;

        private Vector3 _startPosition;
        private Quaternion _startQuaternion;

        private IItemsService _items;

        public void Construct(SkinItem skinItem, IItemsService items)
        {
            _items = items;
            spriteRenderer.sprite = skinItem.Sprite;
            YandexGame.savesData.OnSkinChanged += OnSkinChanged;
        }

        private void OnSkinChanged(SkinType type)
        {
            spriteRenderer.sprite = _items.GetSkinItem(type).Sprite;
        }

        private void Start()
        {
            _startPosition = transform.position;
            _startQuaternion = transform.rotation;
        }

        public void StopMoving()
        {
            _moveAroundComponent.StopMove();
        }

        public void StartMove360()
        {
            _moveAroundComponent.StartMove360();
        }

        public void AddMove360EndAction(Action action)
        {
            _moveAroundComponent.OnMove360End += action;
        }
        
        public void RemoveMove360EndAction(Action action)
        {
            _moveAroundComponent.OnMove360End -= action;
        }
        
        public void StartMoving()
        {
            _moveAroundComponent.StartMove();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _collision = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _collision = false;
        }

        public void ResetComponent()
        {
            transform.position = _startPosition;
            transform.rotation = _startQuaternion;
            _moveAroundComponent.ResetComponent();
        }

        public void ChangeSpeed(float speed)
        {
            _moveAroundComponent.ChangeSpeed(speed);
        }

        public void ChangeDirection()
        {
            _moveAroundComponent.ChangeDirection();
        }

        public bool Collision => _collision;

        private void OnDestroy()
        {
            YandexGame.savesData.OnSkinChanged -= OnSkinChanged;
        }
    }
}
