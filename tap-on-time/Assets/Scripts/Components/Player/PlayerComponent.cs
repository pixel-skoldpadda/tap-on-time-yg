using Items;
using Player;
using UnityEngine;

namespace Components.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private MoveAroundComponent _moveAroundComponent;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private PlayerItem _playerItem;

        private Vector3 _startPosition;
        private Quaternion _startQuaternion;

        private bool _collision;

        public void Construct(PlayerItem playerItem, SkinItem skinItem)
        {
            _playerItem = playerItem;
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
        }

        public void UpdateSpeed(float speed)
        {
            // angularSpeed = speed;
        }

        public void ChangeDirection()
        {
            _moveAroundComponent.ChangeDirection();
        }

        public bool Collision => _collision;
    }
}
