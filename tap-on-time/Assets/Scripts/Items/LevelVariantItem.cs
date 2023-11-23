using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "_LEVEL_VARIANT_ITEM", menuName = "Items/LevelVariant")]
    public class LevelVariantItem : ScriptableObject
    {
        [SerializeField] private int points;
        [SerializeField] private int startSpeed;
        
        [SerializeField] [Range(0,10)] private int moveSectorsProbability;
        [SerializeField] [Range(0,10)] private int changeDirectionProbability;

        public int Points => points;

        public int MoveSectorsProbability => moveSectorsProbability;

        public int ChangeDirectionProbability => changeDirectionProbability;

        public int StartSpeed => startSpeed;
    }
}