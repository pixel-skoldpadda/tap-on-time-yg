using System.Collections.Generic;
using Items.Sector;
using UnityEngine;

namespace Items.Level
{
    [CreateAssetMenu(fileName = "_LEVEL_ITEM", menuName = "Items/Level")]
    public class LevelItem : ScriptableObject
    {
        [SerializeField] private LevelType levelType;
        
        [Space(15)]
        [SerializeField] private Color backgroundColor;
        [SerializeField] private Color fieldColor;

        [Space(15)]
        [SerializeField] private int sectorsAmount;
        [SerializeField] private int startSpeed;
        
        [Space(15)]
        [SerializeField] [Range(0, 10)] private int moveSectorsProbability;
        [SerializeField] [Range(0, 10)] private int changeDirectionProbability;
        [SerializeField] [Range(0, 10)] private int generateTwoSectorsProbability;

        [Space(15)]
        [SerializeField] private List<SectorType> sectorTypes = new() { SectorType.Default };

        public LevelType LevelType => levelType;

        public int SectorsAmount => sectorsAmount;
        public int MoveSectorsProbability => moveSectorsProbability;
        public int ChangeDirectionProbability => changeDirectionProbability;
        public int GenerateTwoSectorsProbability => generateTwoSectorsProbability;
        public List<SectorType> SectorTypes => sectorTypes;

        public int StartSpeed => startSpeed;

        public Color BackgroundColor => backgroundColor;
        public Color FieldColor => fieldColor;
    }
}