using Unity.VisualScripting;
using UnityEngine;


public class AbilityData : ScriptableObject
{
    
    public string abilityName;
    public float damage = 25f;
    public float cooldown;
    

    
    

    public virtual void Use(GameObject player)
    {
       

    }

}
