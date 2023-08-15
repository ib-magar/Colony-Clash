using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class BOSS : LivingEntity
{

    [Header("Move to start position")]
    public Transform _startPosition;
    public float _timeToMoveToStartPosition;


    public override void  Start()
    {
        base.Start();
        _healthBar.maxValue = startingHealth;
        _healthBar.value= startingHealth;
        DamageEvent += GetDamage;
        DiedEvent += Death;
        MoveToStartPosition();
        GameObject.FindObjectOfType<LevelManager>().BOSSENEMY += MoveToStartPosition;
        _cameraMg = GameObject.FindObjectOfType<CameraBlend>();

        StartCoroutine(GenerateEnemyOnLoop());
    }
    public void GetDamage()
    {
        Debug.Log("boss get damage");
    }
    public void Death()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 100f, 8);
        foreach(Collider2D c in enemies)
        {
           if( c.TryGetComponent<LivingEntity>(out LivingEntity l))
            {
                l.takeDamage(200f);
            }

        }

        Debug.Log("boss died");
        Debug.Log("Game complete");
        StartCoroutine(GameComplete());
        SceneManager.LoadScene(2);
        // Game complte view 
        // GAME COMPLETE MENU
    }

    private IEnumerator GameComplete()
    {
        //Time.timeScale = .4f;
        yield return new WaitForSeconds(5f);
        //Debug.Log("loading new scene");
    }

    public void MoveToStartPosition()
    {
        float targetXpos = _startPosition.position.x;
        transform.DOMoveX(targetXpos, _timeToMoveToStartPosition).OnComplete(() =>
        {
            Debug.Log("Reached to start position");
            StartCoroutine(startAttackPattern());
        });
    }
    public float _AttackStarttime=5;
    IEnumerator startAttackPattern()
    {
        yield return new WaitForSeconds(_AttackStarttime);
        StartCoroutine(LegAttackPattern());
    }
    [Header("Health bar")]
    public Slider _healthBar;
    private void FixedUpdate()
    {
        _healthBar.value = health;
    }

    [Header("Leg attack ")]
    public Transform[] _TargetBlocks;
    public Transform front_left;
    public Transform front_right;
    public Transform back_left;
    public Transform back_right;
    public int _noOfAttacks = 5;
    public float _attackTime;
    public float _getLegReturnTime;
    public float _AttackTimeInterval;

    public float horizontalDiffPoint;
    public float verticalDiffPoint;

    [Space]
    private float _originalAlpha;
    public float _targetAlpha;
    public Color _targetColor;
    private Color _originalcolor;

    [Space]
    public CameraBlend _cameraMg;
    public AudioClip[] leg_attackSound;
    public IEnumerator LegAttackPattern()
    {
        int _remainingAttackCount = _noOfAttacks;
        while(_remainingAttackCount>0)
        {
            _remainingAttackCount--;
            Transform targetBlock = _TargetBlocks[Random.Range(0,_TargetBlocks.Length)];
            Transform targetLeg;

            if(targetBlock.position.x>=verticalDiffPoint)
            {
                if (targetBlock.position.y > horizontalDiffPoint) targetLeg = front_right;
                else targetLeg = front_left;
            }
            else
            {
                if (targetBlock.position.y > horizontalDiffPoint) targetLeg = back_right;
                else targetLeg = back_left;
            }
            Vector3 originalPosition = targetLeg.position;
            targetLeg.DOJump(targetBlock.position, 5, 1, _attackTime - _getLegReturnTime).SetEase(Ease.InExpo).OnStart(() =>
            {
               /* Color c = targetBlock.GetComponent<SpriteRenderer>().color;
                _originalcolor = c;
                c = _targetColor;
                c.a = _targetAlpha;
                targetBlock.GetComponent<SpriteRenderer>().color = c;*/

            }).OnComplete(() =>
            {
                DetectAnts(targetBlock.position, _radius);
                // DO CAMERA SHAKE EFFECT 
                SoundManager.Instance.PlaySoundEffect(leg_attackSound[Random.Range(0, leg_attackSound.Length)]);
                _cameraMg.ShakeCamera();

                Debug.Log("leg attack success");
                // Detect the Ants in this range and damage them.
                targetLeg.DOMove(originalPosition, _getLegReturnTime).OnStart(() =>
                {
                    /*Color c = targetBlock.GetComponent<SpriteRenderer>().color;
                    c = _originalcolor;
                    c.a = 0f;
                    targetBlock.GetComponent<SpriteRenderer>().color = c;*/
                });
            });
            yield return new WaitForSeconds(_attackTime);
            yield return new WaitForSeconds(_AttackTimeInterval);
        }

        Debug.Log("Leg attack pattern finished");
        StartCoroutine(MoveHereAndThere());
    }

    public LayerMask _antLayer;
    public void DetectAnts(Vector3 pos, float _radius)
    {
        Collider2D ant = Physics2D.OverlapCircle(pos, _radius,_antLayer);
        if(ant!=null)
        {
            ant.GetComponent<LivingEntity>().takeDamage(100f);
        }
    }

    [Header("Move Behaviour")]
    public float _maxYPosition;
    public float _minYPosition;
    public float _originalPosition;
    public int _moveCount;
    public float _moveTime;
    public float _moveInterval;
    public IEnumerator MoveHereAndThere()
    {
        float originalYPo=transform.position.y;
        float moveCnt = _moveCount;
        bool movingUp = true;
        while(moveCnt>0)
        {
            moveCnt--;
            if(movingUp)
            {
                transform.DOMoveY(_maxYPosition, _moveTime);
            }
            else
            {
                transform.DOMoveY(_minYPosition, _moveTime);
            }
            yield return new WaitForSeconds(_moveTime);
                movingUp = !movingUp;
            yield return new WaitForSeconds(_moveInterval);
        }
        transform.DOMoveY(originalYPo, _moveTime);
        yield return new WaitForSeconds(_moveTime);
        StartCoroutine(GenerateEnemies());
    }

    [Header("Enemies Generation Behaviour")]
    public GameObject[] enemies;
    public int enemiesCount;
    public float enemiesGenerationInterval;

    public float[] yPos;
    public float xPos=11f;

    public float back_posX;
    public float _backTime;
    public float _returnBacktime;
    public IEnumerator GenerateEnemies()
    {
        float originalXPos = 6f;
        transform.DOMoveX(back_posX, _backTime);
        yield return new WaitForSeconds(_backTime/1.2f);
        int enemiesCnt=enemiesCount;
        while(enemiesCnt>0)
        {
            enemiesCnt--;
            float yPosition = yPos[Random.Range(0, yPos.Length)];
            Vector3 pos = new Vector3(xPos, yPosition, 0f);

            GameObject targetEnemy;
            float difficultyValue = Random.value;
            if(difficultyValue<=.4f)
            {
                targetEnemy = enemies[Random.Range(0,2)];
            }
            else if(difficultyValue>.4f && difficultyValue<.8f)
            {
                targetEnemy = enemies[Random.Range(2, 4)];
            }
            else
            {
                targetEnemy = enemies[4];
            }

            Instantiate(targetEnemy, pos, targetEnemy.transform.rotation);
            yield return new WaitForSeconds(enemiesGenerationInterval);
        }

        transform.DOMoveX(originalXPos, _returnBacktime);
        yield return new WaitForSeconds(_returnBacktime);

        Debug.Log("enemies generation attack finished");

        StartCoroutine(LegAttackPattern());
    }
    [Header("Loop enemies")]
    public float _loopGenerateInterval;
    public IEnumerator GenerateEnemyOnLoop()
    {
        while(true)
        {
            float yPosition = yPos[Random.Range(0, yPos.Length)];
            Vector3 pos = new Vector3(xPos, yPosition, 0f);

            GameObject targetEnemy;
            float difficultyValue = Random.value;
            if (difficultyValue <= .4f)
            {
                targetEnemy = enemies[Random.Range(0, 2)];
            }
            else if (difficultyValue > .4f && difficultyValue < .8f)
            {
                targetEnemy = enemies[Random.Range(1, 3)];
            }
            else
            {
                targetEnemy = enemies[0];
            }

            Instantiate(targetEnemy, pos, targetEnemy.transform.rotation);
            yield return new WaitForSeconds(_loopGenerateInterval);
        }
    }
    [Header("attack radius")]
    public float _radius;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, _startPosition.position);

        Gizmos.DrawWireSphere(front_left.position,_radius);
        Gizmos.DrawWireSphere(front_right.position,_radius);
        Gizmos.DrawWireSphere(back_left.position,_radius);
        Gizmos.DrawWireSphere(back_right.position,_radius);
    }

}
