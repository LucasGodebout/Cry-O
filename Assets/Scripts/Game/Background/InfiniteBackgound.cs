using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform target;
    public Transform[] backgrounds;
    public float backgroundHeight = 10f; 

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);

        if (transform.position.y > backgrounds[1].position.y + (backgroundHeight / 2))
        {
            ScrollUp();
        }
        else if (transform.position.y < backgrounds[1].position.y - (backgroundHeight / 2))
        {
            ScrollDown();
        }
    }

    private void ScrollUp()
    {
    
        backgrounds[0].position = new Vector3(backgrounds[0].position.x, backgrounds[2].position.y + backgroundHeight, backgrounds[0].position.z);

        Transform bottom = backgrounds[0];
        backgrounds[0] = backgrounds[1];
        backgrounds[1] = backgrounds[2];
        backgrounds[2] = bottom;
    }

    private void ScrollDown()
    {
        
        backgrounds[2].position = new Vector3(backgrounds[2].position.x, backgrounds[0].position.y - backgroundHeight, backgrounds[2].position.z);

        Transform top = backgrounds[2];
        backgrounds[2] = backgrounds[1];
        backgrounds[1] = backgrounds[0];
        backgrounds[0] = top;
    }
}