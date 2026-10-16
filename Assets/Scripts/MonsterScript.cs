using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    public int maxBloodAmount = 100;   // 满血值
    private int bloodAmount;
    private Animator animator;
    public Image BloodImage;           // 头顶血条（Filled 类型 Image）
    private bool isDead = false;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.bloodAmount = maxBloodAmount;
        UpdateBloodBar();
    }

    // 被玩家击中时调用，每次扣 10 点血
    public void beAttach()
    {
        if (isDead) return;   // 已死亡则不再扣血，避免重复触发死亡逻辑
        this.bloodAmount -= 10;
        UpdateBloodBar();
        if (this.bloodAmount <= 0)
        {
            isDead = true;
            this.animator.SetBool("death", true);
            Invoke("DestroyThis", 2);
        }
    }

    // 根据当前血量更新血条填充比例
    void UpdateBloodBar()
    {
        if (BloodImage != null)
            BloodImage.fillAmount = Mathf.Clamp01((float)bloodAmount / maxBloodAmount);
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
    public void StartSinking()
    {
        Invoke("DestroyThis", 2);
    }
}
