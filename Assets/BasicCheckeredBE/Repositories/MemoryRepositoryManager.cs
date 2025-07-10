namespace BasicCheckeredBE.Repositories
{
    public class MemoryRepositoryManager
    {
        private GameBoardRepository _gameBoardRepository;
        
        
        public GameBoardRepository GameBoardRepository => _gameBoardRepository;
        
        public void Initialize()
        {
            _gameBoardRepository = new GameBoardRepository();
        }
    }
}