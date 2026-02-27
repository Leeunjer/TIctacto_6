
using TicTacTockGame;
using UnityEngine;

public class PlayerState : BaseState
{
    private Constans.PlayerType _playerType;

    //멀티 플레이 관련 변수
    private bool _isMultiplayer;
    private MultiplayManager _multiplayManager;
    private string _multiPlayerRoomId;

    public PlayerState(bool isfirstPlayer)
    {
        _playerType = isfirstPlayer? Constans.PlayerType.Player1 : Constans.PlayerType.Player2;
        _isMultiplayer = false;
    }

    public PlayerState(bool isfirstPlayer , MultiplayManager multiplayManager , string roomId)
    {
        _playerType = isfirstPlayer? Constans.PlayerType.Player1 : Constans.PlayerType.Player2;
        _isMultiplayer = true;
        _multiplayManager = multiplayManager;
        _multiPlayerRoomId = roomId;
       
        
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

        //멀티 플레이인 경우 , 상대방에게 이동 경로 전송

        if (_isMultiplayer)
        {
            _multiplayManager.SendPlayerMove(_multiPlayerRoomId,index);
        }
    }

    public override void HandleNextTurn(GameLogic gameLogic)
    {
        gameLogic.ChangeGameState();
    }

    

    public override void OnExit(GameLogic gameLogic)
    {
        gameLogic.blockController.onBlcokClicked = null;
        
    }
}