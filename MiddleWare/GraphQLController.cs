/* The Controller Overview
 The controller is where all the requests 
 will actually be made to.  The endpoint,
 as defined by the name {something}Controller,
 allows the user to request different data in
 a traditional REST API model.  GraphQL allows 
 this to behave a little differently.  Instead
 of having multiple controllers, you will have one.
 That one is responsible for controlling the 
 logic and directing all of the requests made 
 to the API.  For our example here, our main 
 endpoint will be https://localhost:5001/graphql.
 That endpoint directly corresponds to this document.
 To view the schema you will want to direct your 
 attention to https://localhost:5001/graph, where
 the GraphiQL interface will be displayed. 
 */

//TODO 1.1.1.b Add MVC namespace.
using Microsoft.AspNetCore.Mvc;
//TODO 1.1.2.b Add GraphQL for the Executer and GraphQL.Types for the ISchema
using GraphQL;
using GraphQL.Types;
//TODO 1.1.3.b Add threading to the namespace
using System.Threading.Tasks;
//TODO 1.1.5.b.1 Add System.Net to send back status codes
using System.Net;

namespace FollowAlongLearnAPI.MiddleWare
{
    //TODO 1.1.1.a We are now going to specify that the endpoint to get here is /graphql (it comes from the characters you put in front of the controller in the name of the class)
    [Route("[controller]")]
    public class GraphQLController
    {
        //TODO 1.1.2.c Now add private readonly fields for the executor and the schema
        private readonly ISchema schema;
        private readonly IDocumentExecuter executer;

        //TODO 1.1.2.a make a constructor and add an injection of an ISchema (which will be the schema) and an IDocumentExecuter (for execution of the schema)
        public GraphQLController(ISchema schema, IDocumentExecuter executer)
        {
            //TODO 1.1.2.d Assing the dependency injected fields to the private readonly ones
            this.schema = schema;
            this.executer = executer;
        }

        //TODO 1.1.3.a We will now add the main POST entry point for the API
        [HttpPost]
        //TODO 1.1.3.c Now head over (or create) the GraphQLQuery.cs file.  That will be how these requests are translated so it has to be done before we move forward with the Controller
        public async Task<dynamic> Post([FromBody] GraphQLQuery query) //TODO 1.1.5.a After defining the GraphQLQuery, we can use it by saying [FromBody], which will convert the json post body to the object we just created
        {
            //TODO 1.1.5.b now we are going to make sure the query is not null, if it is return BadRequest
            if (query == null)
                return HttpStatusCode.BadRequest;

            //TODO 1.1.5.c Now we will build the Execution Options to pass to the document executer
            ExecutionOptions executionOptions = new ExecutionOptions()
            {
                Schema = schema,                   //TODO 1.1.5.c.1 Set the schema equal to the one we injected
                Query = query.Query,               //TODO 1.1.5.c.2 Set the query equal to the query from the custom object
                Inputs = query.Variables.ToInputs()   //TODO 1.1.5.c.3 Set any variables equal to the inputs
            };

            //TODO 1.1.5.d We will now send that to the executor, which will resolve the query and return a result
            ExecutionResult result = await executer.ExecuteAsync(executionOptions).ConfigureAwait(false);

            //TODO 1.1.5.e.1 With that result, we will check for errors and report them as necessary
            if (result.Errors?.Count > 0)
                //TODO 1.1.5.e.2 For now we will just return the errors, but coming up with how you want to report the errors is important.  I would suggest reporting errors, data, and a status code that corresponds to any errors that occurred
                return result.Errors;

            //TODO 1.1.5.f Now that we have checked if there are any errors, we can return the result.
            return result;
        }
    }
}

//TODO 1.1.6 For now the controller is done.  Now it's time to build a schema and add some logic into the mix. Make a file called Schema.cs and put it into the Middleware folder.
