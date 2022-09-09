using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuHighlighter : MonoBehaviour
{
    public void ShowAt(Transform target)
    {
        transform.position = target.position;
    }
}
