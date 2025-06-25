using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TELBlazor.Components.OptionalImplementations.Test.TestComponents.SearchExperiment
{
    public interface ISearchExperimentService
    {

            /// <summary>
            /// Gets search suggestions for a term
            /// </summary>
            /// <param name="term">The search term</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>List of suggestion strings</returns>
            public Task<IEnumerable<string>> GetSuggestionsAsync(string term, CancellationToken cancellationToken = default);

            /// <summary>
            /// Performs a search and returns results
            /// </summary>
            /// <param name="searchTerm">The search term</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>List of search result strings</returns>
            public Task<IEnumerable<string>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
        }

        ///// <summary>
        ///// Extended search service interface for more advanced scenarios
        ///// If you need more complex results, implement this instead
        ///// </summary>
        //public interface IAdvancedSearchService
        //{
        //    /// <summary>
        //    /// Gets search suggestions with metadata
        //    /// </summary>
        //    /// <param name="term">The search term</param>
        //    /// <param name="cancellationToken">Cancellation token</param>
        //    /// <returns>Search suggestions with metadata</returns>
        //    Task<SuggestionResult> GetSuggestionsAsync(string term, CancellationToken cancellationToken = default);

        //    /// <summary>
        //    /// Performs a search with pagination and filtering
        //    /// </summary>
        //    /// <param name="request">The search request</param>
        //    /// <param name="cancellationToken">Cancellation token</param>
        //    /// <returns>Paginated search results</returns>
        //    Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
        //}

    
}
