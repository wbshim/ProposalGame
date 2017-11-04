using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillsController : MonoBehaviour {

    SkiGameManager gameManager;
    ObjectPooler objPooler;
    public float HillGenerateTime; // Generate hill every x second
    float hillGenerateTimeScaled; // Hill generation time scaled to speed
    float speed; // Speed from game manager
    public Transform StartPoint;
    bool generating;
	// Use this for initialization

	void Start () {
        generating = false;
        objPooler = GetComponent<ObjectPooler>();
        gameManager = GameObject.Find("GameManager").GetComponent<SkiGameManager>();
        speed = gameManager.Speed;
        hillGenerateTimeScaled = HillGenerateTime * (25 / speed);
        StartCoroutine(GenerateHills());
    }
    private void FixedUpdate()
    {
        speed = gameManager.Speed;
        hillGenerateTimeScaled = HillGenerateTime * (25 / speed);
    }
    void GenerateHill()
    {
        GameObject hill = objPooler.GetPooledObject();
        if (hill == null) return;
        hill.transform.position = StartPoint.position;
        hill.transform.rotation = Quaternion.Euler(-90, 90 * Random.Range(0,4), 0);
        hill.SetActive(true);
    }
    IEnumerator GenerateHills()
    {
        yield return new WaitForSeconds(hillGenerateTimeScaled);
        while(speed > 0)
        {
            while(generating)
            {
                GenerateHill();
                yield return new WaitForSeconds(hillGenerateTimeScaled);
            }
            yield return null;
        }
    }
    public void SetGenerating(bool _generating)
    {
        generating = _generating;
    }
}
