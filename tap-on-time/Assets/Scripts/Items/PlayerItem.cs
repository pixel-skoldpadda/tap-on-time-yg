using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "PLAYER_ITEM", menuName = "Items/Player", order = 0)]
    public class PlayerItem : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float speed;
        [SerializeField] private Vector3 startPoint;
        
        public float Speed => speed;
        public Vector3 StartPoint => startPoint;
        public GameObject Prefab => prefab;
    }
}