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

  [!CAUTION]
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

  [!CAUTION]
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

    [!NOTE]
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

  | フィールド名 | 取得可能 | フィルター可能 | ソート可能 | ファセット可能 | 検索可能 | アナライザー | Suggester |
  | --- | :---: | :---: | :---: | :---: | :---: | --- | :---: |
  |contnt |●||||●| [日本語 ‐ Microsoft アナライザー](#font-size 10px) ||
  |metadata_storage_size|●|●|●|●||||
  |metadata_storage_last_modified|●|●|●|●||||
  |metadata_storage_name|●|||||||
  |metadata_storage_file_extension|●|●|●|●||||
  |people|●|●||●|●|日本語 ‐ Microsoft アナライザー||
  |organizations|●|●||●|●|日本語 ‐ Microsoft アナライザー||
  |locations|●|●||●|●|日本語 ‐ Microsoft アナライザー||
  |keyphrases|●|●||●|●|日本語 ‐ Microsoft アナライザー||
  |language|●|●|●|●|●|標準 ‐ Lucene||
  |merged_content|●||||●|日本語 ‐ Microsoft アナライザー|●|
  |text||||||||
  |layoutText|●||||●|日本語 ‐ Microsoft アナライザー||
  |imageTags|●|●||●|●|標準 ‐ Lucene||
  |imageTags|●|●||●|●|標準 ‐ Lucene||

- **次: インデクサーの作成** をクリック

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