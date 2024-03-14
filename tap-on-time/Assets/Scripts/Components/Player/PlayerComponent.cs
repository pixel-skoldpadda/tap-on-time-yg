using System;
using Infrastructure.Services.Items;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
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

        private bool _isPointerOverGameObject;

        private Sector _collidedSector;
        
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

        private void Update()
        {
            if (_moveAroundComponent.IsMoving)
            {
                _isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();   
            }
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
        
        public void StopMoving()
        {
            _moveAroundComponent.StopMove();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _collidedSector = other.gameObject.GetComponent<Sector>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _collidedSector = null;
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

        private void OnDestroy()
        {
            YandexGame.savesData.OnSkinChanged -= OnSkinChanged;
        }

        public Sector CollidedSector
        {
            get => _collidedSector;
            set => _collidedSector = value;
        }

        public bool IsPointerOverGameObject => _isPointerOverGameObject;
    }
}
