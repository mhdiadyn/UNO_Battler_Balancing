using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Klik kiri pada musuh = beri damage. clickDamage 9 = DPS dasar pemain di Bagian A.
public class PlayerClicker : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float clickDamage = 9f;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    private void Update()
    {
        if (!ClickedThisFrame(out Vector2 screenPos)) return;

        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var enemy = hit.collider.GetComponentInParent<EnemyController>();
            if (enemy != null) enemy.TakeHit(clickDamage);
        }
    }

    private static bool ClickedThisFrame(out Vector2 pos)
    {
#if ENABLE_INPUT_SYSTEM
        var mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            pos = mouse.position.ReadValue();
            return true;
        }
#endif
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            return true;
        }
#endif
        pos = default;
        return false;
    }
}
