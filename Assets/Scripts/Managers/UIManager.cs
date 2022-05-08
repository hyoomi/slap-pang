using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// @UI Canvas를 만들고 산하에 UI를 관리할 수 있게 하는 UIManager
public class UIManager
{
    int _order = 10;

    // @UI 오브젝트를 만들고 UIManager가 들고 있는다(유일성 보장)
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI");
            if (root == null)
                root = new GameObject { name = "@UI" };
            SetCanvas(root);
            return root;
        }
    }

    // SetCanvas에서 편하게 Component를 Get하기 위한 함수. 편의성을 위함.
    static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    // 오브젝트를 Canvas로 세팅해준다
    void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = GetOrAddComponent<Canvas>(go);
        CanvasScaler cs = GetOrAddComponent<CanvasScaler>(go);
        GetOrAddComponent<GraphicRaycaster>(go);

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(1080, 1920);
        cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

        // Canvas가 여러개일 경우, 나중에 생성된 Canvas의 우선순위를 더 높게 만들어준다
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }        
    }

    // path를 입력받아 Load하는 함수
    public GameObject LoadUI<T>(string name = null) where T : BaseUI // 타입 T는 BaseUI형태여야 한다(상속o)
    {
        string path = name;

        if (string.IsNullOrEmpty(name))  // name이 없다면 타입이 곧 path
            path = typeof(T).Name;

        if(typeof(T) == typeof(PopupUI)) // 타입이 PopupUI라면 path는 Popup/name
            path = $"Popup/{name}";

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{path}"); // Resources폴더안에 Prefabs폴더안에 UI폴더안에서 프리펩을 찾아온다
        if (go == null) // 로드 실패 오류처리
        {
            Debug.Log($"Failed to load prefab : Prefabs/UI/{path}");
            return null;
        }
        
        InstantiateUI(go); // 로드해온 것을 Instantiate
        return go;
    }

    // GameObject를 Instatiate하는 함수
    public GameObject InstantiateUI(GameObject go)
    {
        go = Object.Instantiate(go, Root.transform); // @UI를 부모로 삼고 Hierarchy창에 띄운다

        // Prefab을 instantiate하면 (Clone)글자가 이름 뒤에 붙는다. 이를 제거하기 위한 것
        string name = go.name;
        int index = name.LastIndexOf('(');
        if (index >= 0)
            go.name = name.Substring(0, index);

        // 혹시 오브젝트에 BaseUI.cs가 안붙어 있다면 붙여준다
        if (go.GetComponent<BaseUI>() == null)
            go.AddComponent<BaseUI>();

        return go;
    }

}
