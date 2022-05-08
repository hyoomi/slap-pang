using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 모든 Scene이 상속받을 class
// 빈 GameObject에 BaseScene을 상속받은 스크립트를 붙여서 사용
public class BaseScene : MonoBehaviour
{
    // -- get, set을 처음본다면 공부해봅시당
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    [SerializeField] public List<GameObject> uiComponentPrefabs = new List<GameObject>(); // Prefabs를 담아둘 List
    List<GameObject> uiComponents = new List<GameObject>(); // Prefabs를 Instantiate한 GameObjects를 담아둘 List

    void Awake()
    {
        Init(); // 자식class의 Init함수를 실행. 자식class의 Init함수가 없다면 자신의 Init함수 실행
    }

    protected virtual void Init()
    {
        foreach (var ui in uiComponentPrefabs) // 각 씬에 해당하는 UI를 불러옴
        {
            uiComponents.Add(Managers.UI.InstantiateUI(ui));
        }
    }

    public virtual void Clear()
    {

    }
}
