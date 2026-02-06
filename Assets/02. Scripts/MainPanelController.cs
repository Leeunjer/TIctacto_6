using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TicTacTockGame.Constans;

public class MainPanelController : MonoBehaviour 
{

    
    

    public void OnClickSinglePlayButton()
    {
        //todo 싱글 플레이 버튼을 눌렀을 때
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