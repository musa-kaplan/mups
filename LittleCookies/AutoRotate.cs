using MusaUtils;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    private enum AllAxis
    {
        Xcw,
        Xr,
        Ycw,
        Yr,
        Zcw,
        Zr
    }

    [SerializeField] private AllAxis _axis;
    [SerializeField] private Space _space;
    [SerializeField] private float _speed;
    private Vector3 _dir;
    
    private void Start()
    {
        switch (_axis)
        {
            case AllAxis.Xcw:
                _dir = Vector3.right;
                break;
            case AllAxis.Xr:
                _dir = Vector3.left;
                break;
            case AllAxis.Ycw:
                _dir = Vector3.up;
                break;
            case AllAxis.Yr:
                _dir = Vector3.down;
                break;
            case AllAxis.Zcw:
                _dir = Vector3.forward;
                break;
            case AllAxis.Zr:
                _dir = Vector3.back;
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(_dir, _speed * Time.deltaTime, _space);
    }
}
