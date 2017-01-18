using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Team7ADProjectMVC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/msgs", ResponseFormat = WebMessageFormat.Json)]
        List<WCFMsg> DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "/testlistandroid", ResponseFormat = WebMessageFormat.Json)]
        List<wcfRequisitionList> RequisitionList();
    }
}


[DataContract]
public class WCFMsg
{
    string msg;

    [DataMember]
    public string Msg
    {
        get { return msg; }
        set { msg = value; }
    }



    public WCFMsg(string m)
    {
        msg = m;
    }

}

[DataContract]
public class wcfRequisitionList
{

    string id;
    string employeename;
    string status;

    [DataMember]
    public string Employeename
    {
        get { return employeename; }
        set { employeename = value; }
    }

    [DataMember]
    public String Status { get; set; }

    [DataMember]
    public String Id { get; set; }


    public static wcfRequisitionList Make(string name, string s)
    {
        wcfRequisitionList c = new wcfRequisitionList();

        c.employeename = name;
        c.status = s;
     
        return c;
    }
 
 



}
