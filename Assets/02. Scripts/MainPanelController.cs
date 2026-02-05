using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour 
{

    [SerializeField]
    private Button SingePlayButton,DualPlayButton,SettingButton;
    [SerializeField]
    GameObject Popup;

    public void OnClickSinglePlayButton()
    {
        //todo 싱글 플레이 버튼을 눌렀을 때
    }

    public void OnClickDuelPlayButton()
    {
        //todo 2p 플레이 버튼을 눌렀을 때
    }
    public void OnClickSettingButton()
    {
        //todo 설정 화면 클릭시
    }
}