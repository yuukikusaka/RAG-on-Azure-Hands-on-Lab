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

- AI Foundry ハブ・プロジェクトの作成（既存の AOAI リソースに接続）

- Storage リソースに対してログイン中のユーザーに `Storage File Data Privileged Contributor` の権限を付与

- Application Insights を接続

- `.env` に `APPLICATIONINSIGHTS_CONNECTION_STRING` 環境変数を設定

- TODO: C# がトレースは送信できている（Application Insights では確認できている）が、AI Foundry 上から確認できない

## Appendix 3: Azure OpenAI、Document intelligence を使った評価用データセットを用いた生成 AI の評価

- Notebook 実行

- プロジェクトから**評価**を実行（「関連度」の評価）

- TODO: 根拠性に関するデータ合成