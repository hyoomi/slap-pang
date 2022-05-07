using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    int _order = 10;

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

    static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

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

    public GameObject LoadUI<T>(string name = null) where T : BaseUI
    {
        string path = name;

        if (string.IsNullOrEmpty(name))
            path = typeof(T).Name;

        if(typeof(T) == typeof(PopupUI))
            path = $"Popup/{name}";

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{path}");
        if (go == null)
        {
            Debug.Log($"Failed to load prefab : Prefabs/UI/{path}");
            return null;
        }
        
        InstantiateUI(go);
        return go;
    }

    public GameObject InstantiateUI(GameObject go)
    {
        go = Object.Instantiate(go, Root.transform);

        // Prefab을 instantiate하면 (Clone)글자가 이름 뒤에 붙는다. 이를 제거하기 위한 것
        string name = go.name;
        int index = name.LastIndexOf('(');
        if (index >= 0)
            go.name = name.Substring(0, index);

        return go;
    }

}
