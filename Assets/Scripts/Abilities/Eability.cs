using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Yeni_E_Yeteneði", menuName = "Abilities/E_Skill")]
public class Eability : AbilityData
{
    public override void Use(GameObject player)
    {
        Animator anim = player.GetComponent<Animator>();

        if (anim != null)
        {

            anim.SetTrigger("SpAttack");
        }
    }
    


}
