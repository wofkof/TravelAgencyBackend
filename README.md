# TravelAgencyBackend 專案 - 團隊開發指南

確保大家順利協作，並保持程式碼和資料的一致性。

---

## ✨ GitHub 團隊協作完整流程

### ➕ 1. Clone 專案（第一次加入專案）

每個組員第一次加入專案時，請執行：

```bash
git clone https://github.com/wofkof/TravelAgencyBackend.git
```

### 🔄 2. 開發新功能時，從 main 切新分支

每個功能都切一條新分支，命名規則：`feature/功能名稱`

```bash
# 取得最新main
git checkout main
git pull origin main

# 切出新功能分支
git checkout -b feature/行程管理
```

### 🎓 3. 撰寫程式並Commit到自己的功能分支

```bash
git add .
git commit -m "Add travel plan CRUD"
git push origin feature/行程管理
```

### 📃 4. 建立Pull Request（PR），請組員Review

- 到GitHub專案頁面
- 建立PR，`feature/行程管理` → `main`
- 其他組員Review，沒問題再合併

### 📅 5. 功能完成後，合併並刪除分支

合併PR後，本機也要同步刪除舊分支：

```bash
git checkout main
git pull origin main
git branch -d feature/行程管理
```

---

## 🌐 Azure SQL Database

### 連線字串範例

```
Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=TravelAgencyDB;Persist Security Info=False;User ID=你的帳號;Password=你的密碼;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

> 密碼請不要放在appsettings.json，改放Development.json

---

## 🛠️ Git 共同作業流程重點整理

| 步驟      | 指令                                               | 說明            |
| ------- | ------------------------------------------------ | ------------- |
| 第一次下載專案 | `git clone`                                      | 只做一次          |
| 每次開始新功能 | `git checkout main`+`git pull`+`git checkout -b` | 切新功能分支        |
| 撰寫程式    | 編寫功能                                             | 直接開發          |
| 加入檔案    | `git add .`                                      | 加入修改          |
| Commit  | `git commit -m "訊息"`                             | 清楚描述做了什麼      |
| 推到遠端    | `git push origin 分支名`                            | 把分支推上GitHub   |
| 發PR     | 在GitHub建立PR                                      | 其他組員Review後合併 |
| 合併後     | `git checkout main`+`git pull`+`git branch -d`   | 清理舊分支         |

---

## ✨ 有問題請在群組發問，或私訊專案負責人。

