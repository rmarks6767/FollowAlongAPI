namespace FollowAlongLearnAPI.Model.Base
{
    public class Account
    {
        //TODO 3.1 Now we are going to add some basic stuff to the Account object, name, address, etc.
        public string UserName { get; set; }
        public string Password { get; set; }
        //TODO 3.2.a We are going to make a separate Name type that will be stored on the Account.  This will allow us to use it anywhere.  Make that file in the model folder.
        public Name Name { get; set; }
        //TODO 3.3.a For the Address, we are going to make another type, add that to the model folder now.
        public Address Address { get; set; }
    }
}

//TODO 3.4 Now that the Account model is defined, we will make Graph types for these three models.  Make a folder called GraphTypes in the Model folder.  Add a type called AccountGraph to this folder.
