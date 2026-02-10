 using UnityEngine;
namespace TicTacTockGame
{


    public class AIstate : BaseState
    {

        private Constans.PlayerType _playerType;
        public AIstate(bool isfirstPlayer)
        {
        _playerType = isfirstPlayer? Constans.PlayerType.Player1 : Constans.PlayerType.Player2;
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

             // OX UI 업데이트
        GameManager.Instance.SetGameTurn(_playerType);

        var board = gameLogic.GetBoard;
        var result = TicTacToeAI.GetBestMove(board);

        if (result.HasValue)
        {
            int row = result.Value.row;
            int col = result.Value.col;
            int index = row * Constans.BOARD_SIZE + col;

            HandleMove(gameLogic, index);
        } 
        }

        public override void OnExit(GameLogic gameLogic)
        {
            
        }
    }
}