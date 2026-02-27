using TicTacTockGame;
using UnityEngine;

public class MultiPlayerstate : BaseState
{
    private Constans.PlayerType _playerType;

    private MultiplayManager _multiplayManger;

    public MultiPlayerstate(bool isfirstPlayer, MultiplayManager multiplayManager)
    {
        _playerType = isfirstPlayer? Constans.PlayerType.Player1 : Constans.PlayerType.Player2;
        _multiplayManger = multiplayManager;
    }
    public override void HandleMove(GameLogic gameLogic, int index)
    {
        ProcessMove(gameLogic,index , _playerType);
    }

    public override void HandleNextTurn(GameLogic gameLogic)
    {
        gameLogic.ChangeGameState();
    }

    public override void OnEnter(GameLogic gameLogic)
    {
       _multiplayManger.onOpponentMove = movedata =>
       {
        if(movedata.position >= 0 && movedata.position < Constans.BOARD_SIZE * Constans.BOARD_SIZE)
           {

               UnityThread.executeInUpdate(() =>
               {
                HandleMove(gameLogic, movedata.position) ;

                GameManager.Instance.SetGameTurn(_playerType);
               });

               
           }
           else
           {
               //todo : 유효하지 않은 이동 데이터 처리
           }
       };
    }

    public override void OnExit(GameLogic gameLogic)
    {
        _multiplayManger.onOpponentMove = null;
    }
}
