namespace DotNetCoreWithMongoDB.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public MongoDBSettings MongoDB { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MongoDBSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CollectionName { get; set; }
    }
}