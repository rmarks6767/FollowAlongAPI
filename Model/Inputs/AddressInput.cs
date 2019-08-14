using GraphQL.Types;

namespace FollowAlongLearnAPI.Model.Inputs
{
    public class AddressInput : InputObjectGraphType
    {
        public AddressInput()
        {
            Name = "AddressInput";
            Field<NonNullGraphType<StringGraphType>>("street");
            Field<NonNullGraphType<StringGraphType>>("boxNumber");
            Field<NonNullGraphType<StringGraphType>>("state");
            Field<NonNullGraphType<StringGraphType>>("zipcode");
            Field<NonNullGraphType<StringGraphType>>("country");
        }
    }
}