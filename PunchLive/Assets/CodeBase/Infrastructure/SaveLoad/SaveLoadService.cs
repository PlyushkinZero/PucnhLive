using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.LevelManagement;
using CodeBase.Infrastructure.PersistantProgress;

namespace CodeBase.Infrastructure.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressFileName = "Progress";
        private readonly IPersistantProgressService _progress;
        private readonly LevelManager _levelManager;

        public SaveLoadService(IPersistantProgressService progress, LevelManager levelManager)
        {
            _progress = progress;
            _levelManager = levelManager;
        }

        public void SaveProgress()
            => BinarySerializer.Save(_progress.PlayerProgress, ProgressFileName);

        public void LoadProgress()
        {
            if (BinarySerializer.HasSaved(ProgressFileName) && _levelManager.UseSavedProgress)
            {
                _progress.PlayerProgress = BinarySerializer.Load<PlayerProgress>(ProgressFileName);
            }
            else
            {
                _progress.PlayerProgress = new PlayerProgress();
                _progress.PlayerProgress.PlayerStats = _levelManager.InitialPlayerData;
            }
        }
    }
}