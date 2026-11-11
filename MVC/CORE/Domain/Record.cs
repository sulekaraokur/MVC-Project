namespace CORE.Domain
{
    //Base abst. class for all ent. , request and response classes.
    public abstract class Record
    {
        //property for primary key of entities, unique ident. of 
        //request and responses

        public int Id { get; set; }
        //Constructor with id param. to set to Id property value
        //from a derived class const. with id param. 

        protected Record(int id)
        { 
            Id = id;
        }

        //default const. to set the Id prop. def. value 0

        protected Record()
        {
           
        }

    }
}
