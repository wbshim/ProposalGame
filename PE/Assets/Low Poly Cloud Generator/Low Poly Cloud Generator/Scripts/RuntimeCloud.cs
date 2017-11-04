using UnityEngine;
using LowPolyCloudGenerator;

public class RuntimeCloud : MonoBehaviour
{
    //Gets a new cloud at runtime using the component's settings
	void Start ()
    {
        CloudGenerator gen = FindObjectOfType<CloudGenerator>();

        GetComponent<MeshFilter>().sharedMesh = gen.RuntimeCloud();
	}	
}
