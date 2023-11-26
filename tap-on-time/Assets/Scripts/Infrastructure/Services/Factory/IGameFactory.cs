namespace Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        void CreatePlayer();
        void CreateSectors();
        void CreateLevelGenerator();
        void CreateGems();
        void CreateGameField();
    }
}