using CodeBase.Infrastructure.Services.AssetManagment;
using CodeBase.Infrastructure.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.UI
{
    public class UIFactory : IUIFactory
    {
        private const string PauseMenuPath = "UI/PauseMenu";
        private readonly IAssetProvider _assets;
        private readonly RectTransform _uiRoot;

        public UIFactory(IAssetProvider assets, RectTransform uiRoot)
        {
            _assets = assets;
            _uiRoot = uiRoot;
        }

        public PauseWindow CreatePauseWindow()
        {
            GameObject pauseWindow = _assets.Instantiate(PauseMenuPath);
            pauseWindow.transform.SetParent(_uiRoot, false);
            return pauseWindow.GetComponent<PauseWindow>();
        }
    }
}