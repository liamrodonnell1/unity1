using UnityEngine;

public class playerpush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    GameObject moveable_box;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);


        if (hit.collider != null && Input.GetKeyDown(KeyCode.E))
        {
            moveable_box = hit.collider.gameObject;
        }

    }
}
