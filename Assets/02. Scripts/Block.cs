using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class Block : MonoBehaviour
{
    [SerializeField] private Sprite oSprite;
    [SerializeField] private Sprite xSprite;
    [SerializeField] private SpriteRenderer markerSpriteRenderer;
    private int _blockIndex;

    public enum MarkerType {None,O,X}

    
    public void InitMarker(int blockIndex)
    {
        _blockIndex = blockIndex;
    }

    public void SetMarker(MarkerType markerType)
    {
        switch (markerType)
        {
            case MarkerType.O :
            markerSpriteRenderer.sprite = oSprite;
            break;

            case MarkerType.X :
            markerSpriteRenderer.sprite = xSprite;

            break;

            case MarkerType.None :
            markerSpriteRenderer.sprite = null;

            break;
        }
    }

    private void OUpAsButton()
    {
        
    }
}
