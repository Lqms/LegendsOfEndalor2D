using UnityEngine;

public class CursorChangerScript : MonoBehaviour
{
    public static CursorChangerScript Instance;

    [SerializeField] private Texture2D _mouseTexture;
    [SerializeField] private bool _isCursorActiveOnScene = false;

    public bool isCursorAtcive = false;
    
    void Awake() 
    {
        Instance = GetComponent<CursorChangerScript>();
        Cursor.visible = false; 
    }

    void OnGUI()
    {    
        if (isCursorAtcive || _isCursorActiveOnScene)
        {
            Vector2 mouse_pos = Event.current.mousePosition;
            GUI.depth = 0;
            GUI.Label(new Rect(mouse_pos.x - 0.5f, mouse_pos.y + 0.2f, _mouseTexture.width, _mouseTexture.height), _mouseTexture);
        }       
    }

}
