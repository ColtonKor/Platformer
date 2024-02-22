using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI pointText;
    public int timerVariable;
    private int coin = 0;
    private int points = 0;

    void Start(){
        coinText.text = $"x {coin}";
        pointText.text = $"MARIO \n {points}";
    }
    // Update is called once per frame
    void Update()
    {
        int intTime = timerVariable - (int)Time.realtimeSinceStartup;
        string timeStr = $"Time \n {intTime}";
        timerText.text = timeStr;


        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)){
                if (hit.collider.CompareTag("Brick")){
                    BreakBrick(hit.collider.gameObject);
                } else if (hit.collider.CompareTag("Question")){
                    Collect();
                }
            }
        }
    }

    void BreakBrick(GameObject brick)
    {
        Destroy(brick);
    }

    void Collect(){
        coin++;
        coinText.text = $"x {coin}";
    }
}
