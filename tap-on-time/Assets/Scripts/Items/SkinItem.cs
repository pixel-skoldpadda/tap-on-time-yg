using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "_SKIN_ITEM", menuName = "Items/Skin")]
    public class SkinItem : ScriptableObject
    {
        [SerializeField] private SkinType type;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int price;

        public SkinType Type => type;
        public Sprite Sprite => sprite;
        public int Price => price;
    }
}