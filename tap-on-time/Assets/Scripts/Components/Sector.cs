using UnityEngine;

public class Sector : MonoBehaviour
{
    private bool _move;
    
    private void Update()
    {
        if (_move)
        {
            transform.RotateAround( Vector3.zero, Vector3.back, 30 * Time.deltaTime);   
        }
    }

    public bool Move
    {
        set => _move = value;
    }
}