using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaimonAni : MonoBehaviour
{
    private GameObject _MikoRightHand;
    private GameObject _PaimonPrefeb;


    private void Awake()
    {
        if(_MikoRightHand == null || _PaimonPrefeb == null)
        {
            _MikoRightHand = GameObject.FindGameObjectWithTag("Hand");
            _PaimonPrefeb = GameObject.FindGameObjectWithTag("Paimon");
            if (_PaimonPrefeb == null || _MikoRightHand == null)
            {
                Debug.LogError($"MikoRightHand,PaimonPrefeb 값이 null 입니다");
            }
        }

      
    }
    private void Start()
    {
        Debug.Log(_MikoRightHand);
        Debug.Log(_PaimonPrefeb);
    }

    public void PickUpPaimon()
    {
        Debug.Log($" PickUpPaimon 실행 ");
        _PaimonPrefeb.transform.SetParent(_MikoRightHand.GetComponent<Transform>().transform);
    }

    public void PutPaimon()
    {
        Debug.Log($" PutPaimon 실행 ");
        _PaimonPrefeb.transform.SetParent(null);
    }
}
