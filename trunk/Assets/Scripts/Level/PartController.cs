using System;
using UnityEngine;

public class PartController : ScrollablePart
{

    void Start()
    {
        var tr = transform.FindChild("BG");
        if (tr != null) tr.gameObject.SetActive(false);
    }

}

   
