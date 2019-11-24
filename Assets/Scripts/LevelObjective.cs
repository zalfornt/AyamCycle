using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelObjective : MonoBehaviour
{
    public Objective objective;
    public int[] amountToGoal = new int[6];
    public Image targetImage;
    public TMP_Text amountText;
    public TMP_Text timerText;

    private GameManager gm;
    private float timer;
    private int intTime;

    private void Awake()
    {
        Blackboard.Instance.LevelObjective = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = this.GetComponent<GameManager>();
        for (int i = 0; i < objective.objectives.Length; i++)
        {
            amountToGoal[objective.objectives[i]] = objective.amounts[i];
            SetImage(objective.objectives[i]);
            amountText.text = "x " + objective.amounts[i];
        }
        timer = objective.time;
        UpdateTimer();
    }

    private void Update()
    {
        if (gm.playing)
        {
            timer -= Time.deltaTime;
            UpdateTimer();
            if (timer <= 0)
            {
                gm.LoseGame();
            }
        }
    }

    private void SetImage(int stage)
    {
        this.GetComponent<TileSpriteHandler>().SetNewTargetSprite(stage, targetImage);
    }

    public void AddToObjectives(int stage, Tile.Gender gender)
    {
        if (stage == 4 && gender == Tile.Gender.FEMALE) stage = 5;
        if (amountToGoal[stage] > 0)
        {
            //--------------------------------------//
            //BRUTE FORCE ONLY CHECK FOR 1 OBJECTIVE//
            //--------------------------------------//
            amountToGoal[stage]--;
            amountText.text = "x " + amountToGoal[stage];
            if(amountToGoal[stage] == 0)
            {
                gm.WinGame();
            }
        }
    }

    private void UpdateTimer()
    {
        intTime = Mathf.CeilToInt(timer);
        timerText.text = intTime.ToString("D");
    }
}
