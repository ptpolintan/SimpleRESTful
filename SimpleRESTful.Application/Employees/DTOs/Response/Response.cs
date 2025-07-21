namespace SimpleRESTful.Application.Employees.DTOs.Response
{
    public class Response
    {
        public string? Error { get; set; }
        public DateTime TimeStamp { get; set; }

        public void Fail(string error)
        { 
            Error = error; 
        }
    }
}
