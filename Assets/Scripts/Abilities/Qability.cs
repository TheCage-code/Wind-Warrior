using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Yeni_Q_Yeteneði", menuName = "Abilities/Q_Skill")]
public class Qability : AbilityData
{
    public override void Use(GameObject player)
    {
        Animator anim = player.GetComponent<Animator>();
       
        if (anim != null)
        {
            
            anim.SetTrigger("SpAttack2");
        }
    }

}
