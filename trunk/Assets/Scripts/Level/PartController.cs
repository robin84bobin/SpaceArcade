using System;
using UnityEngine;

public class PartController : ScrollablePart
{

    void Start()
    {
        var tr = transform.Find("BG");
        if (tr != null) tr.gameObject.SetActive(false);
    }

}

   
