using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "PlayerItem", menuName = "Items/Player", order = 0)]
    public class PlayerItem : ScriptableObject
    {
        [SerializeField] private Sprite skin;
        [SerializeField] private float speed;
        [SerializeField] private Vector3 startPoint;
        
        public Sprite Skin => skin;
        public float Speed => speed;
        public Vector3 StartPoint => startPoint;
    }
}