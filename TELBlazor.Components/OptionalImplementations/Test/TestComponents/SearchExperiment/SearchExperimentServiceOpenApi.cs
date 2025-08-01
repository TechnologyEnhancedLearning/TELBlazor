using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TELBlazor.Components.OptionalImplementations.Test.TestComponents.SearchExperiment
{
    //curl -X 'GET' \
    //  'https://lh-openapi.dev.local/Search/GetAutoSuggestionResult/blood' \
    //  -H 'accept: */*' \
    //  -H 'Authorization: Bearer '

//    {
//  "stats": {
//    "totalHits": 23,
//    "searchEngineTimeInMillis": 3,
//    "searchEngineRoundTripTimeInMillis": 4,
//    "searchProcessingTimeInMillis": 5
//  },
//  "catalogueDocument": {
//    "id": "catalogues_documents",
//    "totalHits": 0,
//    "catalogueDocumentList": []
//  },
//  "conceptDocument": {
//    "id": "concepts_documents",
//    "totalHits": 6,
//    "conceptDocumentList": [
//      {
//        "id": "blood transfusion_hee-local",
//        "title": "426 Blood Transfusion - Non Registered Staff, Mandatory Training Workbook 2019-20",
//        "concept": "blood transfusion",
//        "click": {
//            "payload": {
//                "searchSignal": {
//                    "stats": {
//                        "totalHits": 23
//                    },
//              "searchId": "5b3e2cb9-2a6b-4d9a-8879-a7ba336abf3e",
//              "profileSignature": {
//                        "applicationId": "HEE",
//                "profileType": "SEARCHER",
//                "profileId": "auto-suggestion-local"
//              },
//              "userQuery": "q=blood",
//              "query": "q=blood",
//              "timeOfSearch": 1750247278498
//                },
//            "hitNumber": 4,
//            "clickTargetUrl": "blood transfusion_hee-local",
//            "documentFields": {
//                    "name": null,
//              "title": "426 Blood Transfusion - Non Registered Staff, Mandatory Training Workbook 2019-20"
//            },
//            "containerId": "concepts_documents",
//            "timeOfClick": null
//            },
//          "url": "/signals/hee/signal/click-hee"
//        }
//    }
//    ]
//  },
//  "resourceDocument": {
//    "id": "resources_documents",
//    "totalHits": 17,
//    "resourceDocumentList": [
//      {

//    }
//    ]
//  }
//}
    public class SearchExperimentServiceOpenApi :ISearchExperimentService
    {
        //qqqq can do alot better
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<SearchExperimentServiceOpenApi> _logger; // logger

        public SearchExperimentServiceOpenApi(HttpClient httpClient, IConfiguration configuration, ILogger<SearchExperimentServiceOpenApi> logger)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["SearchApi:BaseUrl"] ?? "https://lh-openapi.dev.local";
            _logger = logger;

            //var token = configuration["SearchApi:BearerToken"];
            //if (!string.IsNullOrEmpty(token))
            //{
            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
        }
        public async Task<IEnumerable<string>> GetSuggestionsAsync(string term, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = $"{_baseUrl}/Search/GetAutoSuggestionResult/{term}";
                var response = await _httpClient.GetFromJsonAsync<SearchResponse>(url);
                _logger.LogWarning(JsonSerializer.Serialize(response));
                return response?.conceptDocument?.conceptDocumentList?
                    .Select(x => x.title)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    ?? Enumerable.Empty<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSuggestionsAsync: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }

        public async Task<IEnumerable<string>> SearchAsync(string term, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = $"{_baseUrl}/Search/GetAutoSuggestionResult/{term}";
                var response = await _httpClient.GetFromJsonAsync<SearchResponse>(url);
                _logger.LogWarning(JsonSerializer.Serialize(response));
                return response?.conceptDocument?.conceptDocumentList?
            .Select(x => x.click?.payload?.clickTargetUrl)
            .Where(url => !string.IsNullOrWhiteSpace(url)).DefaultIfEmpty("Soz nout found. ⚠️I'm actually just hitting the suggestion endpoint atm. coz t'uther bokked. ⚠️").ToList<string>()
            ?? new List<string>() { "Soz nout found. ⚠I'm actually just hitting the suggestion endpoint atm.coz t'uther bokked. ⚠️" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchAsync: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }

        private class SearchResponse
        {
            public ConceptDocument conceptDocument { get; set; }
            // You could also add resourceDocument or catalogueDocument if needed
        }

        private class ConceptDocument
        {
            public List<ConceptItem> conceptDocumentList { get; set; }
        }

        private class ConceptItem
        {
            //public string id { get; set; }
            public string title { get; set; }
            //public string concept { get; set; }
            public Click click { get; set; }
        }

        private class Click
        {
            public Payload payload { get; set; }
            //public string url { get; set; }
        }

        private class Payload
        {
            public string clickTargetUrl { get; set; }
        }

    }

}

