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
        [WebGet(UriTemplate = "/wcfRequisitionList/{deptid}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfRequisitionList> RequisitionList(string deptid);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfRequisitionList/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfRequisitionItem> getrequisitionitem(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfTodayCollection/{deptid}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfTodayCollectionlist> getTodayCollection(string deptid);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfTodayCollectionDetail?d={deptid}&r={reqDetailID}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfTodayCollectionDetail> getTodayCollectionDetail(string deptid, string reqDetailID);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfApproveRequisitions/{deptid}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfApproveRequisitions> getApproveReqList(string deptid);

        //[OperationContract]
        //[WebGet(UriTemplate = "/ttt?d={deptid}&r={reqDetailID}", ResponseFormat = WebMessageFormat.Json)]
        //String ttt(string deptid, string reqDetailID);


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


[DataContract]
public class wcfRequisitionItem
{

     string itemname;
    string quanity;
    string uom;


    [DataMember]
    public String Itemname { get; set; }

    [DataMember]
    public String Quanity { get; set; }

    [DataMember]
    public String Uom { get; set; }
    
}

[DataContract]
public class wcfTodayCollectionlist
{

    string collectionpt;
    string time;
    string reqDetailID;

    [DataMember]
    public String Collectionpt { get; set; }

    [DataMember]
    public String Time { get; set; }

    [DataMember]
    public String RequisitionDetailID { get; set; }
}


[DataContract]
public class wcfTodayCollectionDetail
{

    string itemDescription;
    string disbursedQty;
    string requstedQty;

    [DataMember]
    public String DisbursedQty { get; set; }

    [DataMember]
    public String RequestedQty { get; set; }
    [DataMember]
    public String ItemDescription { get; set; }

}
public class wcfApproveRequisitions
{
    string empName;
    string reqDate;
    string reqID;

    [DataMember]
    public String EmployeeName { get; set; }

    [DataMember]
    public String RequestedDate { get; set; }

    [DataMember]
    public String RequisitionID { get; set; }
}

