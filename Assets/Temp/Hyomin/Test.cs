using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject go;

    public void SetTure()
    {
        go.SetActive(true);
    }
    public void SetFalse()
    {
        go.SetActive(false);
    }
}
