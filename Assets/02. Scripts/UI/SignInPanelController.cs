using TMPro;
using UnityEngine;



public class SignInPanelController : PanelController 
{
    [SerializeField] private TMP_InputField userNameInputfiled;
    [SerializeField] private TMP_InputField passwordInputfiled;

    

    public void OnClickConfirmButton()
    {
        //INput Feild에 입력된 값을 체크해서 서버에 전달

        var username = userNameInputfiled.text;
        
        var password = passwordInputfiled.text;
        

        if (string.IsNullOrEmpty(username)  || string.IsNullOrEmpty(password))
        {
            GameManager.Instance.OpenConfirmPanel("입력값이 누락되었습니다" , () =>
            {
                
            });
            //입력값이 누락되었습니다 , 팝업 창 표시 
        }

        var signInData = new SignInData();
        signInData.username = username;
        signInData.password = password;

        StartCoroutine(NetworkManager.Instance.SignIn(signInData, () =>
        {
           Hide(); 
        },()=>
        {
            userNameInputfiled.text = "";
            passwordInputfiled.text = "";
        }));

        
    }
    public void OnClickCancelButton()
    {
        Hide();
    }
}