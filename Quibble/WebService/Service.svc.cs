using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WebService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet(UriTemplate = "/DoSomething")]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]

        private QuibbleDataService _service;

        public Service()
        {
            _service = new QuibbleDataService();
        }

        [WebGet(UriTemplate = "/Quibbles")]
        // [WebGet(UriTemplate = "/Quibbles", ResponseFormat = WebMessageFormat.Json)]
        // http://localhost:53482/Service.svc/Quibbles
        public Quibble[] GetAll()
        {
            return _service.GetAll();
        }

        //[WebGet(UriTemplate = "/Quibble?id={id}")]
        [WebGet(UriTemplate = "/Quibble/{id}")]
        // http://localhost:53482/Service.svc/Quibble/2
        public Quibble GetById(string id)
        {
            int parsedId;
            Int32.TryParse(id, out parsedId);
            return _service.GetById(parsedId);
        }

        [WebInvoke(UriTemplate = "/Quibbles")]
        // http://localhost:53482/Service.svc/Quibbles
        public Quibble Create(Quibble quibble)
        {
            return _service.Add(quibble);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/Quibble/{id}")]
        // [WebInvoke(Method = "PATCH", UriTemplate = "/Quibble/{id}")]
        // http://localhost:53482/Service.svc/Quibble/2
        public Quibble Update(string id, Quibble quibble)
        {
            return _service.Update(quibble);
        }

        [WebInvoke(Method = "DELETE", UriTemplate = "/Quibble/{id}")]
        // http://localhost:53482/Service.svc/Quibble/2
        public void Delete(string id)
        {
            int parsedId;
            Int32.TryParse(id, out parsedId);
            _service.Delete(parsedId);
            //return $"Quibble {id} has been deleted.";
        }
    }
}
