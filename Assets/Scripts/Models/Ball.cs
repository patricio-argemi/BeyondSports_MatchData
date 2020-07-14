using UnityEngine;

public class Ball : MonoBehaviour
{
    public float XPosition { get; set; }
    public float YPosition { get; set; }
    public float ZPosition { get; set; }    
    public float Speed { get; set; }
    public string[] ClickerFlags { get; set; }
    public Movement Movement { get; set; }
}
