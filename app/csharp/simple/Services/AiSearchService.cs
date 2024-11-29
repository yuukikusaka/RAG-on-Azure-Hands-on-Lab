using System;
using System.Collections.Generic;

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using Simple.Services.Helpers;

namespace Simple.Services
{
    /// <summary>
    /// Azure AI Searchに接続するためのSearchClientを構築します。
    /// </summary>
    public class SearchClientBuilder
    {
        /// <summary>
        /// SearchClientインスタンスを構築して返します。
        /// </summary>
        /// <returns>SearchClientインスタンス。</returns>
        /// <exception cref="InvalidOperationException">検索サービスの設定が不足している場合にスローされます。</exception>
        public SearchClient BuildSearchClient()
        {
            var searchServiceName = Environment.GetEnvironmentVariable("AI_SEARCH_SERVICE_NAME");
            var indexName = Environment.GetEnvironmentVariable("AI_SEARCH_INDEX_NAME");
            var apiKey = HelperMethods.GetSecretFromKeyVault("ai-search-api-key");

            if (string.IsNullOrEmpty(searchServiceName) || string.IsNullOrEmpty(indexName) || string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Search service configuration is missing.");
            }

            var serviceEndpoint = $"https://{searchServiceName}.search.windows.net/";
            var credential = new AzureKeyCredential(apiKey);

            return new SearchClient(new Uri(serviceEndpoint), indexName, credential);
        }
    }

    /// <summary>
    /// Azure AI Searchを使用してAI検索サービスを提供します。
    /// </summary>
    public class AiSearchService
    {
        private readonly SearchClientBuilder _searchClientBuilder;

        /// <summary>
        /// <see cref="AiSearchService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="searchClientBuilder">SearchClientBuilderインスタンス。</param>
        public AiSearchService(SearchClientBuilder searchClientBuilder)
        {
            _searchClientBuilder = searchClientBuilder;
        }

        /// <summary>
        /// 指定されたクエリを使用して全文検索を実行します。
        /// </summary>
        /// <param name="query">検索クエリ。</param>
        /// <returns>検索ドキュメントのリスト。</returns>
        public List<SearchDocument> FullTextSearch(string query)
        {
            var searchClient = _searchClientBuilder.BuildSearchClient();
            var options = new SearchOptions
            {
                Size = 5
            };
            options.Select.Add("keyphrases");
            options.Select.Add("content");
            options.Select.Add("text");
            SearchResults<SearchDocument> results = searchClient.Search<SearchDocument>(query, options);

            List<SearchDocument> searchResults = new List<SearchDocument>();
            foreach (SearchResult<SearchDocument> result in results.GetResults())
            {
                searchResults.Add(result.Document);
            }
            return searchResults;
        }
    }
}