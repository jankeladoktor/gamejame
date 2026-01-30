using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkriptaBorbe : MonoBehaviour
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
    public BattleState state;

    public Svetlan svetlan;
    public DarkecBoss darkec;

    public HUDborba playerHUD;
    public HUDborba enemyHUD;

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

        // START TEXT (kak si htio)
        dialogueText.text = "Darkec je spreman za kec na kec u matiènu!";

        // odmah da si na potezu (gumbi se pojave u PlayerTurn)
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        // dok biraš
        dialogueText.text = "Odaberi kak buš klepil Darkeca!";
        actionPanel.SetActive(true);
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
        // makni gumbe èim klikneš
        actionPanel.SetActive(false);

        // odmah ispiši koju si akciju odabrao
        dialogueText.text = isHeavy ? "Svetlan koristi HEAVY attack!" : "Svetlan koristi LIGHT attack!";
        yield return new WaitForSeconds(0.8f);

        float hitChance = isHeavy ? 0.7f : 0.95f;
        bool hit = Random.value <= hitChance;

        if (!hit)
        {
            dialogueText.text = isHeavy ? "Heavy attack je promašio!" : "Light attack je promašio!";
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
        // dok darkec bira: nema gumba
        actionPanel.SetActive(false);

        // tekst kak si htio
        dialogueText.text = "Darkec grunta kak da te uspava...";
        yield return new WaitForSeconds(1.2f);

        // Darkec AI
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
            // ispiši koju akciju koristi
            dialogueText.text = heavy ? "Darkec koristi HEAVY attack!" : "Darkec koristi LIGHT attack!";
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
        PlayerTurn();
    }

    int CalculateDamage(int attackerAttack, bool isHeavy)
    {
        float mult = isHeavy ? 1.5f : 1.0f;
        int dmg = Mathf.RoundToInt(attackerAttack * mult);

        dmg += Random.Range(-2, 3);

        if (dmg < 1) dmg = 1;
        return dmg;
    }

    void EndBattle()
    {
        actionPanel.SetActive(false);

        if (state == BattleState.WON)
            dialogueText.text = "Svetlan je pobijedio Darkeca!";
        else if (state == BattleState.LOST)
            dialogueText.text = "Svetlan je izgubio...";
    }
}

