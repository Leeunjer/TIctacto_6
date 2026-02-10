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
            
        }

        public override void HandleNextTurn(GameLogic gameLogic)
        {
            
        }

        public override void OnEnter(GameLogic gameLogic)
        {

            GameManager.Instance.SetGameTurn(_playerType);
        }

        public override void OnExit(GameLogic gameLogic)
        {
            
        }
    }
}