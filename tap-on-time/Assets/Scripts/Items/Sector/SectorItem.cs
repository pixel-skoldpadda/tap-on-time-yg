using UnityEngine;

namespace Items.Sector
{
    [CreateAssetMenu(fileName = "_SECTOR_ITEM", menuName = "Items/Sector", order = 0)]
    public class SectorItem : ScriptableObject
    {
        [Header("General settings")]
        [SerializeField] private SectorType type;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector2 position;
        [SerializeField] private int health;

        [Space(10)]
        [Header("Graphics settings")]
        [SerializeField] private Color color;
        [SerializeField] private Material material;
        [SerializeField] private Sprite sprite;
        
        public int Health => health;
        public SectorType Type => type;
        public Color Color => color;
        public Material Material => material;
        public GameObject Prefab => prefab;
        public Vector2 Position => position;
        public Sprite Sprite => sprite;
    }
}