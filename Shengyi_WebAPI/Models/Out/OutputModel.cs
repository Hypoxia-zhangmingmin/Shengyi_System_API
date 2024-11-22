namespace Shengyi_WebAPI.Models.Out
{
    public class OutputModel<T>
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public OutputModel<T> Success() =>new OutputModel<T> { Code = 1, Message = "Successful" ,Data =default};
        public OutputModel<T> Success(string message) => new OutputModel<T> { Code = 1, Message = message, Data = default };

        public OutputModel<T> Failed()=>new OutputModel<T> { Code = -1, Message = "Failed",Data = default};
        public OutputModel<T> Failed(string message) => new OutputModel<T> { Code = -1, Message = message, Data = default };
        public OutputModel<T> Success(T data) => new OutputModel<T>
        {
            Code = 1,
            Message = "success",
            Data = data
        };
       
    }
}
