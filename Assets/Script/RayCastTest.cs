using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 0.5f;
    [SerializeField]
    private Vector2 raycastOffset = new Vector2(0.5f, -0.2f);
    [SerializeField]
    private LayerMask platformLayerMask;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + raycastOffset.x, rigid.position.y + raycastOffset.y);
        Debug.DrawRay(frontVec, Vector2.down * raycastDistance, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(frontVec, Vector2.down, raycastDistance, platformLayerMask);

        if (hit.collider != null)
        {
            Debug.Log("Raycast Hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Raycast Missed");
        }
    }
}