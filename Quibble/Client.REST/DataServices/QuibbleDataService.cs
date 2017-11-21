using Client.REST.Transports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client.REST.DataServices
{
    class QuibbleDataService
    {
        public QuibbleDataService()
        {
        }

        public Quibble[] GetAll()
        {
            var client = new WebClient();
            client.Headers.Add("Accept", "application/json");

            var result = client.DownloadString("http://localhost:53482/Service.svc/Quibbles");
            var serializer = new DataContractJsonSerializer(typeof(Quibble[]));

            Quibble[] resultObject;

            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (Quibble[])serializer.ReadObject(stream);
            }

            return resultObject;
        }

        public Quibble GetById(int id)
        {
            var client = new WebClient();
            // client.Headers.Add("Accept", "text/xml"); // no need as this is by default

            // var result = client.DownloadString("http://localhost:53482/Service.svc/Quibble/1");
            var result = client.DownloadString("http://localhost:53482/Service.svc/Quibble/" + id.ToString());
            var serializer = new XmlSerializer(typeof(Quibble));

            Quibble resultObject;

            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                resultObject = (Quibble)serializer.Deserialize(stream);
            }

            return resultObject;
        }

        //public Quibble Create(Quibble quibble)
        //{
        //    var request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53482/Service.svc/Quibbles");

        //    request.Accept = "application/json";
        //    request.ContentType = "application/json";

        //    request.Method = "POST";

        //    var serializer = new DataContractJsonSerializer(typeof(Quibble));
        //    var requestStream = request.GetRequestStream();
        //    serializer.WriteObject(requestStream, quibble);
        //    requestStream.Close();

        //    var response = request.GetResponse();
        //    var responseStream = response.GetResponseStream();
        //    var responseObject = (Quibble)serializer.ReadObject(responseStream);
        //    responseStream.Close();

        //    return responseObject;
        //}

        public Quibble Create(Quibble quibble)
        {
            // return SendToServer("http://localhost:53482/Service.svc/Quibbles", "POST", quibble);
            return SendDataToServer("http://localhost:53482/Service.svc/Quibbles", "POST", quibble);
        }

        public Quibble Update(Quibble quibble)
        {
            return SendDataToServer("http://localhost:53482/Service.svc/Quibble/" + quibble.Id, "PUT", quibble);
        }

        public void Delete(int id)
        {
            SendDataToServer("http://localhost:53482/Service.svc/Quibble/" + id, "DELETE", new DeleteQuibble { Id = id });
        }

        private Quibble SendToServer(string endpoint, string method, Quibble quibble)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(endpoint);

            request.Accept = "application/json";
            request.ContentType = "application/json";

            request.Method = method;

            var serializer = new DataContractJsonSerializer(typeof(Quibble));
            var requestStream = request.GetRequestStream();
            serializer.WriteObject(requestStream, quibble);
            requestStream.Close();

            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var responseObject = (Quibble)serializer.ReadObject(responseStream);
            responseStream.Close();

            return responseObject;
        }

        // using generics
        private T SendDataToServer<T>(string endpoint, string method, T quibble)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(endpoint);

            request.Accept = "application/json";
            request.ContentType = "application/json";

            request.Method = method;

            var serializer = new DataContractJsonSerializer(typeof(T));
            var requestStream = request.GetRequestStream();
            serializer.WriteObject(requestStream, quibble);
            requestStream.Close();

            var response = request.GetResponse();
            if (response.ContentLength == 0)
            {
                response.Close();
                return default(T);
            }

            var responseStream = response.GetResponseStream();
            var responseObject = (T)serializer.ReadObject(responseStream);
            responseStream.Close();

            return responseObject;
        }
    }
}
