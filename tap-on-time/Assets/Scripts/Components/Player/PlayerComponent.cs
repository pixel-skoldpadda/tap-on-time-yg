using System;
using Items;
using UnityEngine;

namespace Components.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private MoveAroundComponent _moveAroundComponent;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private bool _collision;

        private Vector3 _startPosition;
        private Quaternion _startQuaternion;
        

        public void Construct(SkinItem skinItem)
        {
            spriteRenderer.sprite = skinItem.Sprite;
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
    }
}
