namespace FollowAlongLearnAPI.Configuration
{
    public class AccountConfiguration
    {
        //TODO 2.0.3.a The example database that we are going to use is Cosmos DB.  There is an emulator that you can download that will work perfectly for what we are doing. --> https://aka.ms/cosmosdb-emulator
        public string Uri { get; set; }  //TODO 2.0.3.b The URI of the CosmosDb
        public string Key { get; set; }  //TODO 2.0.3.c The Primary Key for access to the Database.
        public string AccountDB { get; set; } //TODO 2.0.3.d The Name of the Database that the accounts are stored in.
        public string AccountCollection { get; set; } 

        public DocumentClient GetDocumentClient()
        {
            return new DocumentClient()
            {

            };
        }
    }
}
