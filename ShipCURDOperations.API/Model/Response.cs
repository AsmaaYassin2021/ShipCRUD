using System.Runtime.Serialization;
using System.Collections.Generic;
namespace ShipCURDOperations.API.Model
{
    [DataContract]
    public class Response<T>
    {

        public Response() { }
        

        [DataMember(Name = "data")]
        private T data;
        [DataMember(Name = "errors")]
        private T errors;

        [DataMember(Name = "succeeded")]
        private bool succeeded;

        public T Data
        {
            get { return data; }
            set { data = value; }
        }
        public T Errors
        {
            get { return errors; }
            set { errors = value; }
        }
        public bool Succeeded
        {
            get { return succeeded; }
            set { succeeded = value; }
        }
        public static Response<T> Fail(T error)
        {
            return new Response<T> { Succeeded = false, Errors = error };
        }
        public static Response<T> Success(T data)
        {
            return new Response<T> { Succeeded = true, Data = data };
        }
        public  Response<T> SuccessData(T data)
        {
            return new Response<T> { Succeeded = true, Data = data };
        }
    }
}