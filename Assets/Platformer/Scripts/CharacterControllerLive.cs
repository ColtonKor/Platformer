using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterControllerLive : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI pointText;
    public GameObject loseText;
    public int timerVariable = 100;
    private int coin = 0;
    private int points = 0;
    public GameObject winText;
    public float acceleration = 15f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 51f;
    public float jumpBoost = 3f;
    public bool isGrounded;
    public Camera camera;
    private bool collected = false;
    private bool winner = false;
    private int intTime = 0;
    private float levelStartTime;
    private float speedTemp;
    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        coinText.text = $"x {coin}";
        pointText.text = $"MARIO \n {points}";
    }

    void FixedUpdate(){
        camera.transform.position = new Vector3(transform.position.x + 13f, 10f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rb = GetComponent<Rigidbody>();
        if(Input.GetKey(KeyCode.LeftShift)){
            speedTemp = acceleration*2f;
        } else {
            speedTemp = acceleration;
        }
        rb.velocity += Vector3.right * horizontalMovement * Time.deltaTime * speedTemp;
        // rb.velocity *= Mathf.Abs(horizontalMovement);
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.01f;
        float above = col.bounds.extents.y + col.bounds.extents.y;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * 2f;
        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);

        if (isGrounded && collected) {
            collected = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(startPoint, Vector3.up, out hit, above)){
            if (hit.collider.CompareTag("Brick")){
                BreakBrick(hit.collider.gameObject);
            } else if (hit.collider.CompareTag("Question")){
                Collect();
            }
        }
        // Color lineColor = (isGrounded) ? Color.red : Color.blue;
        // Debug.DrawLine(startPoint, aboveEnd, Color.red, 0f, true);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        } 
        else if (!isGrounded && Input.GetKeyDown(KeyCode.Space)){
            if(rb.velocity.y > 0){
                rb.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
            }
        }

        if(Mathf.Abs(rb.velocity.x) > maxSpeed){
            Vector3 newVel = rb.velocity;
            newVel.x = Mathf.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rb.velocity = newVel;
        }

        // if(isGrounded && Mathf.Abs(horizontalMovement) < .5f){
        //     Vector3 newVel = rb.velocity;
        //     newVel.x = 1f - Time.deltaTime;
        //     rb.velocity = newVel;
        // }

        float yaw = (rb.velocity.x > 0) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f , yaw, 0f);

        float speed = Mathf.Abs(rb.velocity.x);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", speed);
        anim.SetBool("In Air", !isGrounded);

        if(!winner){
            intTime = timerVariable - (int)(Time.realtimeSinceStartup - levelStartTime);
        }
        string timeStr = $"Time \n {intTime}";
        timerText.text = timeStr;

        if(intTime == 0){
            loseText.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R)){
            levelStartTime = Time.realtimeSinceStartup;
            winner = false;
            coin = 0;
            points = 0;
            coinText.text = $"x {coin}";
            pointText.text = $"MARIO \n {points}";
            loseText.SetActive(false);
            winText.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Avoid"){
            gameObject.transform.position = new Vector3(18.24f,1.54f,-0.29f);
        }
    }

    void OnTriggerEnter(Collider collider){
        winner = true;
        winText.SetActive(true);
    }

    void BreakBrick(GameObject brick){
        points += 100;
        pointText.text = $"MARIO \n {points}";
        Destroy(brick);
    }

    void Collect(){
        if (!collected) {
            points += 100;
            pointText.text = $"MARIO \n {points}";
            coin++;
            coinText.text = $"x {coin}";
            collected = true;
        }
    }
}
