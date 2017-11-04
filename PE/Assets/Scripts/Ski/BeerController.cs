using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerController : MonoBehaviour {

    SkiGameManager gameManager;
    ObjectPooler objPooler;
    public float TreeGenerateTime; // Generate hill every x second
    float treeGenerateTimeScaled; // Hill generation time scaled to speed
    float speed; // Speed from game manager
    public Transform StartPoint;
    public float MinX;
    public float MaxX;
    bool generating;

    // Use this for initialization
    void Start () {
        generating = false;
        objPooler = GetComponent<ObjectPooler>();
        gameManager = GameObject.Find("GameManager").GetComponent<SkiGameManager>();
        speed = gameManager.Speed;
        treeGenerateTimeScaled = TreeGenerateTime * (4 / speed);
        StartCoroutine(GenerateTrees());
    }

    private void FixedUpdate()
    {
        speed = gameManager.Speed;
        treeGenerateTimeScaled = TreeGenerateTime * (4 / speed);
    }
    void GenerateTree()
    {
        GameObject tree = objPooler.GetPooledObject();
        if (tree == null) return;
        float yOffset = tree.GetComponent<ObjController>().YOffset;
        tree.transform.position = new Vector3(Random.Range(MinX, MaxX), StartPoint.position.y + yOffset, StartPoint.position.z);
        //tree.transform.rotation = Quaternion.Euler(30, 0, 0);
        tree.SetActive(true);
    }
    IEnumerator GenerateTrees()
    {
        yield return new WaitForSeconds(treeGenerateTimeScaled);
        while (true)
        {
            while(generating)
            {

                GenerateTree();
                yield return new WaitForSeconds(treeGenerateTimeScaled);
            }
            yield return null;
        }

    }
    public void SetGenerating(bool _generating)
    {
        generating = _generating;
    }
}
