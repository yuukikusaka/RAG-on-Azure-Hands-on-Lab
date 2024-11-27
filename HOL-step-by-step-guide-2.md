<img src="./images/ms-cloud-workshop.png" />

Retrieval Augmented Generation (RAG) pattern for Azure AI Search  
Dec 2024

<br />

### Contents

- [Exercise 2: 生成 AI アプリケーションの動作確認](#exercise-2-生成-AI-アプリケーションの動作確認)

## Exercise 2: 生成 AI アプリケーションの動作確認

<br />

### Task 1: Azure AI Foundry プレイグラウンドを用いた動作確認

- `gpt-4o` デプロイ

- `text-embedding-3-large` デプロイ

- Azure AI Foundry プレイグラウンドでチャット（`gpt-4o`）

### Task 2: 作業用端末での動作確認

<details>
<summary>Python</summary>

- `app/python/simple/.env` ファイルに環境変数をセット

- 作業用端末にコンテナイメージをビルドし、実行

```shell
cd app/python/simple
docker build -t python-simple:0.0.1 .
docker run -p 8000:8000 python-simple:0.0.1
```

- ブラウザからチャット API をコール

```
http://localhost:8000/chat?query=こんにちは
```

- ブラウザから検索 API をコール

```
http://localhost:8000/search/fulltext?query=AOAIとは
```

</details>

<details>
<summary>C#</summary>

- `app/csharp/simple/.env` ファイルに環境変数をセット

- 作業用端末にコンテナイメージをビルドし、実行

```shell
cd app/csharp/simple
docker build -t csharp-simple:0.0.1 .
docker run -p 8080:8080 csharp-simple:0.0.1
```

- ブラウザからチャット API をコール

```
http://localhost:8080/chat?query=こんにちは
```

- ブラウザから検索 API をコール

```
http://localhost:8080/search/fulltext?query=AOAIとは
```

</details>