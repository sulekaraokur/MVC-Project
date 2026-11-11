using CORE.Domain;

namespace CORE.Models
{

    //concrete class for returning create, update and delete op. results
    public class CommandResponse : Record
    {
        //readonly propoerty to return the success status as true or false
        //since no setter, the property value can only be set in the below line 
        //or in the constructor

        public bool IsSuccessful { get; }

        //readonly prop. to return add. info. such as a 
        //success msg. or an error msg. containing details.

        public string Message { get; }

        //constructor with param. to set the ISSuccessful and Message
        //prop. values w/ id default param. value passed to the 
        //base abstract Record class const.  with id param
        //If no value is passed for the id param. the default value 0 will be used

        public CommandResponse(bool isSuccessful, string message, int id = 0) : base(id)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }

    }
}
