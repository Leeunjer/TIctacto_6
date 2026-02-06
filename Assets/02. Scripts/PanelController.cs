
using UnityEngine;
using UnityEngine.UI;

[[RequireComponent(typeof(CanvasGroup))]]

public class PanelController : MonoBehaviour {
    

    [SerializeField]
    private RectTransform panelTransform;
    private CanvasGroup _canvasGroup;

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        
    }
    public void Hide()
    {
        
    }

}