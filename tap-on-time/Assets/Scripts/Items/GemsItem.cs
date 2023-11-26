using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "GEMS_ITEM", menuName = "Items/Gems")]
    public class GemsItem : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int amount;
        [SerializeField] private int cost;
        
        public GameObject Prefab => prefab;
        public int Amount => amount;
        public int Cost => cost;
    }
}