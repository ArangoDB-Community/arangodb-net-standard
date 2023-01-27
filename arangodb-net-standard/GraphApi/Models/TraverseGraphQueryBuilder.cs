using ArangoDBNetStandard.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class TraverseGraphQueryBuilder
    {
        private readonly IApiClientSerialization _serialization;
        private readonly ApiClientSerializationOptions _serializationOptions;
        private readonly TraverseGraphQueryParts _qp;

        /// <summary>
        /// Instantiates the TraverseGraphQueryBuilder
        /// </summary>
        /// <param name="serialization">Serialization to use during the query building process.</param>
        /// <param name="serializationOptions">Serialization options to use during the query building process.</param>
        public TraverseGraphQueryBuilder(IApiClientSerialization serialization = null, ApiClientSerializationOptions serializationOptions = null)
        {
            _serialization = serialization ?? new JsonNetApiClientSerialization();
            _serializationOptions = serializationOptions ?? new ApiClientSerializationOptions(true, true);
            _qp = new TraverseGraphQueryParts();
        }

        /// <summary>
        /// List of vertex collections that will be involved in the traversal
        /// </summary>
        TraverseGraphQueryBuilder VertexCollections(List<string> collections)
        {
            if (collections == null)
            {
                throw new ArgumentNullException(nameof(collections));
            }
            
            _qp.VertexCollections = collections;
            return this;
        }

        /// <summary>
        /// The current vertex in a traversal
        /// </summary>
        TraverseGraphQueryBuilder CurrentVertex(object currentVertex)
        {
            if (currentVertex == null)
            {
                throw new ArgumentNullException(nameof(currentVertex));
            }

            _qp.CurrentVertex = currentVertex;
            return this;
        }

        /// <summary>
        /// The current edge in a traversal
        /// </summary>
        TraverseGraphQueryBuilder CurrentEdge(object currentEdge)
        {
            if (currentEdge == null)
            {
                throw new ArgumentNullException(nameof(currentEdge));
            }

            _qp.CurrentEdge = currentEdge;
            return this;
        }

        /// <summary>
        /// Representation of the current path
        /// </summary>
        TraverseGraphQueryBuilder CurrentPath(TraverseGraphPath traverseGraphPath)
        {
            if (traverseGraphPath == null)
            {
                throw new ArgumentNullException(nameof(traverseGraphPath));
            }

            _qp.CurrentPath = traverseGraphPath;
            return this;
        }

        /// <summary>
        /// Edges and vertices returned by the query will start
        /// at the traversal depth of min (thus edges and vertices
        /// below will not be returned). 
        /// If not specified, it defaults to 1. 
        /// The minimal possible value is 0.
        /// </summary>
        TraverseGraphQueryBuilder MinDepth(int? minDepth)
        {
            if (minDepth == null)
            {
                throw new ArgumentNullException(nameof(minDepth));
            }

            _qp.MinDepth = minDepth;
            return this;
        }

        /// <summary>
        /// Defines the max depth/length of paths that are traversed. 
        /// If omitted, it defaults to <see cref="MinDepth"/>.
        /// This cannot be specified without <see cref="MinDepth"/>.
        /// </summary>
        TraverseGraphQueryBuilder MaxDepth(int? maxDepth)
        {
            if (maxDepth == null)
            {
                throw new ArgumentNullException(nameof(maxDepth));
            }

            _qp.MaxDepth = maxDepth;
            return this;
        }

        /// <summary>
        /// Follow outgoing, incoming, or edges pointing in either 
        /// direction in the traversal.
        /// Possible values are <see cref="TraversalDirection.Any"/>,
        /// <see cref="TraversalDirection.Inbound"/>, and 
        /// <see cref="TraversalDirection.Outbound"/>
        /// </summary>
        TraverseGraphQueryBuilder Direction(string direction)
        {
            if (direction == null)
            {
                throw new ArgumentNullException(nameof(direction));
            }

            _qp.Direction = direction;
            return this;
        }

        /// <summary>
        /// A vertex where the traversal will originate from. This 
        /// can be specified in the form of an ID string or in the 
        /// form of a document with the attribute _id (i.e. new { _id = "ABC" })
        /// </summary>
        TraverseGraphQueryBuilder StartVertex(object startVertex)
        {
            if (startVertex == null)
            {
                throw new ArgumentNullException(nameof(startVertex));
            }

            _qp.StartVertex = startVertex;
            return this;
        }

        /// <summary>
        /// The name of the graph.
        /// </summary>
        TraverseGraphQueryBuilder GraphName(string graphName)
        {
            if (graphName == null)
            {
                throw new ArgumentNullException(nameof(graphName));
            }

            _qp.GraphName = graphName;
            return this;
        }

        /// <summary>
        /// Edge collection sets that will be involved in 
        /// the traversal instead of a named graph.
        /// </summary>
        TraverseGraphQueryBuilder EdgeCollections(List<string> edgeCollections)
        {
            if (edgeCollections == null)
            {
                throw new ArgumentNullException(nameof(edgeCollections));
            }

            _qp.EdgeCollections = edgeCollections;
            return this;
        }

        /// <summary>
        /// A condition, like in a FILTER statement, which will be evaluated 
        /// in every step of the traversal, as early as possible.
        /// </summary>
        TraverseGraphQueryBuilder PruneCondition(string pruneCondition)
        {
            if (pruneCondition == null)
            {
                throw new ArgumentNullException(nameof(pruneCondition));
            }

            _qp.PruneCondition = pruneCondition;
            return this;
        }

        /// <summary>
        /// Options to modify the execution of the traversal.
        /// </summary>
        TraverseGraphQueryBuilder Options(TraverseGraphOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _qp.Options = options;
            return this;
        }

        /// <summary>
        /// Builds the query string for the graph traversal.
        /// </summary>
        /// <returns>The query string to traverse the graph</returns>        
        public async Task<string> Build()
        {
            if (_qp.CurrentVertex == null)
            {
                throw new Exception($"{nameof(_qp.CurrentVertex)} cannot be null.");
            }

            //build the query string
            var qs = string.Empty;

            if (_qp.VertexCollections != null && _qp.VertexCollections.Count > 0)
            {
                qs += $"WITH {string.Join(",", _qp.VertexCollections)} {Environment.NewLine}";
            }

            qs += $"FOR {await GetContentStringAsync(_qp.CurrentVertex)} ";

            if (_qp.CurrentEdge != null)
            {
                qs += $", {await GetContentStringAsync(_qp.CurrentEdge)} ";
            }

            if (_qp.CurrentPath != null)
            {
                qs += $", {await GetContentStringAsync(_qp.CurrentPath)} ";
            }

            qs += Environment.NewLine;

            if (_qp.MinDepth != null)
            {
                qs += $"IN {_qp.MinDepth}";
                if (_qp.MaxDepth != null)
                {
                    qs += $"..{_qp.MaxDepth}";
                }
                qs += Environment.NewLine;
            }

            qs += $"{(string.IsNullOrWhiteSpace(_qp.Direction) ? TraversalDirection.Any : _qp.Direction)} {_qp.StartVertex ?? string.Empty} {Environment.NewLine}";

            if (string.IsNullOrWhiteSpace(_qp.GraphName))
            {
                //we're working with a set of edge collections, make sure we have them
                if (_qp.EdgeCollections == null || _qp.EdgeCollections.Count < 1)
                {
                    throw new Exception($"{nameof(_qp.EdgeCollections)} is required if {nameof(_qp.GraphName)} is not specified");
                }
                else
                {
                    qs += $"{string.Join(",", _qp.EdgeCollections)}{Environment.NewLine}";
                }
            }
            else
            {
                //we're working with a named graph
                qs += $"'{_qp.GraphName}'{Environment.NewLine}";
            }

            if (!string.IsNullOrWhiteSpace(_qp.PruneCondition))
            {
                qs += $"PRUNE {_qp.PruneCondition}{Environment.NewLine}";
            }

            if (_qp.Options != null)
            {
                qs += $"OPTIONS {await GetContentStringAsync(_qp.Options)}";
            }
            return qs;
        }

        async Task<string> GetContentStringAsync<T>(T item)
        {
            try
            {
                return await _serialization.SerializeToStringAsync(item, _serializationOptions).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new SerializationException($"A serialization error occured while preparing a request for Arango. See InnerException for more details.", e);
            }
        }
    }
}