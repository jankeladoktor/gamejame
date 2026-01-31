using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkriptaBorbe : MonoBehaviour
{
    public enum BattleState { START, CHOOSE_TO_FIGHT, PLAYERTURN, ENEMYTURN, WON, LOST }
    public BattleState state;

    public Svetlan svetlan;
    public DarkecBoss darkec;

    public HUDborba playerHUD;
    public HUDborba enemyHUD;

    public GameObject fightChoicePanel;  
    public GameObject actionPanel;       
    public Text dialogueText;

    private void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        if (svetlan == null) svetlan = FindObjectOfType<Svetlan>();
        if (darkec == null) darkec = FindObjectOfType<DarkecBoss>();

        playerHUD.SetHUD(svetlan.imeSvetlana, svetlan.nivo, svetlan.trenutniHP, svetlan.HPmax);
        enemyHUD.SetHUD(darkec.imeDarkeca, darkec.nivo, darkec.trenutniHP, darkec.HPmax);

        dialogueText.text = "Darkec je spreman za kec na kec u matiènu!";

        fightChoicePanel.SetActive(true);   
        actionPanel.SetActive(false);       

        state = BattleState.CHOOSE_TO_FIGHT;
    }

    public void OnStartFight()
    {
        if (state != BattleState.CHOOSE_TO_FIGHT) return;

        fightChoicePanel.SetActive(false);  
        actionPanel.SetActive(true);        

        dialogueText.text = "Odaberi kak buš klepil Darkeca!";
        state = BattleState.PLAYERTURN;
    }

    public void OnNormalAttack()
    {
        if (state != BattleState.PLAYERTURN) return;
        StartCoroutine(PlayerAttackCoroutine(isHeavy: false));
    }

    public void OnHeavyAttack()
    {
        if (state != BattleState.PLAYERTURN) return;
        StartCoroutine(PlayerAttackCoroutine(isHeavy: true));
    }

    IEnumerator PlayerAttackCoroutine(bool isHeavy)
    {
        actionPanel.SetActive(false);

        dialogueText.text = isHeavy ? "Svetlan koristi NAPAD AUROM!" : "Svetlan koristi mekanu šapici kako bi ti nanio štetu!";
        yield return new WaitForSeconds(0.8f);

        float hitChance = isHeavy ? 0.7f : 0.95f;
        bool hit = Random.value <= hitChance;

        if (!hit)
        {
            dialogueText.text = isHeavy ? "NAPAD AUROM je promašen!" : "Mekana šapica je ipak premekana da bi nanjela štetu Darkecu!";
            yield return new WaitForSeconds(1f);
        }
        else
        {
            int dmg = CalculateDamage(attackerAttack: svetlan.napad, isHeavy: isHeavy);
            darkec.PrimiStetu(dmg);
            enemyHUD.SetHP(darkec.trenutniHP, darkec.HPmax);
            dialogueText.text = $"Darkec prima {dmg} štete!";
            yield return new WaitForSeconds(1f);

            if (darkec.JeHmrl())
            {
                state = BattleState.WON;
                EndBattle();
                yield break;
            }
        }

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurnCoroutine());
    }

    IEnumerator EnemyTurnCoroutine()
    {
        dialogueText.text = "Darkec grunta kak da te uspava...";
        yield return new WaitForSeconds(1.2f);

        bool heavy = Random.value < 0.3f;
        float hitChance = heavy ? 0.75f : 0.95f;
        bool hit = Random.value <= hitChance;

        if (!hit)
        {
            dialogueText.text = "Darkec je promašio!";
            yield return new WaitForSeconds(1f);
        }
        else
        {
            dialogueText.text = heavy ? "Darkec koristi NAPAD AUROM!" : "Darkec koristi mekanu šapici kako bi ti nanio štetu!";
            yield return new WaitForSeconds(0.8f);

            int dmg = CalculateDamage(attackerAttack: darkec.napad, isHeavy: heavy);
            svetlan.PrimiStetu(dmg);
            playerHUD.SetHP(svetlan.trenutniHP, svetlan.HPmax);
            dialogueText.text = $"Svetlan prima {dmg} štete!";
            yield return new WaitForSeconds(1f);

            if (svetlan.JeHmrl())
            {
                state = BattleState.LOST;
                EndBattle();
                yield break;
            }
        }

        state = BattleState.PLAYERTURN;
        actionPanel.SetActive(true);
        dialogueText.text = "Odaberi kak buš klepil Darkeca!";
    }

    int CalculateDamage(int attackerAttack, bool isHeavy)
    {
        float mult = isHeavy ? 1.5f : 1.0f;
        int dmg = Mathf.RoundToInt(attackerAttack * mult) + Random.Range(-2, 3);
        return Mathf.Max(1, dmg);
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
            dialogueText.text = "Svetlan je pobijedio Darkeca!";
        else if (state == BattleState.LOST)
            dialogueText.text = "Svetlan je izgubio, ali želja za pobjedom u njemu i dalje gori...";
        StartCoroutine(RestartAfterDelay(3f));

    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Svetlan svetlan = FindObjectOfType<Svetlan>();

        if (svetlan != null)
        {
            if (svetlan.nivo == 1)
            {
                SceneTransition.Instance.LoadSceneWithFade("KrunaKraljaZvonimiraMinigame");
            }
            else if (svetlan.nivo == 2)
            {
                SceneTransition.Instance.LoadSceneWithFade("Platformer");

            }
           
        }
    }

}
