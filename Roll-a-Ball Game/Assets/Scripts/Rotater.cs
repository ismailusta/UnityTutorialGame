using UnityEngine;

public class Rotater : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); // Rotate the object around its local axes
    }
}
