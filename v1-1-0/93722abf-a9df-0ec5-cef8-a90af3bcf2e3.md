# PostParseAqlQueryResponse Class
 

Response from <a href="664c4849-5b8f-857e-2abd-0fb526f3456a">PostParseAqlQueryAsync(PostParseAqlQueryBody)</a>


## Inheritance Hierarchy
<a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">System.Object</a><br />&nbsp;&nbsp;<a href="1fbe7dd1-a696-f52b-4750-102bf0210603">ArangoDBNetStandard.AqlFunctionApi.Models.ResponseBase</a><br />&nbsp;&nbsp;&nbsp;&nbsp;ArangoDBNetStandard.AqlFunctionApi.Models.PostParseAqlQueryResponse<br />
**Namespace:**&nbsp;<a href="e03acbe1-782e-533e-7ffe-cd51613ed54f">ArangoDBNetStandard.AqlFunctionApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public class PostParseAqlQueryResponse : ResponseBase
```

**VB**<br />
``` VB
Public Class PostParseAqlQueryResponse
	Inherits ResponseBase
```

**C++**<br />
``` C++
public ref class PostParseAqlQueryResponse : public ResponseBase
```

**F#**<br />
``` F#
type PostParseAqlQueryResponse =  
    class
        inherit ResponseBase
    end
```

The PostParseAqlQueryResponse type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="d08718e8-c3d6-2c07-91a2-9a7f13b02c35">PostParseAqlQueryResponse</a></td><td>
Initializes a new instance of the PostParseAqlQueryResponse class</td></tr></table>&nbsp;
<a href="#postparseaqlqueryresponse-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="197fe15c-e575-8c1d-b99d-0d489dd07328">Ast</a></td><td>
Tree of data nodes providing information about the query.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="99ba100b-d928-4805-b308-b27b13586413">BindVars</a></td><td>
Contains the binding variables involved in the query.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="f984a497-cdde-35a6-8f92-0d1f9df2ce8b">Code</a></td><td>
The HTTP status code.
 (Inherited from <a href="1fbe7dd1-a696-f52b-4750-102bf0210603">ResponseBase</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="dd5f37f2-5441-0731-5081-dbe6fbb2fa46">Collections</a></td><td>
Contains the names of the collections that are involved in the query.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="3baa6c0d-d597-6c7c-46d0-9a25710a7de9">Error</a></td><td>
Indicates whether an error occurred
 (Inherited from <a href="1fbe7dd1-a696-f52b-4750-102bf0210603">ResponseBase</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="9246594e-4a80-9bf5-3ade-e0df638c099a">ErrorMessage</a></td><td>
When the query is invalid this will contain the error message.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="270a0242-648a-0bb4-f00c-1f9e24df67f7">ErrorNum</a></td><td>
When the query is invalid this will contain the error number.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="eb97eb96-54c0-00a8-9545-44e7738d8e14">Parsed</a></td><td>
Indicates that the query was successfully parsed.</td></tr></table>&nbsp;
<a href="#postparseaqlqueryresponse-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Equals</a></td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.finalize#system-object-finalize" target="_blank" rel="noopener noreferrer">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.gethashcode#system-object-gethashcode" target="_blank" rel="noopener noreferrer">GetHashCode</a></td><td>
Serves as the default hash function.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.gettype#system-object-gettype" target="_blank" rel="noopener noreferrer">GetType</a></td><td>
Gets the <a href="https://docs.microsoft.com/dotnet/api/system.type" target="_blank" rel="noopener noreferrer">Type</a> of the current instance.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.memberwiseclone#system-object-memberwiseclone" target="_blank" rel="noopener noreferrer">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="https://docs.microsoft.com/dotnet/api/system.object.tostring#system-object-tostring" target="_blank" rel="noopener noreferrer">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.)</td></tr></table>&nbsp;
<a href="#postparseaqlqueryresponse-class">Back to Top</a>

## See Also


#### Reference
<a href="e03acbe1-782e-533e-7ffe-cd51613ed54f">ArangoDBNetStandard.AqlFunctionApi.Models Namespace</a><br />