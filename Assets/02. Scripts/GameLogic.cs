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
            if (CheckGameWin(PlayerType.Player1,_board)){return GameResult.Win;}
            if(CheckGameWin(PlayerType.Player2,_board)){return GameResult.Lose;}
            if(CHeckGameDraw(_board)){return GameResult.Draw;}
            return GameResult.None;
        }

        public bool CheckGameWin(Constans.PlayerType playerType, Constans.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType &&
                board[row, 1] == playerType &&
                board[row, 2] == playerType)
            {
                return true;
            }
        }
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType &&
                board[1, col] == playerType &&
                board[2, col] == playerType)
            {
                return true;
            }
        }
        if (board[0,0] == playerType &&
            board[1,1] == playerType &&
            board[2,2] == playerType)
        {
            return true;
        }
        if (board[0,2] == playerType &&
            board[1,1] == playerType &&
            board[2,0] == playerType)
        {
            return true;
        }
        return false;
    }

        public bool CHeckGameDraw(Constans.PlayerType[,] board)
        {
            for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constans.PlayerType.None) return false;
            }
        }
        return true;
        }

        public void EndGame(GameResult gameResult)
        {
            //todo : 게임 오버 시 팝업 띄고 확인 누를 시 main 씬 전환 
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