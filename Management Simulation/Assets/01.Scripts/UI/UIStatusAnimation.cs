using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusAnimation : MonoBehaviour
{
    private GameObject _panelAnimationTransform;
    private bool _isUiMove = false;

    private void Awake()
    {
        _panelAnimationTransform = transform.Find("Status/Status - Point").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (_isUiMove)
            {
                case true:
                    _panelAnimationTransform.GetComponentInChildren<Animator>().Play("UIReturn");
                    _isUiMove = false;
                    break;
                case false:
                    _panelAnimationTransform.GetComponentInChildren<Animator>().Play("UIMove");
                    _isUiMove = true;
                    break;
            }
        }
    }
}
