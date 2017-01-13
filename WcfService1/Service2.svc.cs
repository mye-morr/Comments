using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : IService2
    {

        public List<wsTestDbItem> GetAllTestDbItems()
        {
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsTestDbItem> results = new List<wsTestDbItem>();
                foreach (ListBuilder1 item in dc.ListBuilder1s)
                {
                    results.Add(new wsTestDbItem()
                    {
                        sCat = item.sCat,
                        sSubcat = item.sSubcat,
                        sItem = item.sItem,
                        sDialog = item.sDialog
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsSQLResult CreateTestDbItem(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // ..then convert the string into a single "wsCustomer" record.
                JavaScriptSerializer jss = new JavaScriptSerializer();
                wsTestDbItem item = jss.Deserialize<wsTestDbItem>(JSONdata);
                if (item == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsCustomer" object.
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }

                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                ListBuilder1 newCustomer = new ListBuilder1()
                {
                    sCat = item.sCat,
                    sSubcat = item.sSubcat,
                    sDialog = item.sDialog
                };

                dc.ListBuilder1s.InsertOnSubmit(newCustomer);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public wsSQLResult DeleteTestDbItem(string TestDbItemID)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                ListBuilder1 item = dc.ListBuilder1s.Where(s => s.numRow == Int32.Parse(TestDbItemID)).FirstOrDefault();
                if (item == null)
                {
                    // We couldn't find a [Customer] record with this ID.
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [ListBuilder1] record with ID: " + TestDbItemID.ToString();
                    return result;
                }

                dc.ListBuilder1s.DeleteOnSubmit(item);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;     // Success !
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;     // Failed.
            }
        }
    }
}
