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

        [WebGet(UriTemplate = "/Quibbles")]
        public Quibble[] GetAll()
        {
            var service = new QuibbleDataService();
            return service.GetAll();
        }

        //[WebGet(UriTemplate = "/Quibble?id={id}")]
        [WebGet(UriTemplate = "/Quibble/{id}")]
        public Quibble GetById(string Id)
        {
            int parsedId;
            Int32.TryParse(Id, out parsedId);
            var service = new QuibbleDataService();
            return service.GetById(parsedId);
        }
    }
}
