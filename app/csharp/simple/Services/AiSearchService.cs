using System;
using System.Collections.Generic;

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;

namespace Simple.Services
{
    public class SearchClientBuilder
    {
        public SearchClient BuildSearchClient()
        {
            var searchServiceName = Environment.GetEnvironmentVariable("AI_SEARCH_SERVICE_NAME");
            var indexName = Environment.GetEnvironmentVariable("AI_SEARCH_INDEX_NAME");
            var apiKey = Environment.GetEnvironmentVariable("AI_SEARCH_API_KEY");

            if (string.IsNullOrEmpty(searchServiceName) || string.IsNullOrEmpty(indexName) || string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Search service configuration is missing.");
            }

            var serviceEndpoint = $"https://{searchServiceName}.search.windows.net/";
            var credential = new AzureKeyCredential(apiKey);

            return new SearchClient(new Uri(serviceEndpoint), indexName, credential);
        }
    }

    public class AiSearchService
    {
        private readonly SearchClientBuilder _searchClientBuilder;

        public AiSearchService(SearchClientBuilder searchClientBuilder)
        {
            _searchClientBuilder = searchClientBuilder;
        }

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