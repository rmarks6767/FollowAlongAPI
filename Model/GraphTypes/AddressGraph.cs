//TODO 3.9.b.2 Add references to the classes used with the inheritance.
using FollowAlongLearnAPI.Model.Base;
using GraphQL.Types;

namespace FollowAlongLearnAPI.Model.GraphTypes
{
    //TODO 3.9.b.1 This one will be set up very similarly to the AccountGraph, add inheritance and map all of the fields.
    public class AddressGraph : ObjectGraphType<Address>
    {
        //TODO 3.9.c.1 Make a constructor for the AddressGraph type.
        public AddressGraph()
        {
            //TODO 3.9.c.2 Add a name and description to the AddressGraph
            Name = "Address";
            Description = "Holds all the information associated with the Account Address";
            //TODO 3.9.c.3 Define all of the fields from the Address class.
            Field(account => account.Country).Name("country");
            Field(account => account.HouseNumber).Name("houseNumber");
            Field(account => account.State).Name("state");
            Field(account => account.Street).Name("street");
            Field(account => account.Zipcode).Name("zipcode");
        }
    }
}