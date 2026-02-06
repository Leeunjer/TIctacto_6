using UnityEngine;
using static TicTacTockGame.Constans;

public class GamePanelController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        GameManager.Instance.ChangeMain(GameType.Main);
    }

    
    public void OnClickSettingButton()
    {
        GameManager.Instance.OpenSettingsPanel();
    }
}
