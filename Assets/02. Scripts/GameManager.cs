using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using static TicTacTockGame.Constans;
using TicTacTockGame;


public class GameManager : Singltone<GameManager>
{
    [SerializeField]
    private GameObject SettingPrefab; 
    [SerializeField] private Canvas canvas;   
    private GameType _gameType;

    private GameLogic _gameLogic;

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();

        var blockController = FindFirstObjectByType<BlockController>();
        

        if(blockController != null)
        {
            blockController.InitBlocks();
        }
        _gameLogic = new GameLogic(GameType.Dual,blockController);
    }

    public void OpenSettingsPanel()
    {
       var settingsPanelObject =  Instantiate(SettingPrefab , canvas.transform);
       settingsPanelObject.GetComponent<SettingPopUpController>().Show();
    }
    
    // 씬 전환 
    public void ChangeScene(GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene(SCENE_GAME);
    }
    public void ChangeMain(GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene(SCENE_MAIN);
    }
    

}
