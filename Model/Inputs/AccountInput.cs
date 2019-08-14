//TODO 2.4.3.d.4.b Add a reference to the input graph type.
using GraphQL.Types;

namespace FollowAlongLearnAPI.Model.Inputs
{
    //TODO 2.4.3.d.4.a Add inheritance to the InputObjectGraphType to tell GraphQL that this is an input type.
    public class AccountInput : InputObjectGraphType
    {
        //TODO 2.4.3.d.4.c Now add a constructor for the AccountInput.
        public AccountInput()
        {
            //TODO 2.4.3.d.4.d Now you are going to map all of the fields that need to be added to create an Account.
            Field<NonNullGraphType<AddressInput>>("address");  //TODO 2.4.3.d.4.e At this time create an AddressInput type and a NameInput type.  Let's see if you can do this on your own.  If you need to see what they should look like, you can find the two files in this folder.
            Field<NonNullGraphType<NameInput>>("name");
            Field<NonNullGraphType<StringGraphType>>("userName");
            Field<NonNullGraphType<StringGraphType>>("password");
            Field<StringGraphType>("id");
            //TODO 2.4.3.d.4.f Now that you have created those types, head back to the AccountMutation class.
        }
    }
}
