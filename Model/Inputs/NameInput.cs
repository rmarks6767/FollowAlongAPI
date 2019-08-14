using GraphQL.Types;

namespace FollowAlongLearnAPI.Model.Inputs
{
    public class NameInput : InputObjectGraphType
    {
        public NameInput()
        {
            Name = "NameInput";
            Field<NonNullGraphType<StringGraphType>>("firstName");
            Field<NonNullGraphType<StringGraphType>>("lastName");
            Field<NonNullGraphType<StringGraphType>>("middleInitial");
        }
    }
}