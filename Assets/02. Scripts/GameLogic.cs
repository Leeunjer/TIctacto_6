using UnityEngine;
using static TicTacTockGame.Constans;

namespace TicTacTockGame
{
    public class GameLogic  
    {
        public BlockController blockController;


        public BaseState playerAState;
        public BaseState playerBState;

        private  PlayerType[,] _board;

        private BaseState _currentState;
        public GameLogic(GameType gameType , BlockController blockController)
        {
            this.blockController = blockController;

            _board = new PlayerType[BOARD_SIZE,BOARD_SIZE];

            switch (gameType)
            {
                case GameType.Single:
                playerAState = new PlayerState(true);
                playerBState = new AIstate();

                SetState(playerAState);
                break;

                case GameType.Dual:
                playerAState = new PlayerState(true);
                playerBState = new PlayerState(false);
                SetState(playerAState);
                break;
            }
        }

        public void SetState(BaseState newState)
        {
            _currentState?.OnExit(this);
            _currentState = newState;
            _currentState.OnEnter(this);
        }

        public bool PlaceMarker(int index, PlayerType playerType)
        {
            var row = index / BOARD_SIZE;
            var col = index % BOARD_SIZE;

             if(_board[row,col] != Constans.PlayerType.None)return false;


            blockController.PlaceMarker(index, playerType);

            return true;
        }

        public void ChangeGameState()
        {
            if(_currentState == playerAState)
            {
                SetState(playerBState);
            }
            else
            {
                SetState(playerAState);
            }
        }

        public void CheckGameResult()
        {
            
        }

        public bool CheckGameWin(Constans.PlayerType playerType, Constans.PlayerType[,] Board)
        {
            return true;
        }
    }
}