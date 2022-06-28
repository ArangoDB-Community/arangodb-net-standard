# GraphApiClient Class
 

A client for interacting with ArangoDB Graphs endpoints, implementing <a href="9cf68195-2611-f408-a78f-ab77864cc844">IGraphApiClient</a>.


## Inheritance Hierarchy
<a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">System.Object</a><br />&nbsp;&nbsp;<a href="1e4d73ca-864e-e82d-2705-3f6909ffa824">ArangoDBNetStandard.ApiClientBase</a><br />&nbsp;&nbsp;&nbsp;&nbsp;ArangoDBNetStandard.GraphApi.GraphApiClient<br />
**Namespace:**&nbsp;<a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public class GraphApiClient : ApiClientBase, 
	IGraphApiClient
```

**VB**<br />
``` VB
Public Class GraphApiClient
	Inherits ApiClientBase
	Implements IGraphApiClient
```

**C++**<br />
``` C++
public ref class GraphApiClient : public ApiClientBase, 
	IGraphApiClient
```

**F#**<br />
``` F#
type GraphApiClient =  
    class
        inherit ApiClientBase
        interface IGraphApiClient
    end
```

The GraphApiClient type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="fc0c9bc6-edf9-a7da-4128-b8132df82aed">GraphApiClient(IApiClientTransport)</a></td><td>
Create an instance of GraphApiClient using the provided transport layer and the default JSON serialization.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="9ad285be-a2d0-03e2-e092-7232b2023b0d">GraphApiClient(IApiClientTransport, IApiClientSerialization)</a></td><td>
Create an instance of GraphApiClient using the provided transport and serialization layers.</td></tr></table>&nbsp;
<a href="#graphapiclient-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="4e94456f-0c7d-85f0-b3d2-99375a49cbe9">DeleteEdgeAsync(T)(String, String, DeleteEdgeQuery)</a></td><td>
Removes an edge based on its document ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="62725a75-b83e-42b6-963c-b14f451d964d">DeleteEdgeAsync(T)(String, String, String, DeleteEdgeQuery)</a></td><td>
Removes an edge from the collection. DELETE /_api/gharial/{graph}/edge/{collection}/{edge}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="b103bad6-da66-b703-54fd-c7bf3b230f8c">DeleteEdgeDefinitionAsync</a></td><td>
Remove one edge definition from the graph. This will only remove the edge collection, the vertex collections remain untouched and can still be used in your queries. DELETE/_api/gharial/{graph}/edge/{definition}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="9c5a039e-c629-ab48-64dc-a47746ffa401">DeleteGraphAsync</a></td><td>
Deletes an existing graph object by name. Optionally all collections not used by other graphs can be deleted as well, using <a href="0cc8a0f1-7816-fbfd-e108-4c550611abec">DeleteGraphQuery</a>. DELETE /_api/gharial/{graph-name}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="7608e87e-276f-c4ac-2d56-521c21447df9">DeleteVertexAsync(T)(String, String, DeleteVertexQuery)</a></td><td>
Removes a vertex based on its document ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="4e2369b9-4b07-ce73-a451-165aacb7c57c">DeleteVertexAsync(T)(String, String, String, DeleteVertexQuery)</a></td><td>
Removes a vertex from the collection. DELETE/_api/gharial/{graph}/vertex/{collection}/{vertex}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="a6a82ef1-7feb-e84f-31e1-92b5521c1d95">DeleteVertexCollectionAsync</a></td><td>
Removes a vertex collection from the graph and optionally deletes the collection, if it is not used in any other graph. It can only remove vertex collections that are no longer part of edge definitions, if they are used in edge definitions you are required to modify those first. DELETE/_api/gharial/{graph}/vertex/{collection}</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="6433c40c-fce1-03fc-1b4c-303e659347e6">DeserializeJsonFromStream(T)</a></td><td> (Inherited from <a href="1e4d73ca-864e-e82d-2705-3f6909ffa824">ApiClientBase</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Equals</a></td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.finalize#system-object-finalize" target="_blank" rel="noopener noreferrer">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="4cb09346-e57e-8f69-7a27-c36bf1686139">GetApiErrorException</a></td><td>
Gets an <a href="0a4502e4-4207-2375-a5f2-66eb56e92746">ApiErrorException</a> from the provided error response.
 (Inherited from <a href="1e4d73ca-864e-e82d-2705-3f6909ffa824">ApiClientBase</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="46aa3cc6-d616-613c-e079-bd04cf831f6c">GetContent(T)</a></td><td> (Inherited from <a href="1e4d73ca-864e-e82d-2705-3f6909ffa824">ApiClientBase</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="444678d9-f9e5-5efd-39f9-fa8df947605c">GetContentString(T)</a></td><td> (Inherited from <a href="1e4d73ca-864e-e82d-2705-3f6909ffa824">ApiClientBase</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="a8b8512d-ba09-fe9c-8dc1-af7c0bc64801">GetEdgeAsync(T)(String, String, GetEdgeQuery)</a></td><td>
Gets an edge from the given graph using the edge's document-handle. GET /_api/gharial/{graph}/edge/{collection}/{edge}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="47730892-d9b0-908a-e719-7a0d5e6a3d7e">GetEdgeAsync(T)(String, String, String, GetEdgeQuery)</a></td><td>
Gets an edge from the given graph using the edge collection and _key attribute.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="1fa2d505-8422-7082-4ea9-5048c64e27b1">GetEdgeCollectionsAsync</a></td><td>
Lists all edge collections within this graph. GET /_api/gharial/{graph}/edge</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="ae0fee81-551f-6ffa-005f-65d7fd124966">GetGraphAsync</a></td><td>
Selects information for a given graph. Will return the edge definitions as well as the orphan collections. GET /_api/gharial/{graph}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="eea4a069-4765-5884-164c-1376afa25134">GetGraphsAsync</a></td><td>
Lists all graphs stored in this database. GET /_api/gharial</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.gethashcode#system-object-gethashcode" target="_blank" rel="noopener noreferrer">GetHashCode</a></td><td>
Serves as the default hash function.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.gettype#system-object-gettype" target="_blank" rel="noopener noreferrer">GetType</a></td><td>
Gets the <a href="https://docs.microsoft.com/dotnet/api/system.type" target="_blank" rel="noopener noreferrer">Type</a> of the current instance.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="22f16a3a-1dee-27d1-4e9f-d5facfb1f6f4">GetVertexAsync(T)(String, String, GetVertexQuery)</a></td><td>
Gets a vertex based on its document ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="b6f8f726-3d16-9f91-fccb-32133486a328">GetVertexAsync(T)(String, String, String, GetVertexQuery)</a></td><td>
Gets a vertex from the given collection. GET/_api/gharial/{graph}/vertex/{collection}/{vertex}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="4cade40a-e866-ebe7-c470-f3726c25c323">GetVertexCollectionsAsync</a></td><td>
Lists all vertex collections within the given graph. GET /_api/gharial/{graph}/vertex</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.memberwiseclone#system-object-memberwiseclone" target="_blank" rel="noopener noreferrer">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="3a5525b6-cbfc-c863-f3d7-f3a70a52a168">PatchEdgeAsync(T, U)(String, String, T, PatchEdgeQuery)</a></td><td>
Updates the data of the specific edge based on its document ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="a0bce150-04ef-4d91-035a-d43e35333d7c">PatchEdgeAsync(T, U)(String, String, String, T, PatchEdgeQuery)</a></td><td>
Updates the data of the specific edge in the collection. PATCH/_api/gharial/{graph}/edge/{collection}/{edge}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="980726ed-53c4-f800-944c-cd12cf9f96fd">PatchVertexAsync(T, U)(String, String, T, PatchVertexQuery)</a></td><td>
Updates the data of the specific vertex based on its document ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="7eb3256b-0323-cbff-1587-4195767ca907">PatchVertexAsync(T, U)(String, String, String, T, PatchVertexQuery)</a></td><td>
Updates the data of the specific vertex in the collection. PATCH/_api/gharial/{graph}/vertex/{collection}/{vertex}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="241bd976-400c-e7b6-d10d-892c6d35750d">PostEdgeAsync(T)</a></td><td>
Creates an edge in an existing graph. The edge must contain a _from and _to value referencing valid vertices in the graph. The edge has to conform to the definition of the edge collection it is added to. POST /_api/gharial/{graph}/edge/{collection}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="18a840a3-737c-2e06-02a6-074b3bbe0cb2">PostEdgeDefinitionAsync</a></td><td>
Adds an additional edge definition to the graph. This edge definition has to contain a collection and an array of each from and to vertex collections. An edge definition can only be added if this definition is either not used in any other graph, or it is used with exactly the same definition. It is not possible to store a definition “e” from “v1” to “v2” in the one graph, and “e” from “v2” to “v1” in the other graph. POST /_api/gharial/{graph}/edge</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="4e33f188-39ad-24bb-a730-4647098f4883">PostGraphAsync</a></td><td>
Creates a new graph in the graph module. POST /_api/gharial</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="ec6f1a01-0dfd-9fa9-1c51-d0c26ef1db54">PostVertexAsync(T)</a></td><td>
Adds a vertex to the given collection. POST/_api/gharial/{graph}/vertex/{collection}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="720caea6-66cc-aeff-d5da-747166afe94b">PostVertexCollectionAsync</a></td><td>
Adds a vertex collection to the set of orphan collections of the graph. If the collection does not exist, it will be created. POST /_api/gharial/{graph}/vertex</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="9664153c-c464-6229-db7f-d7430ae37a36">PutEdgeAsync(T)(String, String, T, PutEdgeQuery)</a></td><td>
Replaces the data of an edge based on its document ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="7427a2a9-8a3d-7909-3d7c-678accf298b8">PutEdgeAsync(T)(String, String, String, T, PutEdgeQuery)</a></td><td>
Replaces the data of an edge in the collection. PUT /_api/gharial/{graph}/edge/{collection}/{edge}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="23532dfe-d222-b2a3-9eab-0678e25cb66d">PutEdgeDefinitionAsync</a></td><td>
Change one specific edge definition. This will modify all occurrences of this definition in all graphs known to your database. PUT/_api/gharial/{graph}/edge/{definition}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="6f93753e-5649-7f5d-7ae8-2a2b38d3b2cb">PutVertexAsync(T)(String, String, T, PutVertexQuery)</a></td><td>
Replaces the data of a vertex based on its document ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="95d62b10-8488-3958-f9dc-1a40396117e1">PutVertexAsync(T)(String, String, String, T, PutVertexQuery)</a></td><td>
Replaces the data of a vertex in the collection. PUT/_api/gharial/{graph}/vertex/{collection}/{vertex}</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.tostring#system-object-tostring" target="_blank" rel="noopener noreferrer">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="2e06730e-eeec-2270-4bd5-34efbcd2015d">ValidateDocumentId</a></td><td>
Checks whether the provided document ID is in the correct form of "{collection}/{key}".
 (Inherited from <a href="1e4d73ca-864e-e82d-2705-3f6909ffa824">ApiClientBase</a>.)</td></tr></table>&nbsp;
<a href="#graphapiclient-class">Back to Top</a>

## Fields
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Protected field](media/protfield.gif "Protected field")</td><td><a href="9a4a2ec8-97a9-692c-be6b-8d9f0aa35b91">_graphApiPath</a></td><td>
The root path of the API.</td></tr><tr><td>![Protected field](media/protfield.gif "Protected field")</td><td><a href="b1cd0079-6ceb-af48-0ffb-1c7689963ee6">_transport</a></td><td>
The transport client used to communicate with the ArangoDB host.</td></tr></table>&nbsp;
<a href="#graphapiclient-class">Back to Top</a>

## See Also


#### Reference
<a href="5db3e172-88fa-722f-6e7f-25b7310b3db3">ArangoDBNetStandard.GraphApi Namespace</a><br />