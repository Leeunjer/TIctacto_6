
namespace TicTacTockGame
{
    
    
    public abstract class BaseState
    {
        public abstract void OnEnter(GameLogic gameLogic);
        public abstract void HandleMove(GameLogic gameLogic, int index);
        public abstract void OnExit(GameLogic gameLogic);
        public abstract void HandleNextTurn(GameLogic gameLogic);

        public void ProcessMove(GameLogic gameLogic, int index, Constans.PlayerType playerType)
        {
            // 특정 위치의 마커를 표시하는 로직
           if( gameLogic.PlaceMarker(index, playerType))
            {
            HandleNextTurn(gameLogic); // 턴 전환
                
            }

        }


    }    
}