using UnityEngine;

public class CursorChangerScript : MonoBehaviour
{
    public static CursorChangerScript instance;

    [SerializeField] Texture2D mouseTexture;
    [SerializeField] bool isCursorActiveOnScene = false;
    public bool isCursorAtcive = false;
    public bool hideCursor = false;
    
    void Awake() { Cursor.visible = false; }

    private void Start() { instance = GetComponent<CursorChangerScript>(); }

    void OnGUI()
    {
        if (!hideCursor)
        {
            if (isCursorAtcive || isCursorActiveOnScene)
            {
                Vector2 mouse_pos = Event.current.mousePosition;
                GUI.depth = 0;
                GUI.Label(new Rect(mouse_pos.x - 0.5f, mouse_pos.y + 0.2f, mouseTexture.width, mouseTexture.height), mouseTexture);
            }
        }
    }

}
