using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JuicyCursor : MonoBehaviour
{
    [Header("Setup")]
    public RectTransform cursorTransform;
    public RectTransform parentCanvas;

    [Header("Size & Rotation")]
    public float cursorScale = 1.0f;
    public float rotationSpeed = 15f;
    public float rotationOffset = -90f;

    [Header("Juice")]
    public float stretchAmount = 0.5f;
    public float squashSpeed = 10f;

    [Header("Click Settings")]
    [Tooltip("Makes it easier to click NPCs. Higher = bigger click radius.")]
    public float clickForgiveness = 0.5f;

    private Vector2 lastMousePos;

    void Start()
    {
        Cursor.visible = false;
        if (parentCanvas == null)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null) parentCanvas = canvas.GetComponent<RectTransform>();
        }
        if (Mouse.current != null) lastMousePos = Mouse.current.position.ReadValue();
    }

    void Update()
    {
        if (parentCanvas == null || cursorTransform == null || Mouse.current == null) return;

        Vector2 currentMousePos = Mouse.current.position.ReadValue();

        // 1. Position Follow
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas, currentMousePos, null, out Vector2 localPos);
        cursorTransform.anchoredPosition = localPos;

        // 2. Click Detection
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            DetectNPCClick(currentMousePos);
        }

        // 3. Juice Logic
        Vector2 movementDelta = currentMousePos - lastMousePos;
        float speed = movementDelta.magnitude;

        if (speed > 0.1f)
        {
            float angle = Mathf.Atan2(movementDelta.y, movementDelta.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle + rotationOffset);
            cursorTransform.rotation = Quaternion.Lerp(cursorTransform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }

        float stretch = 1f + (speed * stretchAmount * 0.1f);
        float squash = 1f / stretch;
        Vector3 targetScale = new Vector3(squash * cursorScale, stretch * cursorScale, 1f);
        cursorTransform.localScale = Vector3.Lerp(cursorTransform.localScale, targetScale, Time.deltaTime * squashSpeed);

        lastMousePos = currentMousePos;
    }

    void DetectNPCClick(Vector2 screenPos)
    {
        // Convert screen to world
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(Camera.main.transform.position.z)));

        // Instead of a single point, we check a CIRCLE area (clickForgiveness)
        RaycastHit2D hit = Physics2D.CircleCast(worldPos, clickForgiveness, Vector2.zero);

        if (hit.collider != null)
        {
            BouncyNPC npc = hit.collider.GetComponentInParent<BouncyNPC>();
            if (npc != null)
            {
                npc.HandleClick();
            }
        }
    }
}