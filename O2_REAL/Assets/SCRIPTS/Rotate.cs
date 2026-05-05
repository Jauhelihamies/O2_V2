using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float nopeus = 100f;
    public int s1 = 0;
    public int s2 = 0;
    public int s3 = 0;


    public Vector3 suunta = new Vector3(); 

    void Update()
    {

        transform.Rotate(suunta * nopeus * Time.deltaTime);
    }
}
