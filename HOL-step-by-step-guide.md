<img src="./images/ms-cloud-workshop.png" />

Retrieval Augmented Generation (RAG) pattern for Azure AI Search  
Dec 2024

<br />

### Contents

- [環境準備](#環境準備)

<br />

### 事前準備環境

<img src="./images/preparation.png" />

<br />

## 環境準備

<img src="./images/dev_environment.png" />

<br />

#### 仮想マシンへの接続

- [Azure ポータル](https://portal.azure.com/) から展開済みの仮想マシンへ管理ブレードを表示

- **接続** > **Bastion を介した接続** を選択

  <img src="./images/connect-to-azure-vm-01.png" />

- ユーザー名、パスワードを入力し **接続** をクリック

  <img src="./images/connect-to-azure-vm-02.png" />

- 新しいタブが開き、仮想マシンのデスクトップ画面が表示

  <img src="./images/connect-to-azure-vm-03.png" />

  > [!CAUTION]
  > 初回接続時にポップアップ ブロック機能により画面が表示されない場合:  
  > ポップアップ ブロックのアイコンをクリックし、ポップアップとリダイレクトを許可したのち再度接続を実行

<br />

#### GitHub リポジトリのフォーク

- Web ブラウザを起動、[ワークショップのリポジトリ](https://github.com/kohei3110/RAG-on-Azure-Hands-on-Lab) へアクセス

- **Fork** をクリック

  <img src="./images/github-fork-01.png" />

- **Owner** に自身のアカウントが表示、**Copy the main branch only** にチェックがつけられていることを確認し **Create fork** をクリック

  <img src="./images/github-fork-02.png" />

- リポジトリが複製されることを確認

  <img src="./images/github-fork-03.png" />

<br />

#### リポジトリのクローン

- リポジトリの **Code** をクリック、表示されるツールチップよりリポジトリの URL をコピー

  <img src="./images/github-clone-01.png" />

- Visual Studio Code を起動、サイドバーから **Explorer** を選択し **Clone Repository** をクリック

  <img src="./images/github-clone-02.png" />

- リポジトリの URL にコピーした URL を貼り付け Enter キーを押下

  <img src="./images/github-clone-03.png" />

- 複製先となるローカル ディレクトリを選択

  > [!CAUTION]
  > GitHub の認証情報が求められた場合は、資格情報を入力し認証を実施

- クローンされたリポジトリを開きますか？のメッセージが表示されるので **Open** をクリック

  <img src="./images/github-clone-04.png" />

- **Terminal** > **New Terminal** を選択

- git remote コマンドを実行し  クローン先である自身のアカウント名を含む URL が表示されることを確認

  ```
  git remote -v
  ```

<br />

## Exercise 1: Azure AI Search の使用方法

### Task 1: インデックスの作成

- Azure AI Search の管理ブレードへ移動

- **データのインポート** をクリック

  <img src="./images/create-index-01.png" />

- **データに接続します** タブで必要項目を入力

  <img src="./images/create-index-02.png" />

  - **データ ソース**: Azure BLOB ストレージ

  - **データ ソース名**: index-blob (任意)

  - **抽出されるデータ**: コンテンツとメタデータ

  - **解析モード**: 既定

  - **サブスクリプション**: ワークショップで使用中のサブスクリプション

  - **接続文字列**: **既定の接続を選択します** をクリックし、ドキュメントが格納されている BLOB コンテナを選択

    <img src="./images/create-index-03.png" />

  - **マネージド ID の認証**: なし

  - **コンテナー名**: 接続文字列で選択したコンテナー名が自動入力

  - **説明**: 任意

- **次: コグニティブ スキルを追加します (省略可能)** をクリック

- **コグニティブ スキルを追加します** タブで必要項目を選択

  - **AI サービスをアタッチする** で **無料 (制限付きのエンリッチメント)** が表示されていることを確認

    <img src="./images/create-index-04.png" />

    > [!NOTE]
    > 無料リソースは、インデクサーあたり 1 日 20 個のドキュメントまでのエンリッチに制限

  - **エンリッチメントの追加**

    <img src="./images/create-index-05.png" />

    - **スキルセット名**: azureblob-skillset (任意)

    - **OCR を有効にし、すべてのテキストを merged_content フィールドにマージする**: チェック

    - **ソース データ フィールド**: merged_content

    - **テキストの認知技術** で以下の項目をチェック (フィールド名は既定のままで OK)

      - **ユーザー名を抽出**

      - **組織名を抽出**

      - **場所の名前を抽出**

      - **キーフレーズを抽出**

      - **言語の検出**

    - **画像の認知技術**

      - **画像からタグを生成する**

      - **画像からキャプションを生成する**

- **次: 対象インデックスをカスタマイズします** をクリック

- **対象インデックスをカスタマイズします** タブでインデックスとして格納するフィールドを設定

  | フィールド名 | 取得<br />可能 | フィルター<br />可能 | ソート<br />可能 | ファセット<br />可能 | 検索<br />可能 | アナライザー | Suggester |
  | --- | :---: | :---: | :---: | :---: | :---: | --- | :---: |
  |contnt|●||||●|日本語 ‐ Microsoft||
  |metadata_storage_size|●|●|●|●||||
  |metadata_storage_last_modified|●|●|●|●||||
  |metadata_storage_name|●|||||||
  |metadata_storage_file_extension|●|●|●|●||||
  |people|●|●||●|●|日本語 ‐ Microsoft||
  |organizations|●|●||●|●|日本語 ‐ Microsoft||
  |locations|●|●||●|●|日本語 ‐ Microsoft||
  |keyphrases|●|●||●|●|日本語 ‐ Microsoft||
  |language|●|●|●|●|●|標準 ‐ Lucene||
  |merged_content|●||||●|日本語 ‐ Microsoft|●|
  |text|●||||●|日本語 ‐ Microsoft||
  |layoutText|●||||●|日本語 ‐ Microsoft||
  |imageTags|●|●||●|●|標準 ‐ Lucene||
  |imageTags|●|●||●|●|標準 ‐ Lucene||

- **次: インデクサーの作成** をクリック

- **インデクサーの作成** タブで必要項目を指定

  <img src="./images/create-index-06.png" />

  - **インデクサー**

    - **名前**: azureblob-indexer (任意)

    - **スケジュール**: １度

  - **詳細オプション**

    - **Base-64 エンコード キー**: オン

- **送信** をクリックし、インデクサーを作成

- Azure AI Search の管理ブレードで **検索管理** > **インデクサー** を選択

  <img src="./images/create-index-07.png" />

  > [!NOTE]
  > ステータスに **成功** と表示されており、インデキシングが完了していることを確認

<br />

### Task 2: インデックス、スキルセットの設定

- **検索管理** - **インデックス** を選択、インデックス名をクリック

- **CORS** タブをクリック

- **許可されたオリジンの種類** で **すべて** を選択し **保存** をクリック

  <img src="./images/create-index-08.png" />

  > [!NOTE]
  > クライアントの JavaScript から API の呼び出しを許可するために CORS を有効化

- **検索管理** > **スキルセット** を選択、スキルセット名をクリック

  > [!NOTE]
  > スキルセット ＝ 各スキルの定義とその実行順を JSON 形式で定義したもの

- OCR スキルの言語、キーフレーズ抽出スキルの上限、エンティティ認識スキルの信頼度スコアの閾値を設定

  <img src="./images/create-index-09.png" />

  - OCR スキル (Microsoft.Skills.Vision.OcrSkill) の defaultLanguageCode を en から ja に変更

    ```
    "defaultLanguageCode": "ja"
    ```

  - キーフレーズ抽出 (Microsoft.Skills.Text.KeyPhraseExtractionsSkill) に追加

    ```
    "defaultLanguageCode": "en",
    "maxKeyPhraseCount": 20
    ```

  - エンティティ認識スキル (Microsoft.Skills.Text.V3.EntityRecognitionSkill) に追加

    ```
    "defaultLanguageCode": "en",
    "minimumPercision": 0.8
    ```

- **保存** をクリック

<br />

### Task 3: インデックスの再作成

- **検索管理** > **インデクサー** を選択、インデクサー名をクリック

- **リセット** をクリックし、インデキシングされたデータをクリア

  <img src="./images/create-index-10.png" />

- インデクサーのリセットのメッセージが表示されるので **はい** をクリック

  <img src="./images/create-index-11.png" />

- インデクサー ｘｘｘ は正常にリセットされましたの通知を確認し **実行** をクリック

- インデクサーを実行のメッセージが表示されるので **はい** をクリック

  <img src="./images/create-index-12.png" />

- **最新の情報に更新** をクリックし、インデックスの再作成が正常に完了したことを確認

  <img src="./images/create-index-13.png" />

<br />

### 参考情報

- [Azure AI Search の概要](https://learn.microsoft.com/ja-jp/azure/search/search-what-is-azure-search)

- [インデクサーの概要](https://learn.microsoft.com/ja-jp/azure/search/search-indexer-overview)

<br />

## Exercise 2: Azure OpenAI Searvice

<br />

## Exercise 3: API

<br />

## Exercise 4: Azure への展開

<br />