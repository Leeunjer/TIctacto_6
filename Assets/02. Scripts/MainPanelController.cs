using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static TicTacTockGame.Constans;

public class MainPanelController : MonoBehaviour
{

    [SerializeField] private GameObject signinPanelPrefab;
    //[SerializeField] private GameObject signUpPanelPrefab;


    //// 회원가입 패널 테스트
    //void Start()
    //{
    //    string sid = PlayerPrefs.GetString("SID", null);

    //    if (string.IsNullOrEmpty(sid))
    //    {
    //        var signUpPanelObject = Instantiate(signUpPanelPrefab, transform);
    //        signUpPanelObject.GetComponent<SignUpPenelController>().Show(() =>
    //        {
    //            Debug.Log("회원가입 패널 테스트");
    //        });
    //    }

    //    var signinPanelObject = Instantiate(signinPanelPrefab, transform);
    //    signinPanelObject.GetComponent<SignInPanelController>();
    //}

    //public void OnClickGetScore()
    //{
    //    StartCoroutine(NetworkManager.Instance.GetScore((score) =>
    //    {
    //        GameManager.Instance.OpenConfirmPanel($"현제 점수 : {score}", () =>
    //        {

    //        });
    //    }, () =>
    //    {

    //    }));
    //}

    //public void OnClickSignOut()
    //{
    //    StartCoroutine(NetworkManager.Instance.Signout((result) =>
    //    {
    //        GameManager.Instance.OpenConfirmPanel($"로그아웃: {result.message}", () =>
    //        {
    //            PlayerPrefs.DeleteKey("SID");
    //        });
    //    }, () =>
    //    {

    //    }));
    //}

    public void OnClickSinglePlayButton()
    {
        GameManager.Instance.ChangeScene(GameType.Single);
    }

    public void OnClickDuelPlayButton()
    {
        GameManager.Instance.ChangeScene(GameType.Dual);
    }
    public void OnClickSettingButton()
    {
        GameManager.Instance.OpenSettingsPanel();
    }

    public void OnClickMultiPlayButton()
    {
        GameManager.Instance.ChangeScene(GameType.MultyPlay);
    }
}