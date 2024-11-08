<img src="./images/ms-cloud-workshop.png" />

Retrieval Augmented Generation (RAG) pattern for Azure AI Search  
Dec 2024

<br />

### Contents

- [Exercise 5: GitHub Actions から Azure への接続](#exercise-5-github-actions-から-azure-への接続)

  - [Task 1: Microsoft Entra ID へのアプリケーションの登録](#task-1-microsoft-entra-id-へのアプリケーションの登録)

  - [Task 2: フェデレーション資格情報の追加](#task-2-フェデレーション資格情報の追加)

  - [Task 3: ロールの割り当て](#task-3-ロールの割り当て)

  - [Task 4: GitHub シークレットの作成](#task-4-github-シークレットの作成)

  - [Task 5: OpenID Connect 認証を使用した Azure へのログイン](#task-5-openid-connect-認証を使用した-azure-へのログイン)

<br />

## Exercise 5: GitHub Actions から Azure への接続

<img src="./images/Ex5.png" />

<br />

### Task 1: Microsoft Entra ID へのアプリケーションの登録

- [Azure Portal](https://portal.azure.com/) から Microsoft Entra ID の **アプリの登録** を選択

- **＋ 新規登録** をクリック

  <img src="./images/create-microsoft-entra-application-02.png" />

- **アプリケーションの登録** で **名前**、**サポートされているアカウントの種類** を指定

  - **名前**: GitHubActions (任意)

  - **サポートされているアカウントの種類**: この組織ディレクトリのみに含まれるアカウント

    <img src="./images/create-microsoft-entra-application-03.png" />

- **登録** をクリックし、アプリケーションを登録

<br />

### Task 2: フェデレーション資格情報の追加

- **証明書とシークレット** を選択、**フェデレーション資格情報** > **＋ 資格情報の追加** をクリック

  <img src="./images/configure-federated-identity-02.png" />

- **資格情報の追加**

  - **フェデレーション資格情報のシナリオ** で **Azure リソースをデプロイする GitHub Actions** を選択

    <img src="./images/configure-federated-identity-03.png" />

  - 接続先の GitHub アカウント、資格情報の詳細を入力

    - **GitHub アカウントを接続する**

      - **組織**: GitHub アカウント

      - **リポジトリ**: RAG-on-Azure-Hands-on-Lab

      - **エンティティ型**: ブランチ

      - **サブジェクト識別子**: main

    - **資格情報の詳細**

      - **名前**: 任意

        <img src="./images/configure-federated-identity-04.png" />

- **追加** をクリックして、資格情報を生成

  <img src="./images/configure-federated-identity-05.png" />

<br />

### Task 3: ロールの割り当て

- リソース グループの **アクセス制御 (IAM)** を選択し、**追加** > **ロール割り当ての追加** をクリック

  <img src="./images/role-assignment-01.png" />

- **特権管理者ロール** > **共同作成者** を選択

  <img src="./images/role-assignment-02.png" />

- **アクセスの割当先** で **ユーザー、グループ、またはサービス プリンシパル** を選択、**＋ メンバーを選択する** をクリック

  <img src="./images/role-assignment-03.png" />

- 登録したアプリケーション名を入力

- 検出されたサービス プリンシパルを選択し **選択** をクリック

  <img src="./images/role-assignment-04.png" />

- **レビューと割り当て** をクリックし、ロールを割り当て

  <img src="./images/role-assignment-05.png" />

<br />

### Task 4: GitHub シークレットの作成

- Web ブラウザで GitHub アカウントのリポジトリを表示

- **Settings** をクリック

  <img src="./images/create-github-secrets-03.png" />

- **Secrets and variables** > **Actions** を選択、**New repository secret** をクリック

  <img src="./images/create-github-secrets-04.png" />

- **Name**, **Secret** を入力し **Add secret** をクリック

  <img src="./images/create-github-secrets-05.png" />

  - 登録するシークレット

    |Name|Secret|
    |---|---|
    |AZURE_CLIENT_ID|アプリケーション (クライアント) ID|
    |AZURE_TENANT_ID|ディレクトリ (テナント) ID|
    |AZURE_SUBSCRIPTION_ID|サブスクリプション ID|

  - アプリケーション (クライアント) ID, ディレクトリ (テナント) ID は、登録したアプリケーションの概要から取得

    <img src="./images/create-github-secrets-01.png" />

  - サブスクリプション ID は、サブスクリプションの概要から取得

    <img src="./images/create-github-secrets-02.png" />

<br />

### Task 5: OpenID Connect 認証を使用した Azure ログイン アクションの使用

- Visual Studio Code の Explorer から **.github** > **workflows** 内のワークフロー ファイルを選択

- Azure ログイン アクションの確認

  <img src="./images/create-github-secrets-07.png" />

  > GitHub Actions と Microsoft Entra ID 間でフェデレーション ID を用いた信頼関係を設定  
  > OpenID Connect の認証でシークレットを使わないセキュアな構成が可

<br />

### 参考情報

- [GitHub Actions を使用して Azure に接続する](https://learn.microsoft.com/ja-jp/azure/developer/github/connect-from-azure?tabs=azure-portal%2Cwindows)

<br />
