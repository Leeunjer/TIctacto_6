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

        public Constans.PlayerType[,] GetBoard
        {
            get{return _board;}
        }
        private BaseState _currentState;

        public enum GameResult{Win,Lose,Draw,None}

        public GameLogic(GameType gameType , BlockController blockController)
        {
            this.blockController = blockController;

            _board = new PlayerType[BOARD_SIZE,BOARD_SIZE];

            switch (gameType)
            {
                case GameType.Single:
                playerAState = new PlayerState(true);
                playerBState = new AIstate(false);

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
            _board[row, col] = playerType;

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

        public GameResult CheckGameResult()
        {
            if (TicTacToeAI.CheckGameWin(PlayerType.Player1,_board)){return GameResult.Win;}
            if(TicTacToeAI.CheckGameWin(PlayerType.Player2,_board)){return GameResult.Lose;}
            if(TicTacToeAI.CheckGameDraw(_board)){return GameResult.Draw;}
            return GameResult.None;
        }

       public void EndGame(GameResult gameResult)
    {
        string resultStr = "";
        switch (gameResult)
        {
            case GameResult.Win:
                resultStr = "Player1 승리!";
                break;
            case GameResult.Lose:
                resultStr = "Player2 승리!";
                break;
            case GameResult.Draw:
                resultStr = "무승부";
                break;
        }

        GameManager.Instance.OpenConfirmPanel(resultStr, () =>
        {
            GameManager.Instance.ChangeMain(GameType.Main);
        });
    }
        
    }
}