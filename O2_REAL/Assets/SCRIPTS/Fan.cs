using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{

    private float speed = 0f;
    public Vector3 suunta = new Vector3(0, 1, 0);
    int SpeedVar;

    public void Start()
    {
        SpeedVar = Random.Range(-4, 4);
        Debug.Log(SpeedVar);
    }
    public void RotateFan()
    {
        speed += 30f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(suunta * speed * Time.deltaTime * SpeedVar);
    }
}
