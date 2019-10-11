using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class damageControl : MonoBehaviour
{
    public static bool hasFainted = false;
    public static bool youFainted = false;
    private float endWait = 3f;


    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().sortingOrder = 10;
    }

    void OnClickFight()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "EnemyText")
            GetComponent<TextMesh>().text = battleFlow.enemyName + "\n" + battleFlow.enemyHealth;
        if (gameObject.name == "PlayerText")
            GetComponent<TextMesh>().text = battleFlow.playerName + "\n" + battleFlow.playerHealth + "\n" + battleFlow.playerMagic;

        if (gameObject.name == "BattleText")
            GetComponent<TextMesh>().text = battleFlow.playerName + " did " + battleFlow.playerAttack + " damage";

        if (battleFlow.turn == "runaway")
        {
            if (gameObject.name == "BattleText")
            {
                GetComponent<TextMesh>().text = "You Escaped";
                SceneManager.LoadScene("overworld");
            }
        }

        if (battleFlow.turn == "begin")
        {
            if (gameObject.name == "BattleText")
            {
                GetComponent<TextMesh>().text = "Wild " + battleFlow.enemyName + " Appeared!";
            }
        }

        if (gameObject.name == "DamageText(Clone)")
        {
            GetComponent<TextMesh>().text = "" + battleFlow.playerAttack.ToString();
        }

        if (gameObject.name == "Potion")
        {
            GetComponent<TextMesh>().text = "Potion: " + battleFlow.InvSlot1Total;
        }
        if (gameObject.name == "Ether")
        {
            GetComponent<TextMesh>().text = "Ether: " + battleFlow.InvSlot1Total;
        }
        if (gameObject.name == "Elixer")
        {
            GetComponent<TextMesh>().text = "Elixer: " + battleFlow.InvSlot1Total;
        }


        if (battleFlow.playerHealth <= 0)
        {
            if (gameObject.name == "BattleText")
            {
                GetComponent<TextMesh>().text = "Game Over";
                youFainted = true;
            }
        }


        if (battleFlow.enemyHealth <= 0)
        {
            if (gameObject.name == "BattleText")
            {
                GetComponent<TextMesh>().text = "Enemy Fainted";
                hasFainted = true;
                StartCoroutine(Waiting());
                
            }
        }
    }


    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(endWait);
        GetComponent<TextMesh>().text = battleFlow.playerName + " gained " + battleFlow.payout + "EXP!";
        yield return new WaitForSeconds(endWait);
        SceneManager.LoadScene("overworld");
    }

}
