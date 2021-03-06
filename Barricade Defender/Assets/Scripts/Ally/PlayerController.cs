using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : PlayerStats
{
    [HideInInspector]
    public UnityEvent OnDeath;

    public Slider SpSlider;
    public Slider HpSlider;
    public GameObject MeleeAttackPrefab;
    public GameObject MeleeAttackLeftPos;
    public GameObject MeleeAttackRightPos;
    public GameObject BulletPrefab;
    public GameObject ShootPosition;
    public int Damage;
    public float MeleeAttackTime;
    public float SpellShieldCd;
    public Image SpellShieldImage;
    public TextMeshProUGUI SpellShieldImageText;
    public float SpellMeleeCd;
    public Button SpellMeleeImage;
    public TextMeshProUGUI SpellMeleeImageText;
    public TextMeshProUGUI PlayerStatsText;

    [SerializeField]
    private int moveSpeed;

    private Animator animator;
    private float lastXPos;
    private Rigidbody2D rb;
    private Vector2 shootPosVector;
    private bool canAttack = true;
    private bool canMove = true;
    private float spellShieldCooldownTimer;
    private float spellMeleeCooldownTimer;
    private int spellMeleeEffectId;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HpCurrent = HpMax;
        HpSlider.maxValue = HpMax;
        SpSlider.maxValue = spMax;
    }

    void Update()
    {
        Death();
        UpdateSliders();
        MeleeAttack();
        MeleeSpell();
        SpellShield();
        DisplayShieldSlider();
        UpdateStats();

        if (canMove == false)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Movement();
    }

    private void UpdateStats()
    {
        PlayerStatsText.text = "Attack: " + Damage + "\nHealth: " + HpCurrent + "/" + HpMax + "\nShield: " + spCurrent + "/" + spMax; 
    }

    private void UpdateSliders()
    {
        HpSlider.value = HpCurrent;
        SpSlider.value = spCurrent;
    }

    private void Death()
    {
        if (HpCurrent <= 0)
        {
            OnDeath.Invoke();
        }
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }

        if (horizontalInput > 0f)
        {
            lastXPos = 1f;
        }

        if (horizontalInput < 0f)
        {
            lastXPos = -1f;
        }

        animator.SetFloat("xPos", lastXPos);
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

        gameObject.transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -13f, 19f),
            Mathf.Clamp(transform.position.y, -6, 2));
    }

    private void MeleeAttack()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (mousePosition.x >= gameObject.transform.position.x)
            {
                lastXPos = 1f;
            }
            else
            {
                lastXPos = -1f;
            }
            animator.SetFloat("xPos", lastXPos);

            canMove = false;
            canAttack = false;
            animator.SetBool("isAttacking", true);
            animator.SetBool("isAttackingMelee", true);
            StartCoroutine(CoDoAttack(MeleeAttackTime, "isAttackingMelee"));


            switch (lastXPos)
            {
                case 1f:
                    var attackRight = Instantiate(MeleeAttackPrefab, MeleeAttackRightPos.transform.position, Quaternion.identity);
                    attackRight.GetComponent<PlayerMeleeAttack>().Damage = Damage;
                    break;

                case -1f:
                    var attackLeft = Instantiate(MeleeAttackPrefab, MeleeAttackLeftPos.transform.position, Quaternion.identity);
                    attackLeft.GetComponent<PlayerMeleeAttack>().Damage = Damage;
                    break;

                default:
                    break;
            }
        }
    }

    private IEnumerator CoDoAttack(float attackTime, string attackTypeName)
    {
        yield return new WaitForSeconds(attackTime);
        canMove = true;
        canAttack = true;
        animator.SetBool(attackTypeName, false);
        animator.SetBool("isAttacking", false);
    }

    private void MeleeSpell()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        spellMeleeCooldownTimer -= Time.deltaTime;

        if (spellMeleeCooldownTimer <= 0f)
        {
            SpellMeleeImageText.text = "RMB";
            SpellMeleeImage.image.color = new Color(255, 255, 255, 1);

            if (Input.GetMouseButtonDown(1) && canAttack)
            {
                canMove = false;
                canAttack = false;
                animator.SetBool("isAttacking", true);
                animator.SetBool("isAttackingSpell", true);
                StartCoroutine(CoDoAttack(MeleeAttackTime, "isAttackingSpell"));
                shootPosVector = new Vector2(ShootPosition.transform.position.x, ShootPosition.transform.position.y);

                GameObject bullet = Instantiate(BulletPrefab, shootPosVector, Quaternion.identity);

                switch (spellMeleeEffectId)
                {
                    case 0:
                        bullet.GetComponent<BulletController>().effectId = 0;
                        break;

                    case 1:
                        bullet.GetComponent<BulletController>().effectId = 1;
                        break;

                    default:
                        Debug.LogWarning("incorrect melee spell id - effect not found");
                        return;
                }

                bullet.GetComponent<BulletController>().TargetTag = "Enemy";
                bullet.GetComponent<BulletController>().Damage = Damage;
                bullet.transform.right = direction;

                spellMeleeCooldownTimer = SpellMeleeCd;
            }
        }
        else
        {
            SpellMeleeImageText.text = spellMeleeCooldownTimer.ToString("0.0");
            SpellMeleeImage.image.color = new Color(255, 255, 255, 0.75f);
        }
    }

    private void SpellShield()
    {
        spellShieldCooldownTimer -= Time.deltaTime;

        if (spellShieldCooldownTimer <= 0f)
        {
            SpellShieldImageText.text = "MMB";
            SpellShieldImage.color = new Color(255, 255, 255, 1);

            if (Input.GetMouseButtonDown(2) && canAttack)
            {
                canMove = false;
                canAttack = false;
                animator.SetBool("isAttacking", true);
                animator.SetBool("isCastingShield", true);
                StartCoroutine(CoDoAttack(MeleeAttackTime, "isCastingShield"));

                spCurrent = spMax;
                spellShieldCooldownTimer = SpellShieldCd;
            }
        }
        else
        {
            SpellShieldImageText.text = spellShieldCooldownTimer.ToString("0.0");
            SpellShieldImage.color = new Color(255, 255, 255, 0.75f);
        }
    }

    private void DisplayShieldSlider()
    {
        if (spCurrent > 0)
        {
            SpSlider.gameObject.SetActive(true);
        }
        else
        {
            SpSlider.gameObject.SetActive(false);
        }
    }

    public void SetSpellMeleeEffect(int effectId)
    {
        spellMeleeEffectId = effectId;

        switch (effectId)
        {
            case 0:
                SpellMeleeCd = 2f;
                break;

            case 1:
                SpellMeleeCd = 6f;
                break;

            default:
                break;
        }

        if (SpellMeleeCd < spellMeleeCooldownTimer)
        {
            return;
        }
        spellMeleeCooldownTimer = SpellMeleeCd;
    }
}
