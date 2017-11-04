using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestroy : MonoBehaviour {

    SkiGameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<SkiGameManager>();
        //Invoke("Destroy", 80/gameManager.Speed);
        StartCoroutine(_Destroy());
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    public void CancelDestroy()
    {
        CancelInvoke();
    }
    IEnumerator _Destroy()
    {
        while(transform.position.z < 50)
        {
            yield return null;
        }
        //GameObject.Find("HillManager").GetComponent<HillsController>().GenerateHill();
        Destroy();
    }
}
