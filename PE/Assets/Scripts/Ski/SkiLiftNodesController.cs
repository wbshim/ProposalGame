using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiLiftNodesController : MonoBehaviour {
    SkiGameManager gameManager;
    ObjectPooler objPooler;
    public float GenerateTime; // Generate hill every x second
    public Transform StartPoint;
    float generateTimeScaled; // Hill generation time scaled to speed
    float speed; // Speed from game manager
    bool generating;

    // Use this for initialization
    void Start () {
        generating = false;
        objPooler = GetComponent<ObjectPooler>();
        gameManager = GameObject.Find("GameManager").GetComponent<SkiGameManager>();
        speed = gameManager.Speed;
        generateTimeScaled = GenerateTime * (4 / speed);
        StartCoroutine(GenerateNodes());
    }

    private void FixedUpdate()
    {
        speed = gameManager.Speed;
        generateTimeScaled = GenerateTime * (4 / speed);
    }
    void GenerateNode()
    {
        GameObject node = objPooler.GetPooledObject();
        if (node == null) return;
        float yOffset = node.GetComponent<ObjController>().YOffset;
        node.transform.position = new Vector3(15, StartPoint.position.y + yOffset, StartPoint.position.z);
        //tree.transform.rotation = Quaternion.Euler(30, 0, 0);
        node.SetActive(true);
    }
    IEnumerator GenerateNodes()
    {
        yield return new WaitForSeconds(generateTimeScaled);
        while (true)
        {
            while (generating)
            {

                GenerateNode();
                yield return new WaitForSeconds(generateTimeScaled);
            }
            yield return null;
        }

    }
    public void SetGenerating(bool _generating)
    {
        generating = _generating;
    }
}
