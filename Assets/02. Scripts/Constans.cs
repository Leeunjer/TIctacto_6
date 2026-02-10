using System;
using Unity.VisualScripting;

namespace TicTacTockGame
{
    public static class Constans
    {

    public const string SCENE_MAIN = "Main";
    public const string SCENE_GAME = "Dual";


        public enum GameType{Main,Single,Dual}

        public enum PlayerType { None, Player1, Player2 }
        public const int BOARD_SIZE = 3;
    }
}