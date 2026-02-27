using System;
using System.Diagnostics;
using UnityEngine;
using static TicTacTockGame.Constans;

namespace TicTacTockGame
{
    public class GameLogic  : IDisposable
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
        //멀티 플레이를 처리하는 매니저
        private MultiplayManager _multiplayerManager;
        private string _multiplayRoomId;

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

                case GameType.MultyPlay:
                // 멀티플레이어 모드 초기화 작엄
                _multiplayerManager = new MultiplayManager((state,roomId) =>
                {

                    _multiplayRoomId = roomId;

                    switch (state)
                    {
                        case MultiPlayMangerState.CreateRoom:
                            //TODO: "상대방을 기다리고 있습니다." 라는 팝업 표시
                            UnityEngine.Debug.Log("방 생성 됨 , 방 : " + _multiplayRoomId);
                            

                        break;
                        case MultiPlayMangerState.JoinRoom:
                            //TODO:
                            playerAState = new MultiPlayerstate(true,_multiplayerManager);
                            playerBState = new PlayerState(false, _multiplayerManager, _multiplayRoomId);
                            SetState(playerAState);
                        break;
                        case MultiPlayMangerState.StartGame:
                            playerAState = new PlayerState(true , _multiplayerManager , _multiplayRoomId);
                            playerBState = new MultiPlayerstate(false, _multiplayerManager);
                            SetState(playerAState);
                        break;
                        case MultiPlayMangerState.ExitRoom:
                        //TODO : "본인이 나갔습니다." 팝업 표시

                        UnityEngine.Debug.Log("상대방이 나감, 방 ID" + _multiplayRoomId);

                        break;
                        case MultiPlayMangerState.EndGame:
                        //TODO : "상대방이 접속을 끊었습니다." 팝업 표시

                        UnityEngine.Debug.Log("" + _multiplayRoomId);
                        break;
                    }
                });
                
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

        public void Dispose()
        {
            _multiplayerManager?.LeaveRoom(_multiplayRoomId);
            _multiplayerManager?.Dispose();
        }
    }
}