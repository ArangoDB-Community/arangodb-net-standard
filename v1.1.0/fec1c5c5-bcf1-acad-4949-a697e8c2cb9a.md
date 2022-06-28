# PutDocumentsDocumentResponse(*T*) Class
 

Represents the response for one document when replacing multiple document.


## Inheritance Hierarchy
<a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">System.Object</a><br />&nbsp;&nbsp;<a href="a5eaa0e0-20e6-6527-df46-e76faa3ec20a">ArangoDBNetStandard.DocumentApi.Models.DocumentBase</a><br />&nbsp;&nbsp;&nbsp;&nbsp;<a href="e02f4b6d-cd9b-3f2c-8347-335a724a8493">ArangoDBNetStandard.DocumentApi.Models.PutDocumentResponse</a>(*T*)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ArangoDBNetStandard.DocumentApi.Models.PutDocumentsDocumentResponse(T)<br />
**Namespace:**&nbsp;<a href="81a73561-cfc6-64b8-9923-29f0333f4867">ArangoDBNetStandard.DocumentApi.Models</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public class PutDocumentsDocumentResponse<T> : PutDocumentResponse<T>

```

**VB**<br />
``` VB
Public Class PutDocumentsDocumentResponse(Of T)
	Inherits PutDocumentResponse(Of T)
```

**C++**<br />
``` C++
generic<typename T>
public ref class PutDocumentsDocumentResponse : public PutDocumentResponse<T>
```

**F#**<br />
``` F#
type PutDocumentsDocumentResponse<'T> =  
    class
        inherit PutDocumentResponse<'T>
    end
```


#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>The type of the deserialized new/old document object when requested.</dd></dl>&nbsp;
The PutDocumentsDocumentResponse(T) type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="24055867-5465-fa65-44b7-7fb8acef02fe">PutDocumentsDocumentResponse(T)</a></td><td>
Initializes a new instance of the PutDocumentsDocumentResponse(T) class</td></tr></table>&nbsp;
<a href="#putdocumentsdocumentresponse(*t*)-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="a0476557-4a19-0eae-f9e6-bf05599ad283">_id</a></td><td>
ArangoDB document ID.
 (Inherited from <a href="a5eaa0e0-20e6-6527-df46-e76faa3ec20a">DocumentBase</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="12b077b9-8dd6-3303-069d-e132c7a8c710">_key</a></td><td>
ArangoDB document key.
 (Inherited from <a href="a5eaa0e0-20e6-6527-df46-e76faa3ec20a">DocumentBase</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="9acb6c26-1d90-b993-4e4d-18dd48b33aea">_oldRev</a></td><td>
Contains the revision of the old (now replaced) document.
 (Inherited from <a href="e02f4b6d-cd9b-3f2c-8347-335a724a8493">PutDocumentResponse(T)</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="94e9ac20-6fa8-2d8d-8804-795d19fe03ec">_rev</a></td><td>
ArangoDB document revision tag.
 (Inherited from <a href="a5eaa0e0-20e6-6527-df46-e76faa3ec20a">DocumentBase</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="ae313e43-b326-3a66-ab3e-b5a2bd69279f">Error</a></td><td>
Indicates whether an error occurred.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="42738f57-8840-2a84-e481-a9d19b0f8c72">ErrorMessage</a></td><td>
ArangoDB error message.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="72485bb2-76b5-3556-25e1-3e8faac00899">ErrorNum</a></td><td>
ArangoDB error number.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="d95f84e5-7bab-d319-9a23-3ffbf3fb7e84">New</a></td><td>
Deserialized copy of the new document object. This will only be present if requested with the <a href="0d7b346f-1dfa-faaf-19bb-b5be31e31340">ReturnNew</a> option.
 (Inherited from <a href="e02f4b6d-cd9b-3f2c-8347-335a724a8493">PutDocumentResponse(T)</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="ba8f0a2e-4603-b7b9-5b9c-f9431f182082">Old</a></td><td>
Deserialized copy of the old document object. This will only be present if requested with the <a href="3663b415-723f-275c-6ace-a9c5c7673609">ReturnOld</a> option.
 (Inherited from <a href="e02f4b6d-cd9b-3f2c-8347-335a724a8493">PutDocumentResponse(T)</a>.)</td></tr></table>&nbsp;
<a href="#putdocumentsdocumentresponse(*t*)-class">Back to Top</a>

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
<a href="#putdocumentsdocumentresponse(*t*)-class">Back to Top</a>

## See Also


#### Reference
<a href="81a73561-cfc6-64b8-9923-29f0333f4867">ArangoDBNetStandard.DocumentApi.Models Namespace</a><br />