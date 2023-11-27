using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "_LEVEL_ITEM", menuName = "Items/Level")]
    public class LevelItem : ScriptableObject
    {
        [SerializeField] private Color backgroundColor;
        [SerializeField] private Color fieldColor;
        
        [SerializeField] private int points;
        [SerializeField] private int startSpeed;
        
        [SerializeField] [Range(0,10)] private int moveSectorsProbability;
        [SerializeField] [Range(0,10)] private int changeDirectionProbability;

        public int Points => points;
        public int MoveSectorsProbability => moveSectorsProbability;
        public int ChangeDirectionProbability => changeDirectionProbability;
        public int StartSpeed => startSpeed;

        public Color BackgroundColor => backgroundColor;
        public Color FieldColor => fieldColor;
    }
}