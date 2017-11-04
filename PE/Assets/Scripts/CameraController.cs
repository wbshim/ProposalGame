using UnityEngine;
using UnityEngine.PostProcessing;
using System.Collections;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class CameraController : MonoBehaviour {

    PostProcessingProfile m_Profile;
    DepthOfFieldModel.Settings dof;

    Camera cameraMain;

    void Start()
    {
        cameraMain = Camera.main;
        m_Profile = cameraMain.GetComponent<PostProcessingBehaviour>().profile;
        dof = m_Profile.depthOfField.settings;
        dof.focalLength = 60f;
        m_Profile.depthOfField.settings = dof;
    }

    void Update()
    {
        //if(Input.GetKeyDown("space"))
        //{
        //    StartCoroutine(focusPicture(dof, 1f, 2f));
        //}
    }

    public void focusPicture()
    {
        StartCoroutine(focusPicture(dof, 1f, 2f));
    }


    private IEnumerator focusPicture(DepthOfFieldModel.Settings _dof, float targetFocalLength, float duration)
    {
        while(_dof.focalLength > 2f)
        {
            _dof.focalLength = Mathf.Lerp(_dof.focalLength, targetFocalLength, duration*Time.deltaTime);
            m_Profile.depthOfField.settings = _dof;
            yield return null;
        }

        _dof.focalLength = targetFocalLength;
        m_Profile.depthOfField.settings = _dof;
        print("Reached target.");

    }
    
}
