using UnityEngine;

public class CursorChangerScript : MonoBehaviour
{
    public static CursorChangerScript instance;

    [SerializeField] Texture2D mouseTexture;
    [SerializeField] bool isCursorActiveOnScene = false;
    public bool isCursorAtcive = false;
    
    void Awake() 
    {
        instance = GetComponent<CursorChangerScript>();
        Cursor.visible = false; 
    }

    void OnGUI()
    {    
        if (isCursorAtcive || isCursorActiveOnScene)
        {
            Vector2 mouse_pos = Event.current.mousePosition;
            GUI.depth = 0;
            GUI.Label(new Rect(mouse_pos.x - 0.5f, mouse_pos.y + 0.2f, mouseTexture.width, mouseTexture.height), mouseTexture);
        }       
    }

}
