using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 Manager를 포함하는 Managers. 
// Manager를 사용하기위해 이 싱글톤 객체를 사용할 것.
// ex) Managers.Sound.Play()
public class Managers : MonoBehaviour
{   
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { if(!gameStart) Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    static Object _lock = new Object();
    static bool gameStart = false;

    #region Instances
    DataManager _data = new DataManager();
    SceneManager _scene = new SceneManager();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    ActionManager _action = new ActionManager();

    public static DataManager Data { get { return Instance._data; } }
    public static SceneManager Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static ActionManager Action { get { return Instance._action; } }
    #endregion

    void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        Clear();
    }

    static void Init()
    {
        gameStart = true;
        if (s_instance == null)
        {
            lock (_lock)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();

                s_instance._action.Init();
                s_instance._data.Init();
                s_instance._sound.Init();
                s_instance._scene.Init();
            }
        }
    }

    public static void Clear()
    {
        Scene.Clear();
        Data.Clear();
        Action.Init();
        //Sound.Clear();
    }
}
