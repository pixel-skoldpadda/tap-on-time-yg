using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "SECTORS_ITEM", menuName = "Items/Sectors", order = 0)]
    public class SectorsItem : ScriptableObject
    {
        [SerializeField] private GameObject[] sectorPrefabs;
        [SerializeField] private GameObject finishSector;
        [SerializeField] private Vector3 spawnPoint;

        public GameObject[] SectorPrefabs => sectorPrefabs;
        public GameObject FinishSector => finishSector;
        public Vector3 SpawnPoint => spawnPoint;
    }
}