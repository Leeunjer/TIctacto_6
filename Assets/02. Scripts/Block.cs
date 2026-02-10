using System;
using UnityEditor;
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

    public delegate void OnBlcokClicked(int index); // 함수의 시그니쳐

    private OnBlcokClicked _onBlcokClicked;
    

    public void InitMarker(int blockIndex, OnBlcokClicked onBlcokClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarkerType.None);
        _onBlcokClicked = onBlcokClicked;
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

    private void  OnMouseUpAsButton() {
         if (EventSystem.current.IsPointerOverGameObject())
        {
            
            return;
        }
        _onBlcokClicked?.Invoke(_blockIndex);
    }
}
