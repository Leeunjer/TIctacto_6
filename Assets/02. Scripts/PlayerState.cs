
using TicTacTockGame;
using UnityEngine;

public class PlayerState : BaseState
{
    private Constans.PlayerType _playerType;
    public PlayerState(bool isfirstPlayer)
    {
        _playerType = isfirstPlayer? Constans.PlayerType.Player1 : Constans.PlayerType.Player2;
    }
    public override void OnEnter(GameLogic gameLogic)
    {
        gameLogic.blockController.onBlcokClicked = (blockIndex) =>
        {
            HandleMove(gameLogic,blockIndex);
        };

        // 상태 진입시 로직 구현
        GameManager.Instance.SetGameTurn(_playerType);
    }
    public override void HandleMove(GameLogic gameLogic, int index)
    {
        ProcessMove(gameLogic,index , _playerType);
    }

    public override void HandleNextTurn(GameLogic gameLogic)
    {
        gameLogic.ChangeGameState();
    }

    

    public override void OnExit(GameLogic gameLogic)
    {
    }
}