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
                Debug.LogError($"MikoRightHand,PaimonPrefeb ���� null �Դϴ�");
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
        Debug.Log($" PickUpPaimon ���� ");
        _PaimonPrefeb.transform.SetParent(_MikoRightHand.GetComponent<Transform>().transform);
    }

    public void PutPaimon()
    {
        Debug.Log($" PutPaimon ���� ");
        _PaimonPrefeb.transform.SetParent(null);
    }
}
