using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TicTacTockGame.Constans;

public class MainPanelController : MonoBehaviour 
{

    
    

    public void OnClickSinglePlayButton()
    {
        GameManager.Instance.ChangeScene(GameType.Single);
    }

    public void OnClickDuelPlayButton()
    {
        GameManager.Instance.ChangeScene(GameType.Dual);
    }
    public void OnClickSettingButton()
    {
        GameManager.Instance.OpenSettingsPanel();
    }
}