using UnityEngine;
using TMPro;





public class SignUpPenelController : PanelController
{
    [SerializeField] private TMP_InputField userNameInputfiled;
    [SerializeField] private TMP_InputField passwordInputfiled;
    [SerializeField] private TMP_InputField confirmPasswordInputfiled;
    [SerializeField] private TMP_InputField NickNameInputfiled;


    //회원가입 팝업창에서 확인 버튼 클릭시 동작할 함수를 전달 받기 위해 만든 델리게이트
    public delegate void OnSignUpButtonClicked();

    private OnSignUpButtonClicked _onSignUpButtonClicked;

    public void Show(OnSignUpButtonClicked onSignUpButtonClicked)
    {
        _onSignUpButtonClicked = onSignUpButtonClicked;
        Show();
    }



    public void OnClickConfirmButton()
    {
        //INput Feild에 입력된 값을 체크해서 서버에 전달

        var userName = userNameInputfiled.text;
        var nickname = NickNameInputfiled.text;
        var password = passwordInputfiled.text;
        var confirmPassword = confirmPasswordInputfiled.text;

        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            //입력값이 누락되었습니다 , 팝업 창 표시 
        }

        if (password.Equals(confirmPassword))
        {
            // 동일하면 회원가입 실행

            var SignUpData = new SignUpData();
            SignUpData.username = userName;
            SignUpData.password = password;
            SignUpData.nickname = nickname;

            //서버로 signupdata를 전달하면서 회원가임
            StartCoroutine(NetworkManager.Instance.signup(SignUpData, () =>{
                    Hide();
            }, () =>
            {
                userNameInputfiled.text = "";
                passwordInputfiled.text = "";
                confirmPasswordInputfiled.text = "";
                NickNameInputfiled.text = "";
            }));
        }
        else
        {
            //비밀번호가 다릅니다 팝업 표시
            GameManager.Instance.OpenConfirmPanel("비밀번호가 다릅니다.", () =>
            {
                passwordInputfiled.text = "";
                confirmPasswordInputfiled.text = "";
            });
        }
    }
    public void OnClickCancelButton()
    {
        Hide();
    }
}
