//TODO 1.3.8.b.2 Add references to the classes used with the inheritance.
using FollowAlongLearnAPI.Model.Base;
using GraphQL.Types;

namespace FollowAlongLearnAPI.Model.GraphTypes
{
    //TODO 1.3.8.b.1 This one will be set up very similarly to the NameGraph, add inheritance and map all of the fields.
    public class NameGraph : ObjectGraphType<Name>
    {
        //TODO 1.3.8.c.1 Make a constructor for the NameGraph type.
        public NameGraph()
        {
            //TODO 1.3.8.c.2 Add a name and description to the AddressGraph.
            Name = "Name";
            Description = "Holds first name and last name from the Account type";
            //TODO 1.3.8.c.3 Define all of the fields from the Name class.
            Field(name => name.FirstName).Name("firstName");
            Field(name => name.LastName).Name("lastName");
            Field(name => name.MiddleInitial).Name("middleInitial");
        }
    }
}