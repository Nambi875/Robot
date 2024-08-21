using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    // 移動関連の変数
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;

    // 体力関連の変数
    public int health = 3;
    public int maxHealth = 5;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite halfHeart;  // 追加: ハーフハートのスプライト

    // 無敵システム関連の変数
    public float invincibleTime = 2.0f;
    private bool isInvincible = false;
    private float invincibleTimer = 0f;

    // 入力制御の変数
    private bool isInputEnabled = true;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // 足音関連の変数
    public AudioSource movementAudioSource;
    public AudioClip footstepSound;
    public float footstepInterval = 0.5f; // 足音の再生間隔
    private float footstepTimer = 0f;

    // ノックバック関連の変数
    public float knockbackForce = 10f;  // プレイヤーのノックバック力
    private bool isKnockback = false;
    private Vector2 knockbackDirection;

    private void Awake()
    {
        // 必要なコンポーネントの取得
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // movementAudioSourceが設定されていない場合、AudioSourceコンポーネントを取得
        if (movementAudioSource == null)
        {
            movementAudioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // タイムライン再生中の場合、入力を無視
        if (!isInputEnabled)
        {
            rb.velocity = Vector2.zero;  // プレイヤーの速度を0に設定
            anim.SetFloat("Speed", 0f);  // アニメーションの速度を0に設定
            Cursor.visible = false; // カットシーンが開始された時にマウスカーソルを非表示にする
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }

        if (isInputEnabled)
        {
            Cursor.visible = true; // カットシーンが終了した時にマウスカーソルを表示する
            Cursor.lockState = CursorLockMode.Confined;
        }

        // プレイヤーの入力処理
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 移動速度が一定になるように正規化
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // アニメーションを更新
        anim.SetFloat("Speed", movement.sqrMagnitude);

        // プレイヤーが移動している場合、足音を再生
        if (movement.sqrMagnitude > 0)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                movementAudioSource.PlayOneShot(footstepSound);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            // プレイヤーが停止したら足音を停止
            if (movementAudioSource.isPlaying)
            {
                movementAudioSource.Stop();
            }
            footstepTimer = 0f; // タイマーをリセット
        }

        // 無敵状態の処理
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
                spriteRenderer.color = originalColor; // 無敵状態解除時に色を復元
            }
        }
    }

    private void FixedUpdate()
    {
        // 物理的な移動処理
        if (isInputEnabled && !isKnockback)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int damage, Vector2 damageSourcePosition)
    {
        if (!isInvincible)
        {
            // 体力を減らす
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            else
            {
                // ノックバック方向を設定
                knockbackDirection = (transform.position - (Vector3)damageSourcePosition).normalized;
                StartCoroutine(Knockback(0.2f, knockbackForce));  // ノックバックを適用

                // 無敵状態を開始
                StartCoroutine(BecomeInvincible());
            }

            // 体力UIを更新
            UpdateHealthUI();
        }
    }

    private IEnumerator Knockback(float duration, float power)
    {
        float timer = 0f;
        isKnockback = true;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            rb.velocity = Vector2.zero; // 現在の速度をリセット
            rb.AddForce(knockbackDirection * power, ForceMode2D.Impulse);
            yield return null;
        }

        isKnockback = false;
    }

    private IEnumerator BecomeInvincible()
    {
        // 無敵状態の開始
        isInvincible = true;
        invincibleTimer = invincibleTime;

        // 無敵状態中はプレイヤーの色を透明に変更
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        // 無敵時間の待機
        yield return new WaitForSeconds(invincibleTime);

        // 無敵状態解除時に色を元に戻す
        spriteRenderer.color = originalColor;
    }

    private void UpdateHealthUI()
    {
        // 体力UIの更新
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i < health + 1 && health % 2 != 0) // 体力が奇数の場合、半分のハートを表示
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void Die()
    {
        // プレイヤーが死亡した時の処理
        Debug.Log("Player died!");
        // 例: ゲームオーバー画面を表示
    }

    public void EnableInput()
    {
        // 入力を有効化
        isInputEnabled = true;
        Debug.Log("PlayerAction: Input enabled");
    }

    public void DisableInput()
    {
        // 入力を無効化
        isInputEnabled = false;
        rb.velocity = Vector2.zero;  // 入力を無効にした時に即座に速度を0に設定
        Debug.Log("PlayerAction: Input disabled");
    }
}