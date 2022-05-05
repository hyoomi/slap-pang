using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 모든 Scene이 상속받을 class
// Init() 함수 내용을 다른곳에 작성하고 BaseScene Class를 abstract, virtual, interface로 돌리는것 고려
// SceneManager에서 UI(Canvas)도 Control할 것
public class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null) ;
        // Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        Managers.Sound.Play(); // Test Code
    }

    public virtual void Clear()
    {

    }
}
