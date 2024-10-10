using System.Collections;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [Header("Dice values")]
    [SerializeField] private Rigidbody2D rb; //2d rigid body
    [SerializeField] private float power; //Dice throw power
    [SerializeField] private float torque; //Dice tourque power

    [Header("Results")]
    [SerializeField] private TMP_Text diceText; //The text component for showing the dice number.
    [SerializeField] private int[] DLength;//Dice length array
    private int diceNum; //Dice outcome

    private Coroutine movementLocker; //coroutine for stop calling twice
    private WaitForSeconds movementLockTimer = new WaitForSeconds(0.35f); //the time for lock movement

    [Header("Game Event")]
    [SerializeField] private GameEvent OnDiceRollOver; //Raise the event when dice number setted and dice movement locked

    private void Start()
    {
        diceText.text = "::";
    }

    public void DiceRoll()
    {
        if(movementLocker != null)
            StopCoroutine(movementLocker);

        //Make dice number empty while rolling
        diceText.text = "::";
        //Set the dice back to the starting position
        transform.position = new Vector2(0, -3);
        //Give random direction
        Vector2 dir = new Vector2(Random.Range(-1.5f, 1.5f), power);
        //Unlock dice movement
        rb.bodyType = RigidbodyType2D.Dynamic;
        //Stop dice
        rb.velocity = Vector2.zero;
        //Throw dice
        rb.AddForce(dir, ForceMode2D.Impulse);
        //Give torque
        rb.AddTorque(torque, ForceMode2D.Impulse);
        //Call GroundCheck function
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if dice on ground
        if(collision.gameObject.layer == 3)
        {
            //Set dice number
            diceNum = DLength[Random.Range(0, DLength.Length)];
            //Show it on the screen;
            diceText.text = diceNum.ToString();
            //Make dice rotation zero
            transform.eulerAngles= Vector3.zero;
            //Call the function after a short delay
            if(movementLocker == null)
               movementLocker = StartCoroutine(LockDiceMovement());

            AudioPlayer.Play(transform.position, SoundManager.i.ChosedClipGround());
        }
        else if(collision.gameObject.layer == 6)
        {
            //Play dice to dice hit sound
            AudioPlayer.Play(transform.position, SoundManager.i.ChosedClipDice());
        }
    }

    private IEnumerator LockDiceMovement()
    {
        yield return movementLockTimer;
        ///Lock movement
        rb.bodyType = RigidbodyType2D.Static;
        movementLocker= null;
        OnDiceRollOver.Raise(null, null);
    }
}
