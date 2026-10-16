# Nightmare Shooter (kcsj)

一个基于 **Unity 2022.3.34f1c1** 的 3D 射击小游戏（课程设计作品）。玩家在场景中移动、转身并向不断刷新的僵尸怪物开火，怪物血量归零后播放死亡动画并销毁。游戏内提供设置面板，可调节背景音乐音量与主灯光强度。

美术资源取材自 Unity 官方经典教程 **Survival Shooter** 资源包（角色 Robot / 怪物 ZomBunny 等）。

---

## 环境要求

| 项目 | 版本 |
| --- | --- |
| Unity 编辑器 | `2022.3.34f1c1` |
| 渲染管线 | Built-in Render Pipeline |
| 主要依赖 | `com.unity.ugui`、`com.unity.textmeshpro`、`com.unity.visualscripting`、`com.unity.timeline` |

> 建议通过 Unity Hub 使用 **2022.3.34f1c1** 打开本工程，避免版本升级导致的资源兼容问题。

## 运行方式

1. 克隆 / 下载本仓库。
2. 用 Unity Hub 以 `2022.3.34f1c1` 打开工程根目录。
3. 打开场景 `Assets/Scenes/StartScene.unity`，点击 Play 即可运行。
4. 打包发行版：`File → Build Settings → Build`（本仓库不包含已构建产物，需自行构建）。

## 操作说明

| 操作 | 按键 |
| --- | --- |
| 前进 / 后退 | `W` / `S`（Vertical 轴） |
| 左右转身 | `A` / `D`（Horizontal 轴） |
| 开火 | 鼠标左键（`Fire1`），有效射击间隔 0.2 秒 |
| 打开设置面板 | 界面上的设置按钮 |
| 开始 / 退出 | 主菜单按钮 |

## 场景

| 场景 | 说明 |
| --- | --- |
| `Assets/Scenes/StartScene.unity` | 开始菜单：开始游戏 / 退出游戏 |
| `Assets/Scenes/ShooterScene.unity` | 战斗关卡：移动、射击、刷怪、设置面板 |

## 目录结构

```
Assets/
├── Scripts/        # 游戏逻辑脚本
├── Scenes/         # 场景文件
├── Prefabs/        # 预制体（僵尸怪物）
├── Animator/       # 动画状态机
├── Materials/      # 材质
├── Images/         # 图片资源
└── Shooter/        # Survival Shooter 素材（模型 / 贴图 / 环境）
Packages/           # Unity 包依赖清单
ProjectSettings/    # 工程设置
```

## 核心脚本

| 脚本 | 职责 |
| --- | --- |
| `Scripts/StartScript.cs` | 主菜单：加载战斗场景 / 退出游戏 |
| `Scripts/PlayerManagerScript.cs` | 玩家移动与转身；按下开火键从枪口发射 `Raycast`，命中怪物扣血，并用 `LineRenderer` 短暂显示射击射线 |
| `Scripts/MonsterScript.cs` | 怪物血量管理：每次被击中扣 10 点血，更新头顶血条填充，血量归零播放 `death` 动画并在 2 秒后销毁 |
| `Scripts/GenMonsterScript.cs` | 每 3 秒在玩家周围的圆环范围内随机刷新一只怪物 |
| `Scripts/GameSettingScript.cs` | 设置面板：打开时暂停游戏（`Time.timeScale = 0`），可调节背景音乐音量与主灯光强度 |

## 玩法流程

开始菜单 → 进入战斗关卡 → 键盘移动 / 转身 → 鼠标左键开火 → 命中怪物扣血（血条同步减少）→ 怪物死亡播动画并销毁 → 每 3 秒随机位置刷新新怪物。

## 已修复的问题

- 怪物不再固定刷新于原点 `(0,0,0)` 而互相重叠，改为在玩家周围半径 6~12 的圆环上随机取点。
- 射击改为「点击开火 + 冷却」，不再每帧持续扣血秒杀。
- 命中判定不再依赖 `monster` 标签（生成的预制体不带该标签），改为直接检测目标上的 `MonsterScript` 组件。
- 怪物头顶血条现在会随扣血实时更新，并移除脚本中无作用的计时逻辑。

## 版权说明

`Assets/Shooter/` 下的模型与贴图来自 Unity 官方 **Survival Shooter** 教程资源，仅供学习交流使用。
