using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    private Vector2 _direction;

    [SerializeField] private List<Transform> _openPoints = new List<Transform>();

    public IEnumerable<Transform> OpenPoints => _openPoints;

    public PathTileType PathTileType= PathTileType.None;

    [SerializeField]
    private GameObject _head;

    private Color _color;

    public float Size
    {
        get => transform.localScale.x;
        set => transform.localScale = value * Vector3.one;
    }

    public Color Color
    {
        set
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.color = value;
            }

            _color = value;
        }

        get
        {
            return _color;
        }
    }

    public Vector2 Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, value));
        }
    }

    public void EnableHead()
    {
        _head.SetActive(true);
        _head.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, _direction));
        _head.GetComponent<SpriteRenderer>().color = Color;
    }

    public void DisableHead()
    {
        _head.SetActive(false);
    }
}

public enum PathTileType
{
    None,
    Curve,
    HalfEndCapTile,
    HalfStraightTile,
    Head,
    EndCapTile,
    StraightTile,
    Join
}