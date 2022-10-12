using Super3.Domain.Model;

namespace Super3.Domain.Validations.Base
{
    public class Response
    {
        private Customer customer;

        public Response()
        {
            Report = new List<Report>();
        }

        public Response(List<Report> reports)
        {
            Report = reports ?? new List<Report>();
        }

        public Response(Report report) : this(new List<Report>() { report })
        {

        }

        public Response(Customer customer)
        {
            Report = new List<Report>();
        }

        public List<Report> Report { get; }
        public Task Data { get; internal set; }

        public static Response<T> OK<T>(T data) => new Response<T>(data);
        public static Response OK() => new Response();
        public static Response Unprocessable(List<Report> reports) => new Response(reports);
        public static Response Unprocessable(Report report) => new Response(report);
        public static Response<T> Unprocessable<T>(List<Report> reports) => new Response<T>(reports);

    }

    public class Response<T> : Response
    {
        public Response()
        {
        }
        public Response(List<Report> reports) : base(reports)
        {

        }

        public Response(T data, List<Report> reports = null) :base(reports)
        {
            Data = data;
        }

        public T Data { get; set; }

    }
}
