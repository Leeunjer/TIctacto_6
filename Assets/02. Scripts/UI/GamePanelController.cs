using TicTacTockGame;
using UnityEngine;
using UnityEngine.UI;
using static TicTacTockGame.Constans;

public class GamePanelController : MonoBehaviour
{

    [SerializeField] private Image playerATurnPanel;
    [SerializeField] private Image PlayerBTruenPanel;

    public enum PlayerTurnType{None,ATurn,BTrun}

    public void OnClickBackButton()
    {
        GameManager.Instance.OpenConfirmPanel("게임을 종료합니다.", () =>
        {
            GameManager.Instance.ChangeMain(GameType.Main);
        });
    }

    

    
    public void OnClickSettingButton()
    {
        GameManager.Instance.OpenSettingsPanel();
    }

    public void SetPlayerTurnPanel(Constans.PlayerType playerTurnType)
    {
        switch (playerTurnType)
        {
            case Constans.PlayerType.None:
                playerATurnPanel.color = Color.white;
                PlayerBTruenPanel.color = Color.white;
                break;
            case Constans.PlayerType.Player1:
                playerATurnPanel.color = Color.blue;
                PlayerBTruenPanel.color = Color.white;
                break;
            case Constans.PlayerType.Player2:
                playerATurnPanel.color = Color.white;
                PlayerBTruenPanel.color = Color.blue;
                break;
        }
    }
}
