using System;
using System.Collections;
using TicTacTockGame;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SignUpData
{
    public string username;
    public string nickname;
    public string password;

}

public struct SignInData
{
    public string username;
    public string password;
    
}
public struct SigninResult
{
    public string message;
}

public struct ScoreResult
{
    public int score;
}


public class NetworkManager : Singltone<NetworkManager>
{

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // 네트워크 매니저는 씬이 로드될 때마다 초기화할 필요가 없습니다.
        // 따라서 이 메서드는 비워둡니다.
    }

    /// <summary>
    /// 회원가입을 위한 함수
    /// </summary>
    /// <param name="signUpData"> 회원가입에 필요한 정보</param>
    /// <returns></returns>
    public IEnumerator signup(SignUpData signUpData , Action success , Action failure)
    {
        string jsonString = JsonUtility.ToJson(signUpData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www = new UnityWebRequest(Constans.ServerURL + "/users/signup" , UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type","application/json");

            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError|| 
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                if (www.responseCode == 409)
                {
                    GameManager.Instance.OpenConfirmPanel("이미 가임된 아이디 입니다", () =>
                    {
                        failure?.Invoke();
                    });
                }else if(www.responseCode == 400){
                    GameManager.Instance.OpenConfirmPanel("필수 요소가 누락되었습니다", () =>
                    {
                       failure?.Invoke(); 
                    });
                }

                //오류 발생 팝업 표시
                GameManager.Instance.OpenConfirmPanel("회원가입이 실패했습니다", () =>
                {
                   failure?.Invoke(); 
                });
            }
            else
            {
                var result = www.downloadHandler.text;
                Debug.Log("REsult: " + result);

                //회원가입 성공 팝업 표시
                GameManager.Instance.OpenConfirmPanel("회원가입이 성공적으로 완료되었습니다", ()=>
                {
                    success?.Invoke();
                });
            }
        }
 
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="signInData">로그인에 필요한 데이터</param>
    /// <param name="success">성공시 호출할 함수</param>
    /// <param name="failure">실패시 호출할 함수</param>
    /// <returns></returns>
    public IEnumerator SignIn(SignInData signInData , Action success, Action failure)
    {
        string jsonString = JsonUtility.ToJson(signInData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest WWW = new UnityWebRequest(Constans.ServerURL + "/users/signin", UnityWebRequest.kHttpVerbPOST))
        {
            WWW.uploadHandler = new UploadHandlerRaw(bodyRaw);
            WWW.downloadHandler = new DownloadHandlerBuffer();
            WWW.SetRequestHeader("Content-Type","application/json");

            yield return WWW.SendWebRequest();

            if (WWW.result == UnityWebRequest.Result.ConnectionError|| 
                WWW.result == UnityWebRequest.Result.ProtocolError)
            {
                //오류 코드 처리
            }
            else
            {
                var cookie = WWW.GetResponseHeader("Set-Cookie");

                if (!string.IsNullOrEmpty(cookie))
                {
                    int lastIndex = cookie.LastIndexOf(";");
                    string sid = cookie.Substring(0,lastIndex);
                    PlayerPrefs.SetString("SID",sid);
                }

                var resultString = WWW.downloadHandler.text;
                var result = JsonUtility.FromJson<SigninResult>(resultString);

                Debug.Log("Result" + resultString);
            }

        }

        yield return null;
    }


    public IEnumerator GetScore(Action<ScoreResult> success, Action failure)
    {
        using (UnityWebRequest WWW = new UnityWebRequest(Constans.ServerURL + "/users/score", UnityWebRequest.kHttpVerbGET))
        {
            
            WWW.downloadHandler = new DownloadHandlerBuffer();

            string sid = PlayerPrefs.GetString("SID" , null);
            if (!string.IsNullOrEmpty(sid))
            {
                WWW.SetRequestHeader("Cookie",sid);
            }

            yield return WWW.SendWebRequest();

            if (WWW.result == UnityWebRequest.Result.ConnectionError ||
                WWW.result == UnityWebRequest.Result.ProtocolError)
            {
                if(WWW.responseCode == 400)
                {
                    //400오류 발생
                }
                failure?.Invoke();
            }
            else
            {
                var resultString = WWW.downloadHandler.text;
                var result = JsonUtility.FromJson<ScoreResult>(resultString);
                Debug.Log("Score : " + result);

                success?.Invoke(result);
            }


        }
        
        yield return null;
    }

    public IEnumerator Signout(Action<SigninResult> success, Action failure)    
    {
    using (UnityWebRequest www = new UnityWebRequest(Constans.ServerURL + "/users/signout", UnityWebRequest.kHttpVerbGET))
    {
        www.downloadHandler = new DownloadHandlerBuffer();

        string sid = PlayerPrefs.GetString("SID", null);
        if (!string.IsNullOrEmpty(sid))
        {
            www.SetRequestHeader("Cookie", sid);
        }

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError
            || www.result == UnityWebRequest.Result.ProtocolError)
        {
            // 오류 코드별 처리
            if (www.responseCode == 400)
            {
                // 400 오류 발생 팝업 표시
            }

            failure?.Invoke();
        }
        else
        {
            var resultString = www.downloadHandler.text;
            var result = JsonUtility.FromJson<SigninResult>(resultString);
            Debug.Log("Score: " + result.message);

            success?.Invoke(result);
        }
    }
}






}
