<img src="./images/ms-cloud-workshop.png" />

Retrieval Augmented Generation (RAG) pattern for Azure AI Search  
Dec 2024

<br />

### Contents

- AI Foundry を使った運用

<br />

## Appendix 1: Azure OpenAI 使用状況の確認

- デプロイ、モデルの選択、メトリックからリクエスト数・トークンの使用状況・リスクと安全性を確認

## Appendix 2: トレースを使用したアプリケーションのデバッグ

- AI Foundry ハブを作成

    - Azure Portal の検索バーに `AI Project` と入力し、**Azure AI Studio** を選択

    - 以下の項目を入力

        - **基本**

            - **サブスクリプション**: `ワークショップで使用中のサブスクリプション`
            - **リソースグループ**: `ワークショップで使用中のリソースグループ`
            - **リージョン**: `ワークショップで使用中のリージョン`
            - **名前**: `任意 (hub-cloudworkshop)`
            - **フレンドリ名**: `任意 (Hub cloudworkshop)`
            - **既定のプロジェクト リソース グループ**: `ワークショップで使用中のリソースグループ`
            - **OpenAI を含む AI サービスに接続する**: `展開済みの Azure OpenAI リソース (oai-mcwfy25q2gxx)`

            <img src="./images/appendix-02.png" />
        
        - **ストレージ**

            - **ストレージ アカウント**: `既定 (新規)`
            - **Credential store**: `Azure key vault`
                - **キー コンテナー**: `既定 (新規)`
            - **Application Insights**: `展開済みの Application Insights (appi-mcwfy25q2gxxxx)`
            - **コンテナー レジストリ**: `なし`

            <img src="./images/appendix-03.png" />

        - **ネットワーク**: `パブリック`

        - **暗号化**、**ID**、**タグ** は既定のまま

        - **確認および作成**を選択し、**作成**を押下

    - リソースが正常に作成されたことを確認し、**Azure AI Studio の起動**を選択

    <img src="./images/appendix-04.png" />

    - プロジェクト名（例: `prj-cloudworkshop`）を入力し、**プロジェクトの作成**を選択

    <img src="./images/appendix-05.png" />

- Storage リソースに対してログイン中のユーザーに `Storage File Data Privileged Contributor` の権限を付与

    - 展開されたストレージアカウント（例: `hubcloudworkshxxxxx`）の **アクセス制御 (IAM)**から **+ 追加** > **ロールの割り当てを追加**を選択

    <img src="./images/appendix-06.png" />

    - **Storage File Data Privileged Contributor** を選択し、**次へ**を押下

    <img src="./images/appendix-07.png" />

    - メンバータブでワークショップで使用中のユーザーを選択し、**次へ**を押下

    - **レビューと割り当て**を押下

- `.env` に `APPLICATIONINSIGHTS_CONNECTION_STRING` 環境変数を設定

    - Application Insights の概要ページから、**接続文字列** (`InstrumentationKey=xxxxx`)をコピー

    <details>
    <summary>Python</summary>

    - `app/python/simple/.env` の `APPLICATIONINSIGHTS_CONNECTION_STRING` に、コピーした Application Insights の接続文字列をセット

    - 作業用端末にコンテナイメージをビルドし、実行

    ```shell
    cd ./app/python/simple
    docker build -t python-simple:0.0.1 .
    docker run -p 8000:8000 python-simple:0.0.1
    ```

    - ブラウザからチャット API をコールし、レスポンスを取得することを確認

    > GET /chat?query={input} で、上記手順で試した会話を行うチャットボットとの対話を実施可能。APIは実装済み。

    ```
    http://localhost:8000/chat?query=こんにちは
    ```

    </details>

    <details>
    <summary>C#</summary>


    - `app/csharp/simple/.env` の `APPLICATIONINSIGHTS_CONNECTION_STRING` に、コピーした Application Insights の接続文字列をセット

    - 作業用端末にコンテナイメージをビルドし、実行

    ```shell
    cd ./app/csharp/simple
    docker build -t csharp-simple:0.0.1 .
    docker run -p 8080:8080 csharp-simple:0.0.1
    ```

    - ブラウザからチャット API をコールし、レスポンスを取得することを確認

    > GET /chat?query={input} で、上記手順で試した会話を行うチャットボットとの対話を実施可能。APIは実装済み。

    ```
    http://localhost:8080/chat?query=こんにちは
    ```

    </details>

## Appendix 3: Azure OpenAI、Document intelligence を使った評価用データセットを用いた生成 AI の評価

- Notebook 実行

- プロジェクトから**評価**を実行（「関連度」の評価）

- TODO: 根拠性に関するデータ合成