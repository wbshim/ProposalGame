using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender { Male, Female, Max };
public enum SkinComplexion { VeryLight, Light, Medium, Dark, VeryDark, Max };
public enum Pose { Sitting, Standing };
public class NPC_random : MonoBehaviour {

    public SkinComplexion skinComplexion;
    public Gender gender;
    public Pose pose;
    public Transform hairHolder;

    public Material skinComplexionMaterial;

	// Use this for initialization
	void Start () {
        NPC_creator NPCCreator = GameObject.Find("NPC_creator").GetComponent<NPC_creator>();
        GameObject hairToAssign;

        gender = (Gender)Random.Range(0, (int)Gender.Max);
        skinComplexion = (SkinComplexion)Random.Range(0, (int)SkinComplexion.Max);

        // Assign gender specific attributes
        if (gender == Gender.Male) /* If NPC is male */
        {
            // Assign hair
            hairToAssign = NPCCreator.hairTypesMale[Random.Range(0, 1)];
        }
        else /* If NPC is female */
        {
            // Assign hair
            hairToAssign = NPCCreator.hairTypesFemale[Random.Range(0, 1)];
        }
        GameObject hair = Instantiate(hairToAssign, hairHolder.transform.position, hairHolder.transform.rotation);
        // Assign hair color
        hair.GetComponent<Renderer>().material = NPCCreator.hairColors[Random.Range(0, 2)];
        // Change skin color
        GetComponent<Renderer>().material = NPCCreator.skinComplexions[(int)skinComplexion];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
