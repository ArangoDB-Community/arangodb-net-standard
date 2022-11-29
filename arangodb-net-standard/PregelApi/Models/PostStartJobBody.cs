using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ArangoDBNetStandard.PregelApi.Models
{
    /// <summary>
    /// Body parameter for 
    /// <see cref="IPregelApiClient.PostStartJobAsync(PostStartJobBody, System.Threading.CancellationToken)"/>
    /// </summary>
    public class PostStartJobBody
    {
        /// <summary>
        /// Required. Name of the algorithm. 
        /// See <see cref="PregelAlgorithms"/> for possible values.
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// Optional. Name of a graph. Either this or the parameters
        /// <see cref="VertexCollections"/> and <see cref="EdgeCollections"/>
        /// are required.
        /// </summary>
        /// <remarks>
        /// Please note that there are special sharding requirements
        /// for graphs in order to be used with Pregel.
        /// </remarks>
        public string GraphName { get; set; }

        /// <summary>
        ///  List of vertex collection names. 
        /// </summary>
        /// <remarks>
        /// Please note that there are special sharding requirements
        /// for collections in order to be used with Pregel.
        /// </remarks>
        public IEnumerable<string> VertexCollections { get; set; }

        /// <summary>
        /// List of edge collection names. 
        /// </summary>
        /// <remarks>
        /// Please note that there are special sharding requirements
        /// for collections in order to be used with Pregel.
        /// </remarks>
        public IEnumerable<string> EdgeCollections { get; set; }

        /// <summary>
        /// General as well as algorithm-specific options.
        /// </summary>
        /// <remarks>
        /// <see cref="https://www.arangodb.com/docs/stable/http/pregel.html#start-pregel-job-execution"/>
        /// </remarks>
        public Dictionary<string,object> Params { get; set; }
    }
}