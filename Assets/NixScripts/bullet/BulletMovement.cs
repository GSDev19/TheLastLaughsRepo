using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed = 10f;
    [SerializeField] private int dañoDeLaBala;
    [SerializeField] Vector2 velocity;
    [SerializeField] Vector2 direction = new Vector2(0, 1);
    [SerializeField] int timeDestroySelf;

    private void Start()
    {
        Destroy(gameObject, timeDestroySelf);
    }

    private void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (CollisionCheck.CheckIfBlocker(collision.gameObject) || CollisionCheck.CheckIfEnemy(collision.gameObject) || CollisionCheck.CheckIfNPC(collision.gameObject))
            {
                ObjectBehavior obj = collision.GetComponent<ObjectBehavior>();
                Destroy(gameObject);

                if(CollisionCheck.CheckIfNPC(collision.gameObject))
                {
                    Vector3 pos = obj.transform.position;
                    Destroy(obj.gameObject);
                    SpawnController.Instance.InstantiateClown(pos);
                }
            }
        }
    }
}
