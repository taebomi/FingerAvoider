using UnityEngine;
using System.Collections;
using DG.Tweening;
public class CharacterScript : MonoBehaviour
{
    Vector2 lastPositionTouch;
    Vector2 nowPositionTouch;
    Vector2 deltaPositionTouch;
    Vector3 lastPosition;
    Vector3 deltaPosition;
    Rigidbody2D rb;
    public BoxCollider2D bc;
    Touch touch;
    public Sprite[] sp;
    bool isDeath = false;
    bool isClear = false;
    public AudioClip[] se;
    float speed;
    Coroutine playerMoveC;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        isDeath = false;
        isClear = false;
        speed = PlayerPrefs.GetFloat("Sensivity");
        GameStart();
    }
    public void Restart() {
        StopCoroutine(playerMoveC);
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<SpriteRenderer>().sprite = sp[PlayerPrefs.GetInt("Character") * 3];
        isDeath = false;
        isClear = false;
        speed = PlayerPrefs.GetFloat("Sensivity");
        rb.linearVelocity = Vector3.zero;
    }
    /*
    IEnumerator TouchPositionMove()
    {
        while (true)
        {
            if (Input.touchCount == 1)
            {
                lastPositionTouch = Input.GetTouch(0).position;
                lastPositionTouch = Camera.main.ScreenToWorldPoint(lastPositionTouch);
                transform.position = Vector2.Lerp(transform.position, lastPositionTouch, 0.33f);
            }
            yield return null;
        }
    }
    */  // TouchPositionMove함수
    public void GameStart()
    {
#if UNITY_ANDROID
        //StartCoroutine(TouchPositionMove());
        playerMoveC = StartCoroutine(TouchDeltaPositionMove());
#endif
#if UNITY_EDITOR
        //StartCoroutine(MousePositionMove());
        playerMoveC = StartCoroutine(MouseDeltaPositionMove());
#endif
    }
    public void StopSpeed()
    {
        speed = 0;
    }
    public void RefreshSpeed()
    {
        speed = PlayerPrefs.GetFloat("Sensivity");
    }
    public void TrailClear()
    {
        transform.GetComponentInChildren<TrailRenderer>().Clear();
    }
    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y,-1f);
    }
    public void GameOver()
    {
        if (!isDeath && !isClear)
        {
            isDeath = true;
            bc.enabled = false;
            GameSystem.instance.PlaySE(se[0]);
            StopCoroutine(playerMoveC);
            GetComponent<SpriteRenderer>().sprite = sp[PlayerPrefs.GetInt("Character") * 3 + 1];
            transform.DOShakePosition(0.5f,1.5f,30);
        }
    }
    public void GameClear()
    {
        isClear = true;
        bc.enabled = false;
        StopCoroutine(playerMoveC);
        GetComponent<SpriteRenderer>().sprite = sp[PlayerPrefs.GetInt("Character") * 3 + 2];
    }
    IEnumerator TouchDeltaPositionMove()
    {
        while (true)
        {
            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);
                lastPositionTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                yield return new WaitForFixedUpdate();
                if (Input.touchCount == 1 && touch.phase != TouchPhase.Ended)
                {
                    deltaPositionTouch = (Vector2)(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)) - lastPositionTouch;
                    deltaPosition = deltaPositionTouch;
                    rb.MovePosition(transform.position + deltaPosition*speed);
                }
            }
            else
                yield return new WaitForFixedUpdate();
        }
    }
    /*
    IEnumerator MousePositionMove()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                lastPosition = Input.mousePosition;
                lastPosition = Camera.main.ScreenToWorldPoint(lastPosition);
                transform.position = Vector2.Lerp(transform.position, lastPosition, 0.33f);
            }
            yield return null;
        }
    }
    */  // MousePositionMove 함수
    IEnumerator MouseDeltaPositionMove()
    {
        while (true)
        {
            if (Input.GetMouseButton(1))
            {
                lastPosition = Input.mousePosition;
                lastPosition = Camera.main.ScreenToWorldPoint(lastPosition);
                yield return new WaitForFixedUpdate();
                deltaPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastPosition;
                rb.MovePosition(transform.position+deltaPosition * speed);
            }
            else
                yield return new WaitForFixedUpdate();
        }
    }
    public void CheckGround()
    {
        if (!Physics2D.Raycast(transform.position, -transform.forward, 100, LayerMask.GetMask("Ground")))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
}
