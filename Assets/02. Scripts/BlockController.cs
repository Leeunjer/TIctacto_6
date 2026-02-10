using TicTacTockGame;
using UnityEngine;
using UnityEngine.Timeline;
public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[]Blocks;

    public delegate void OnBlcokClicked(int index);

    public OnBlcokClicked onBlcokClicked;
    public void InitBlocks()
    {
        //블록 초기화
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].InitMarker(i, blockIndex =>
            {
                onBlcokClicked?.Invoke(blockIndex);
                Debug.Log("BlockClicked in BlockController" + blockIndex);
            });
        }
    }

    public void PlaceMarker(int blockIndex, Constans.PlayerType playerType)
    {
        switch (playerType)
        {
            case Constans.PlayerType.Player1:
            Blocks[blockIndex].SetMarker(Block.MarkerType.O);
            break;
            case Constans.PlayerType.Player2:
            Blocks[blockIndex].SetMarker(Block.MarkerType.X);
            break;

        }
    }

    
}
