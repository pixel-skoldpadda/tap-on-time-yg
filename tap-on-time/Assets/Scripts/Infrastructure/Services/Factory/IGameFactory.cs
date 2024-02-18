using Components;
using Items.Sector;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        void CreatePlayer();
        void CreateLevelGenerator();
        void CreateGems();
        void CreateGameField();
        void CreateTapArea();
        void CreateConfetti();
        Sector CreateSector(SectorItem item, float angle, bool canMove);
    }
}