//TODO 1.4.d.1 Add / install the Newtonsoft Json NuGet package
using Newtonsoft.Json.Linq;

namespace FollowAlongLearnAPI.MiddleWare
{
    public class GraphQLQuery
    {
        //TODO 1.4.a Operation is in relation to Query or Mutation when it comes to GraphQL
        public string Operation { get; set; }
        //TODO 1.4.b NamedOperation is in relation to the specific function in query or mutation that you are going to perform
        public string NamedOperation { get; set; }
        //TODO 1.4.c The query is the information that you request from the database
        public string Query { get; set; }
        //TODO 1.4.d The inputs are any variables that you may have in your query
        public JObject Inputs { get; set; }
    }
}

//TODO 1.4.e Now that that's finished, head back over to the controller