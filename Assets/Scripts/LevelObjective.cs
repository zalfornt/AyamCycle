using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelObjective : MonoBehaviour
{
    public Objective objective;
    public int[] amountToGoal = new int[6];
    public int[] achieved = new int[6];
    public Image[] targetImage;
    public TMP_Text[] amountText;
    public TMP_Text timerText;

    public int[] stageHolder = new int[3];

    private GameManager gm;
    public float timer;
    private int intTime;

    private void Awake()
    {
        Blackboard.Instance.LevelObjective = this;
        objective = Blackboard.Instance.Objective;
        timer = objective.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < achieved.Length; i++)
        {
            achieved[i] = 0;
        }
        gm = this.GetComponent<GameManager>();
        int image = 0;
        for (int i = 0; i < objective.objectives.Length; i++)
        {
            if (objective.amounts[i] > 0)
            {
                amountToGoal[objective.objectives[i]] = objective.amounts[i];
                SetImage(objective.objectives[i], image);
                amountText[image].text = "0 / " + objective.amounts[i];
                stageHolder[image++] = objective.objectives[i];
            }
        }
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

    private void SetImage(int stage, int imgIndex)
    {
        this.GetComponent<TileSpriteHandler>().SetNewTargetSprite(stage, targetImage[imgIndex]);
    }

    public void AddToObjectives(int stage, Tile.Gender gender, bool isX2)
    {
        if (stage == 4 && gender == Tile.Gender.FEMALE) stage = 5;
        if (amountToGoal[stage] > 0)
        {
            int multiplier = 1;
            if (isX2) multiplier = 2;
            //if (amountToGoal[stage] > 0)
            //{
            //    //--------------------------------------//
            //    //BRUTE FORCE ONLY CHECK FOR 1 OBJECTIVE//
            //    //--------------------------------------//
            //    amountToGoal[stage]--;
            //    amountText.text = "x " + amountToGoal[stage];
            //    if(amountToGoal[stage] == 0)
            //    {
            //        gm.WinGame();
            //    }
            //}
            int image = 0;
            for (int i = 0; i < 3; i++)
            {
                if (stage == 0)
                {
                    image = 0;
                    break;
                }
                if (stageHolder[i] == stage) image = i;
            }
            achieved[stage] += (1 * multiplier);
            amountText[image].text = achieved[stage] + " / " + amountToGoal[stage];

            bool win = true;

            for (int i = 0; i < 6; i++)
            {
                if (achieved[i] < amountToGoal[i]) win = false;
                if (!win) break;
            }

            if (win) gm.WinGame();
        }
    }

    private void UpdateTimer()
    {
        intTime = Mathf.CeilToInt(timer);
        timerText.text = intTime.ToString("D");
    }
}
