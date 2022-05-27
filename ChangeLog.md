# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [Unreleased]

## [1.1.0] - 2022-05-25
### Added
- Run tests against ArangoDB 3.8 by @DiscoPYF in #347
- Use arangodb image 3.8.5.1 for 3.8 tests. by @DiscoPYF in #349
- Support for indexes api by @tjoubert in #350
- Support for additional AQL/Query endpoints by @tjoubert in #352
- Support for additional collection endpoints by @tjoubert in #356
- Add changelog file by @tjoubert in #369
- Support for analyzers by @tjoubert in #357
- Support for Views API by @tjoubert in #358
- Support for Oasis (ArangoDB Cloud) by @tjoubert in #364
- Simplify error handling in test fixture of Index API client by @DiscoPYF in #377
- Support basic Admin API by @tjoubert in #370
- Use constants to represent index types. by @DiscoPYF in #376
- Bulk operations support by @tjoubert in #354

### Changed
- Make header properties optional in cursor API client interface. by @DiscoPYF in #344
- Update CircleCI badge link by @Zyqsempai in #365
- Update CI config to run tests against ArangoDB 3.9 by @DiscoPYF in #361
- Amended changelog file by @tjoubert in #371
- Resolve build warnings caused by syntax issues in XML doc comments. by @DiscoPYF in #373
- Fix type of selectivity estimate in responses. by @DiscoPYF in #374
- Extract common index response properties into an interface. by @DiscoPYF in #378
- Updated ChangeLog by @tjoubert in #379

## 2022-05-24
### Added
- added support for ArangoDB Bulk Operations API features (#354)
- `POST/_api/import#document` Imports document values
- `POST/_api/import#json` Imports documents from JSON

## 2022-05-16
### Added
- added support for basic ArangoDB Admin API features (#370)
- `GET/_admin/log/entries` Read global logs from the server
- `POST/_admin/routing/reload` Reloads the routing information
- `GET/_admin/server/id` Returns the id of a server in a cluster
- `GET/_admin/server/role` Returns the role of a server in a cluster
- `GET/_api/engine` Returns the server database engine type
- `GET/_api/version` Returns the server version

## 2022-05-02
### Added
- Added support for connecting to ArangoDB Oasis Cloud Service (#364)
- Operations defaults to _system database when a database is not specified.
- Added support for Oasis SSL certificates.

## 2022-04-29
### Added
- added support for ArangoDB Views API features (#358)
- `GET : /_api/view` : reads all views
- `POST : /_api/view#iresearch` : Create iresearch view
- `DELETE : /_api/view/{view-name}` : Drops a view
- `GET : /_api/view/{view-name}` : Return information about a view
- `GET : /_api/view/{view-name}/properties` : Read properties of a view
- `PATCH : /_api/view/{view-name}/properties#iresearch` : Partially changes properties of an iresearch view
- `PUT : /_api/view/{view-name}/properties#iresearch` : Change properties of an iresearch view
- `PUT : /_api/view/{view-name}/rename` : Rename view

## 2022-04-28
### Added
- added support for ArangoDB Analyzers API features (#357)
- `GET /_api/analyzer` List all Analyzers
- `POST /_api/analyzer` Create an Analyzer with the supplied definition
- `DELETE /_api/analyzer/{analyzer-name}` Remove an Analyzer
- `GET /_api/analyzer/{analyzer-name}` Return the Analyzer definition

## 2022-04-12
### Added
- added support for additional ArangoDB Collections API features (#356)
- `GET/_api/collection/{collection-name}/checksum`: Returns checksum for the collection
- `PUT/_api/collection/{collection-name}/loadIndexesIntoMemory`: Loads a collection's indexes into memory
- `PUT/_api/collection/{collection-name}/recalculateCount`: Recalculate count of a collection
- `PUT​/_api​/collection​/{collection-name}​/responsibleShard`: Returns the responsible shard for a document
- `GET/_api/collection/{collection-name}/shards`: Returns the shard ids of a collection
- `PUT/_api/collection/{collection-name}/compact`: Compacts a collection

## 2022-03-24
### Added
- added support for additional ArangoDB AQL/Query API features (#352)
- `POST: /_api/explain`: Explain an AQL query
- `POST: /_api/query`: Parse an AQL query
- `DELETE: /_api/query/slow`: Clears the list of slow AQL queries
- `GET: /_api/query/slow`: Returns the list of slow AQL queries
- `DELETE: /_api/query/{query-id}`: Kills a running AQL query
- `DELETE: /_api/query-cache`: Clears any results in the AQL query cache
- `GET: /_api/query-cache/entries`: Returns the currently cached query results
- `GET: /_api/query-cache/properties`: Returns the global properties for the AQL query cache
- `PUT: /_api/query-cache/properties`: Globally adjusts the AQL query result cache properties
- `GET: /_api/query/current`: Returns the currently running AQL queries
- `GET: /_api/query/properties`: Returns the properties for the AQL query tracking
- `PUT: /_api/query/properties`: Changes the properties for the AQL query tracking

## 2022-03-04
### Added
- added support for ArangoDB Indexes API features (#350)
- `GET /_api/index` Read all indexes of a collection
- `POST /_api/index#fulltext` Create fulltext index 
- `POST /_api/index#geo` Create geo-spatial index 
- `POST /_api/index#persistent` Create a persistent index
- `POST /_api/index#ttl` Create TTL index
- `DELETE /_api/index/{index-id}` Delete index
- `GET /_api/index/{index-id}` Read index

## 2022-05-16
### Added
- added support for basic Admin API features (#370)
- `GET/_admin/log/entries` Read global logs from the server
- `POST/_admin/routing/reload` Reloads the routing information
- `GET/_admin/server/id` Return id of a server in a cluster
- `GET/_admin/server/role` Return role of a server in a cluster
- `GET/_api/engine` Return server database engine type
- `GET/_api/version` Return server version
