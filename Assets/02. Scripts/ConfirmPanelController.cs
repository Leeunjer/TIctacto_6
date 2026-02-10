using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController {
    [SerializeField] Transform contrnsfom;
    [SerializeField] TMP_Text messageText;

    public delegate void OnConfirmButtonClicked();

    public OnConfirmButtonClicked onConfirmButtonClicked;



    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {   
        this.onConfirmButtonClicked = onConfirmButtonClicked;
        messageText.text = message;
        Show();
    }

    public void OnClickCloseButton()
    {
        Hide(() =>
        {
            onConfirmButtonClicked?.Invoke();
        });
    }
}