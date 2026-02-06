using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using static TicTacTockGame.Constans;


public class GameManager : Singltone<GameManager>
{
    [SerializeField]
    private GameObject SettingPrefab; 
    [SerializeField] private Canvas canvas;   
    private GameType _gameType;

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();
        //씬이 로드 될 때 실행 되는 코드
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
        SceneManager.LoadScene("DualGame");
    }
    public void ChangeMain(GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene(0);
    }
    

}
