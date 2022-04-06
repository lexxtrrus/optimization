using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    Vector3 velocity;
    float sides = 30.0f;
    float speedMax = 0.3f;
    private static readonly int _Color = Shader.PropertyToID("_Color");
    private Material _material;
    private Color _color;
    private Vector3 pos = new Vector3();

    void Start()
    {
        _material = GetComponent<Renderer>().material;
        
        velocity = new Vector3(Random.Range(0.0f, speedMax),
                        Random.Range(0.0f, speedMax),
                        Random.Range(0.0f, speedMax));

    }

    Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    void Update()
    {
        transform.Translate(velocity);

        if (transform.position.x > sides || transform.position.x < -sides)
        {
            velocity.x *= -1f;
        }
        if (transform.position.y > sides || transform.position.y < -sides)
        {
            velocity.y *= -1f;
        }
        if (transform.position.z > sides || transform.position.z < -sides)
        {
            velocity.z *= -1f;
        }

        pos = transform.position / sides;
        _color.r = pos.x;
        _color.g = pos.y;
        _color.b = pos.z;
        _material.SetColor(_Color, _color);

    }
}
