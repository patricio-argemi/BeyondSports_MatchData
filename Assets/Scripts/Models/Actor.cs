using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Material Referee;
    [SerializeField]
    private Material TeamBlue;
    [SerializeField]
    private Material TeamRed;

    public int Team { get; set; }
    public int TrackingId { get; set; }
    public int Number { get; set; }
    public float XPosition { get; set; }    
    public float ZPosition { get; set; }
    public float Speed { get; set; }
    public Movement Movement { get; set; }    

    public void AssignRole(int role)
    {
        Material actorSkin = GetRoleSkin((RoleType)role);
        gameObject.GetComponent<Renderer>().material = actorSkin;
    }

    public Material GetRoleSkin(RoleType role)
    {
        switch (role)
        {
            case RoleType.Referee: return Referee;
            case RoleType.TeamBlue: return TeamBlue;
            case RoleType.TeamRed: return TeamRed;
            default:
                return Referee;
                //throw new Exception("Role not supported/configured.");
        }
    }

    public void AssignNumber(string assignedNumber)
    {
        TextMesh numberComponent = gameObject.GetComponentInChildren<TextMesh>();
        numberComponent.text = assignedNumber;
    }
}
