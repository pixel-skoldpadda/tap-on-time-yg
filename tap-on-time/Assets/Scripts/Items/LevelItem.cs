using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "LEVEL_ITEM", menuName = "Items/Level", order = 0)]
    public class LevelItem : ScriptableObject
    {
        [SerializeField] private List<GameObject> sectors;
        [SerializeField] private GameObject finishSector;
        [SerializeField] private List<LevelVariantItem> variants;
        
        public List<GameObject> Sectors => sectors;
        public GameObject FinishSector => finishSector;
        public List<LevelVariantItem> Variants => variants;
    }
}