using CodeBase.Infrastructure.Services.AssetManagment;
using CodeBase.Infrastructure.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.UI
{
    public class UIFactory : IUIFactory
    {
        private const string HudPath = "UI/Hud";       
        private const string WinWindowPath = "UI/WinWindow";
        private const string LoseWindowPath = "UI/LoseWindow";
        
        private readonly IAssetProvider _assets;
        private readonly RectTransform _uiRoot;

        public UIFactory(IAssetProvider assets, RectTransform uiRoot)
        {
            _assets = assets;
            _uiRoot = uiRoot;
        }

        public Hud CreateHud()
        {
            GameObject hud = _assets.Instantiate(HudPath);
            hud.transform.SetParent(_uiRoot, false);
            return hud.GetComponent<Hud>();
        }

        public WinWindow CreateWinWindow()
        {
            GameObject winWindow = _assets.Instantiate(WinWindowPath);
            winWindow.transform.SetParent(_uiRoot, false);
            return winWindow.GetComponent<WinWindow>();
        }

        public LoseWindow CreateLoseWindow()
        {
            GameObject loseWindow = _assets.Instantiate(LoseWindowPath);
            loseWindow.transform.SetParent(_uiRoot, false);
            return loseWindow.GetComponent<LoseWindow>();
        }
    }
}