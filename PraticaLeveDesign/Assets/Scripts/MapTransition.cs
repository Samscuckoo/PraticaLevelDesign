using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;
    [SerializeField] private Direction direction;
    [SerializeField] private float additivePos = 2.0f;

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPosition = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPosition.y += additivePos;
                break;
            case Direction.Down:
                newPosition.y -= additivePos;
                break;
            case Direction.Left:
                newPosition.x -= additivePos;
                break;
            case Direction.Right:
                newPosition.x += additivePos;
                break;
        }    
        player.transform.position = newPosition;
    }

}

