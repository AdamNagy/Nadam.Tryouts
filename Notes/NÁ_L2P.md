# .NET/C#
## C# Core
Reference/value types semantics, stack/heap, Structs in general (basic differences compared to class) 
    <-- AppContext, App indulásnál mi történik, leállásnál esetleg, milyen mélységben kell érteni/tudni 
class- , property- , function visibility modifiers (defaults)
internal keyword
class vs interface vs abstract class
primitive types, (un)boxing, casting, (as <vmi interface>) <-- ?? van egy kis zavar 
string, date (immutable types)
yield return/deferred execution <-- nem kéne keverni (push/pull system)
Object Guid <-- pointer ??
Early/Late binding (Reflection concenrns)

## Reflection, Expression tree, dynamic prog <-- mit is kell itt tudni
Reflection usage: unit tests, DI container, static code analyzer) Assembly, DLL scanning
Expression Trees, IQueryable (Query provider, dynamic query)
dynamic programming (code generation)

## Collections, LINQ
Extension methods, indexer
LINQ(2Object, SQL, XML) methods (IEnumerable extensions: ToList, ToDict, Select, Where, Take, )
Collections, IEnumerable, ICollection, IList (Array, List, LInkedList, Dict, Set, HashSet, OrderedList, Enumerator-ok)
Dictionaries/hashing

## Async programming
CPU-bound and IO-bound tasks (async/await and threading) semaphore(slim), TPL (Task), Asnyc/await
concurrent collections
Event based asnyc -, task based async prog

# ASP.NET
Middleware, Logging, Caching (Cross Cutting Concern), CORS, Auth, DI
Approaches to investigate controller performance issues
Async action
SignalR (web socket, real time messaging) alternative: (long)polling
(ASP).NET Core

# Networking
TCP to UDP
Stateless protocols vs stateless applications (pros and cons of stateless/stateful solutions)
REST
HTTP(s) headers, response (status) codes, verbs
Describe/compare the HTTP verbs (GET vs POST, request body, conventions, google bots/caching, ...)
Web protocols: WS, FTP, HTTP(S), AMQP
OSI model
TCP/IP (v4/v6)

# Software engineering
Patterns: unit of work, repository, CQRS, factory, singleton
SOLID, Gof
Delivery: Agile, Scrum (artifacts: grooming, planning, standup, retro, scrum master, PO, story, epic, story points)
CI/CD, AzureDevops, Jira, Git, Source control, 
Kanban
Testing (unit, integration, end-2-end, acceptance, smoke, load, automated) code coverage (tools), static code analyzers
Architecturing: Classic(monolithic), Microservices, Messaging (queue, Kaffka, RabbitMq), 3Layer, Cross Cutting Concerns, Serverless

# DB engineering
relational, document, key-value, graph
ACID, primary key, secondary key, indexing, SQL
LINQ2SQL, Entity Framework, ORM
SQL Server vs. PostgreSQL (object relational)
Greatest-n-per-group problem

# Cloud Computing
Azure (Az 204)
Functions (triggers), Queues, VMs, Storage (blob, append, random access)
WebApps (slots, etc)
cache
DB solutions, Cosmos DB, SQL Server, Mongo
Azure CLI/Powerhell, Functions CLI
VNets, APIs, security
Resource groups, tags, pricing, logging/monitoring

# JavaScript 
Data types (Number, String, Boolean, Object, Undefined) NaN
this keyword
== and === operators
Implicit Type Coercion
passed by value and passed by reference
Immediately Invoked Function
Higher Order Functions
call(), apply() and, bind() methods
currying
Scope and scope chain
object prototype
hoisting, closures, context, let/var, Promise, parallel exec (Promise),
event loop

# Front-End
HTML, DOM
TypeScript (interface, types, ...)
NPM (versioning, (peer) dependency), modules, ES5 vs ES6+
SPA, Observable (RxJS) -, Store pattern
CSS (selectors, positioning, display (grid/flex)), Bootstrap (grid system, responsivity)
SSR (Server Side Rendering), Caching, bots

