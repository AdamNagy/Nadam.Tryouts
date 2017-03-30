namespace Nadam.Lib.JsonDb
{
    /// <summary>
    /// Entitiy Frameworks load strategies for queryable
    /// </summary>
    public enum DeferredExecutionPlans
    {
        /// <summary>
        /// Lazy loading is the process whereby an entity or collection of entities is automatically loaded from the database the first 
        /// time that a property referring to the entity/entities is accessed. (There is no .Include() method)
        /// </summary>
        LazyLoading,
        /// <summary>
        /// Eager loading is the process whereby a query for one type of entity also loads related entities as part of the query. 
        /// Eager loading is achieved by use of the Include method. For example, the queries below will load blogs and all the posts 
        /// related to each blog.
        /// </summary>
        EagerLoading
    }
}
