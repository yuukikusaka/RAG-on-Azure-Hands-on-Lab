import os
from dotenv import load_dotenv
from fastapi import FastAPI

from service.aoai_service import get_embedding_from_query, rewrite_query, generate_answer, chat

from service.ai_search_service import search_fulltext, search_vector


load_dotenv()


app = FastAPI()


@app.get("/health")
def get_health():
    """
    ヘルスチェックエンドポイント。

    Returns:
        dict: アプリケーションのステータスを含む辞書。
    """
    return {"status": "ok"}


@app.get("/chat")
def chat_handler(query: str):
    """
    クエリを処理し、回答を返すチャットエンドポイント。

    Args:
        query (str): 処理するクエリ文字列。

    Returns:
        dict: クエリに対する回答を含む辞書。
    """
    answer = chat(query)
    return {"answer": answer}


@app.get("/search/fulltext")
def search_fulltext_handler(query: str):
    """
    クエリを処理し、書き換え、検索を実行し、回答を生成する
    フルテキスト検索エンドポイント。

    Args:
        query (str): 処理するクエリ文字列。

    Returns:
        dict: クエリに対する回答を含む辞書。
    """
    rewritten_query = rewrite_query(query)
    search_results = search_fulltext(rewritten_query)
    answer = generate_answer(query, search_results)
    return {"answer": answer}


# TODO: In-Progess
@app.get("/search/vector")
def search_vector_handler(query: str):
    """
    ベクトルを処理し、検索を実行し、回答を生成する
    ベクトル検索エンドポイント。

    Args:
        query (str): 処理するクエリ文字列。

    Returns:
        dict: クエリに対する回答を含む辞書。
    """
    rewritten_query = rewrite_query(query)
    vector = get_embedding_from_query(rewritten_query)
    search_results = search_vector(vector)
    answer = generate_answer("", search_results)
    return {"answer": answer}