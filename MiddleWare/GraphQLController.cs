//TODO 1.1.b Add MVC.
using Microsoft.AspNetCore.Mvc;
//TODO 1.2.b Add GraphQL for the Executer and GraphQL.Types for the ISchema
using GraphQL;
using GraphQL.Types;
//TODO 1.3.b Add threading to the namespace
using System.Threading.Tasks;
//TODO 1.5.b.1 Add System.Net to send back status codes
using System.Net;

namespace FollowAlongLearnAPI.MiddleWare
{
    //TODO 1.1.a We are now going to specify that the endpoint to get here is /graphql (it comes from the characters you put in front of the controller in the name of the class)
    [Route("[controller]")]
    public class GraphQLController
    {
        //TODO 1.2.c Now add private readonly fields for the executor and the schema
        private readonly ISchema schema;
        private readonly IDocumentExecuter executer;

        //TODO 1.2.a make a constructor and add an injection of an ISchema (which will be the schema) and an IDocumentExecuter (for execution of the schema)
        public GraphQLController(ISchema schema, IDocumentExecuter executer)
        {
            //TODO 1.2.d Assing the dependency injected fields to the private readonly ones
            this.schema = schema;
            this.executer = executer;
        }

        //TODO 1.3.a We will now add the main POST entry point for the API
        [HttpPost]
        //TODO 1.3.c Now head over (or create) the GraphQLQuery.cs file.  That will be how these requests are translated so it has to be done before we move forward with the Controller
        public async Task<dynamic> Post([FromBody] GraphQLQuery query) //TODO 1.5.a After defining the GraphQLQuery, we can use it by saying [FromBody], which will convert the json post body to the object we just created
        {
            //TODO 1.5.b now we are going to make sure the query is not null, if it is return BadRequest
            if (query == null)
                return HttpStatusCode.BadRequest;

            //TODO 1.5.c Now we will build the Execution Options to pass to the document executer
            ExecutionOptions executionOptions = new ExecutionOptions()
            {
                Schema = schema,                   //TODO 1.5.c.1 Set the schema equal to the one we injected
                Query = query.Query,               //TODO 1.5.c.2 Set the query equal to the query from the custom object
                Inputs = query.Inputs.ToInputs()   //TODO 1.5.c.3 Set any variables equal to the inputs
            };

            //TODO 1.5.d We will now send that to the executor, which will resolve the query and return a result
        }
    }
}
