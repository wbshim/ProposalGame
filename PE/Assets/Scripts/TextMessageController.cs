using UnityEngine;

public class TextMessageController : MonoBehaviour {

    public Transform target;
    public RectTransform canvas;
    public float offsetX;
    public float offsetY;

    float width;
    float height;
    

    void Start()
    {
        width = canvas.rect.width;
        height = canvas.rect.height;
    }

	// Update is called once per frame
	void Update () {
        Vector2 targetPosition = (Camera.main.WorldToViewportPoint(target.position) - new Vector3(0.5f,0.5f));
        transform.localPosition = new Vector2(targetPosition.x * width + offsetX, targetPosition.y * height + offsetY);
    }
}
