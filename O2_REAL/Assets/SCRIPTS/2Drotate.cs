using UnityEngine;

public class OptimizedRotate2D : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private bool useRandomSpeed = false;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float minRotationSpeed = 50f;
    [SerializeField] private float maxRotationSpeed = 200f;

    [Header("Direction Settings")]
    [SerializeField] private bool clockwise = true;
    [SerializeField] private bool isRotating = true;

    private float currentDirection = -1f;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;


        if (useRandomSpeed)
        {
            rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        }

        UpdateDirectionCached();
        enabled = isRotating;
    }

    void Update()
    {
        myTransform.localEulerAngles += new Vector3(0, 0, rotationSpeed * currentDirection * Time.deltaTime);
    }

    public bool IsRotating
    {
        get => isRotating;
        set
        {
            isRotating = value;
            enabled = value;
        }
    }

    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = Mathf.Max(0f, value);
    }

    public bool Clockwise
    {
        get => clockwise;
        set
        {
            clockwise = value;
            UpdateDirectionCached();
        }
    }

    public void ChangeDirection()
    {
        Clockwise = !clockwise;
    }

    private void UpdateDirectionCached()
    {
        currentDirection = clockwise ? -1f : 1f;
    }

    private void OnValidate()
    {
        UpdateDirectionCached();
        enabled = isRotating;
    }
}