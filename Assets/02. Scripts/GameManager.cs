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
    [SerializeField] private GameObject confirmPanelPrefab;

    private GamePanelController _gamePanelController;
    private GameType _gameType;

    private GameLogic _gameLogic;

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();
        if (scene.name == SCENE_GAME)
        {
            var blockController = FindFirstObjectByType<BlockController>();
        

        if(blockController != null)
        {
            blockController.InitBlocks();
        }

         // GamePanelController 참조 가져오기
        _gamePanelController = FindFirstObjectByType<GamePanelController>();

        _gameLogic = new GameLogic(GameType.Dual,blockController);
        }
    }
    //셋팅 페널
    public void OpenSettingsPanel()
    {
       var settingsPanelObject =  Instantiate(SettingPrefab , canvas.transform);
       settingsPanelObject.GetComponent<SettingPopUpController>().Show();
    }
    //컨펌 페널
    public void OpenConfirmPanel(string message , ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        var confirmPanaelOBJ = Instantiate(confirmPanelPrefab , canvas.transform);
        confirmPanaelOBJ.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClicked);
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
    // Game O/X UI 업데이트
    public void SetGameTurn(Constans.PlayerType playerTurnType)
    {
        _gamePanelController.SetPlayerTurnPanel(playerTurnType);
    }

}
