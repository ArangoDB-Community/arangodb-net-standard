# ITransactionApiClient Interface
 

Defines a client to access the ArangoDB Transactions API.

**Namespace:**&nbsp;<a href="10b4cda7-da42-de9a-2bf6-0d4cae3bd2e3">ArangoDBNetStandard.TransactionApi</a><br />**Assembly:**&nbsp;ArangoDBNetStandard (in ArangoDBNetStandard.dll) Version: 1.1.0

## Syntax

**C#**<br />
``` C#
public interface ITransactionApiClient
```

**VB**<br />
``` VB
Public Interface ITransactionApiClient
```

**C++**<br />
``` C++
public interface class ITransactionApiClient
```

**F#**<br />
``` F#
type ITransactionApiClient =  interface end
```

The ITransactionApiClient type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="dd112c87-19e8-0be0-06c3-13ea62eab9a0">AbortTransaction</a></td><td>
Abort a stream transaction by DELETE.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="6c04efb0-8600-c464-23bf-df0ed592fa61">BeginTransaction</a></td><td>
Begin a stream transaction by POST.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="05fbb3fb-653c-df14-01a7-45aa9bf307c7">CommitTransaction</a></td><td>
Commit a transaction by PUT.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="f1c52c80-9838-113c-7783-32493a5abc29">GetAllRunningTransactions</a></td><td>
Get currently running transactions.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="7555364f-0bf7-2b62-c709-2f8553ef7c4d">GetTransactionStatus</a></td><td>
Get the status of a transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="f0af56e7-b068-e8e3-f047-9ad88f20d22a">PostTransactionAsync(T)</a></td><td>
POST a transaction to ArangoDB.</td></tr></table>&nbsp;
<a href="#itransactionapiclient-interface">Back to Top</a>

## See Also


#### Reference
<a href="10b4cda7-da42-de9a-2bf6-0d4cae3bd2e3">ArangoDBNetStandard.TransactionApi Namespace</a><br />