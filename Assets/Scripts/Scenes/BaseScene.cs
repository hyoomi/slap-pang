using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 모든 Scene이 상속받을 class
public class BaseScene : MonoBehaviour
{
    // -- get, set을 처음본다면 공부해봅시당
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    [SerializeField] public List<GameObject> uiComponentPrefabs = new List<GameObject>();
    List<GameObject> uiComponents = new List<GameObject>();

    void Awake()
    {
        Init(); // 자식Class의 Init함수를 실행합니다. ex) LobbyScene의 Init()함수
       
    }

    protected virtual void Init()
    {
        foreach (var ui in uiComponentPrefabs) // UI를 Awake 시기에 생성합니다.
        {
            uiComponents.Add(Managers.UI.InstantiateUI(ui)); // 캔버스의 자식 컴포넌트로 부착합니다.
        }
    }

    public virtual void Clear()
    {

    }
}
