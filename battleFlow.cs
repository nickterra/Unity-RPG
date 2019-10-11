using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.Collections;
using System;

public class battleFlow : MonoBehaviour {

    public Transform attackDamage;
    public static string turn = "begin";
    public int enemySpawn = 1;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject newenemy;

    public GameObject swipe;
    public GameObject sword_swipe;
    public GameObject invHUD;
    public static string InvSlot1 = "Empty";
    public static string InvSlot2 = "Empty";
    public static string InvSlot3 = "Empty";

    public static string className;
    public static string playerName = "Nick";
    public static float playerHealth = 100;
    public static float playerMagic  = 50;
    public static float playerAttack;
    public static float playerMana;
    public static float playerDefend = 10;

    public static string enemyName;
    public static float enemyHealth = 1;
    public static float enemyAttack;
    public static float payout;
    public static float enemyDefend;
    public static string enemyItem = "";

    public static float InvSlot1Total;
    public static float InvSlot2Total;
    public static float InvSlot3Total;

    public static float DOTDur;
    public static float DOTDam;

    public Button fightButton;
    public Button itemButton;
    public Button magicButton;
    public Button runButton;

    private bool itIsTrue = false;
    private float startWait = 3f;
    private KeyCode invToggle;

    // Use this for initialization
    void Start()
    {
        Debug.Log(itIsTrue);
 
        Button fight = fightButton.GetComponent<Button>();
        Button item  = itemButton.GetComponent<Button>();
        Button magic = magicButton.GetComponent<Button>();
        Button run   = runButton.GetComponent<Button>();

        fight.onClick.AddListener(OnClickFight);
        item.onClick.AddListener(OnClickItem);
        magic.onClick.AddListener(OnClickMagic);
        run.onClick.AddListener(OnClickRunaway);
        magic.interactable = true;
        
        enemySpawn = UnityEngine.Random.Range(1, 4);
        if (enemySpawn == 1)
        { 
            newenemy = Instantiate(enemy1, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newenemy.SetActive(true);   
        }
        if (enemySpawn == 2)
        { 
            newenemy = Instantiate(enemy2, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newenemy.SetActive(true);
        }
        if (enemySpawn == 3)
        {
            newenemy = Instantiate(enemy3, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newenemy.SetActive(true);
        }
    }

    void OnClickFight()
    {        
        battleFlow.playerAttack = 10;
        battleFlow.turn = "neither";
        swipe.SetActive(true);
    }

    void OnClickMagic()
    {
        battleFlow.playerAttack = 15;
        battleFlow.playerMagic -= 5;
        battleFlow.turn = "neither";

        if (playerMagic <= 0)
        {
            magicButton.GetComponent<Button>().interactable = false;
        }

    }

    void OnClickRunaway()
    {
        battleFlow.turn = "runaway";
    }

    void OnClickItem()
    {
        if (invHUD.activeSelf)
        {
            invHUD.SetActive(false);
        }
        else
        { 
            invHUD.SetActive(true);

            if (gameObject.name == "Potion")
                GetComponent<TextMesh>().text = battleFlow.InvSlot1 + ": " + battleFlow.InvSlot1Total;
            
            if (gameObject.name == "Ether")
            {
                GetComponent<TextMesh>().text = battleFlow.InvSlot2 + ": " + battleFlow.InvSlot2Total;
            }
            if (gameObject.name == "Elixer")
            {
                GetComponent<TextMesh>().text = battleFlow.InvSlot3 + ": " + battleFlow.InvSlot3Total;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {

        if (battleFlow.turn == "begin")
        {
            damageControl.hasFainted = false;
            StartCoroutine(Wait());
            fightButton.GetComponent<Button>().interactable = true;
            itemButton.GetComponent<Button>().interactable = true;
            magicButton.GetComponent<Button>().interactable = true;
            runButton.GetComponent<Button>().interactable = true;
        }

        if (battleFlow.turn == "neither")
        {
            battleFlow.enemyHealth -= battleFlow.playerAttack;


            if (battleFlow.DOTDur>=1)
            {
                battleFlow.DOTDur -= 1;
                battleFlow.enemyHealth -=battleFlow.DOTDam; 
            }

            Instantiate(attackDamage, new Vector3(0, 0, 0), attackDamage.rotation);
            battleFlow.turn = "enemy";
        }

        if (battleFlow.turn == "enemy")
        {
            battleFlow.playerHealth += battleFlow.playerDefend - battleFlow.enemyAttack;
            Debug.Log(battleFlow.enemyHealth + " " + battleFlow.playerHealth);
            battleFlow.turn = "hero";
        }

        if (battleFlow.turn == "battleEnd")
        {
            battleFlow.turn = "exitBattle";

            if (battleFlow.InvSlot1 == battleFlow.enemyItem)
                battleFlow.InvSlot1Total += 1;
            else
            if (battleFlow.InvSlot2 == battleFlow.enemyItem)
                battleFlow.InvSlot2Total += 1;
            else
            if (battleFlow.InvSlot3 == battleFlow.enemyItem)
                battleFlow.InvSlot3Total += 1;


            if (battleFlow.InvSlot1 == "Empty")
            {
                battleFlow.InvSlot1 = battleFlow.enemyItem;
                battleFlow.InvSlot1Total += 1;
            }
            if (battleFlow.InvSlot2 == "Empty")
            {
                battleFlow.InvSlot2 = battleFlow.enemyItem;
                battleFlow.InvSlot2Total += 1;
            }
            if (battleFlow.InvSlot3 == "Empty")
            {
                battleFlow.InvSlot3 = battleFlow.enemyItem;
                battleFlow.InvSlot3Total += 1;
            }

            /*
            if (gameObject.name == "inv1")
            {
                GetComponent<TextMesh>().text = battleFlow.InvSlot1 + ": " + battleFlow.InvSlot1Total;
            }
            if (gameObject.name == "inv2")
            {
                GetComponent<TextMesh>().text = battleFlow.InvSlot2 + ": " + battleFlow.InvSlot2Total;
            }
            if (gameObject.name == "inv1")
            {
                GetComponent<TextMesh>().text = battleFlow.InvSlot3 + ": " + battleFlow.InvSlot3Total;
            }*/

        }

        if (damageControl.hasFainted == true)
        {
            fightButton.GetComponent<Button>().interactable = false;
            itemButton.GetComponent<Button>().interactable = false;
            magicButton.GetComponent<Button>().interactable = false;
            runButton.GetComponent<Button>().interactable = false;
        }
    }



    private void system(string v)
    {
        throw new NotImplementedException();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(startWait);
        // execute the rest of your script
        battleFlow.turn = "hero";
    }

}