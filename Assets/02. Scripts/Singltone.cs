using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Singltone<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if(_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoad; // 씬 메니저가 씬을 로드 할 때 onScenLoad를 출력함
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected abstract void OnSceneLoad(Scene scene, LoadSceneMode mode);
}
