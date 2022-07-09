using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scene을 제어하는 Manager
public class SceneManager 
{
    // 현재 Scene
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public bool Login;

    public void Init()
    {
        Login = false;
    }

    // Scene을 Load하는 함수
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadScene(GetSceneName(type));
        Time.timeScale = 1;
    }

    // Scene의 이름을 가져오는 함수. Enum의 활용.
    public string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {

    }
}
