using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{

    private float speed = 0f;
    public Vector3 suunta = new Vector3(0, 1, 0);

    public void RotateFan()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(suunta * speed * Time.deltaTime);
    }
}
