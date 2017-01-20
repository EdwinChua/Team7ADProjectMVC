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
        [WebGet(UriTemplate = "/wcfRequisitionItem?d={deptid}&r={reqID}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfRequisitionItem> getrequisitionitem(string deptId, string reqID);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfTodayCollection/{deptid}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfTodayCollectionlist> getTodayCollection(string deptid);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfTodayCollectionDetail?d={deptid}&r={disListID}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfTodayCollectionDetail> getTodayCollectionDetail(string deptid, string disListID);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfApproveRequisitions/{deptid}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfApproveRequisitions> getApproveReqList(string deptid);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfApproveReqDetails?d={deptId}&r={reqId}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfApproveReqDetails> getApproveReqDetails(string deptId, string reqId);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfCollectionPoint/{deptid}", ResponseFormat = WebMessageFormat.Json)]
        List<String> getCollectionPoint(string deptid);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfDisbursementList/", ResponseFormat = WebMessageFormat.Json)]
        List<wcfDisbursementList> getDisbursementList();

        [OperationContract]
        [WebGet(UriTemplate = "/wcfDisbursementListDetail/{disListID}", ResponseFormat = WebMessageFormat.Json)]
        List<wcfDisbursementListDetail> getDisbursementListDetails(string disListID);

        [OperationContract]
        [WebGet(UriTemplate = "/wcfStockReorder/", ResponseFormat = WebMessageFormat.Json)]
        List<wcfStockReorder> getStockReorder();

        [OperationContract]
        [WebGet(UriTemplate = "/wcfRetrivalList/", ResponseFormat = WebMessageFormat.Json)]
        List<wcfRetrivalList> getRetrivalList();

        [OperationContract]
        [WebGet(UriTemplate = "/wcfallocate/", ResponseFormat = WebMessageFormat.Json)]
        List<wcfallocate> getallocate();


        [OperationContract]
        [WebGet(UriTemplate = "/wcflogin?userid={userid}&password={password}", ResponseFormat = WebMessageFormat.Json)]
        wcflogin getlogin(String userid,String password);

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
    string deptID;

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
    string quantity;
    string uom;


    [DataMember]
    public String Itemname { get; set; }

    [DataMember]
    public String Quantity { get; set; }

    [DataMember]
    public String Uom { get; set; }
    
}

[DataContract]
public class wcfTodayCollectionlist
{

    string collectionpt;
    string time;
    string disbursementListID;

    [DataMember]
    public String Collectionpt { get; set; }

    [DataMember]
    public String Time { get; set; }

    [DataMember]
    public String DisbursementListID { get; set; }
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
    public String EmpName { get; set; }

    [DataMember]
    public String ReqDate { get; set; }

    [DataMember]
    public String ReqID { get; set; }
}

public class wcfApproveReqDetails
{
    string item;
    string quantity;
    string uom;
  

    [DataMember]
    public String Item { get; set; }

    [DataMember]
    public String Quantity { get; set; }

    [DataMember]
    public String UOM { get; set; }
}
public class wcfDisbursementList
{
    string disListID;
    string deptName;
    string collectionPt;
    string deliveryDate;
    string deliveryTime;
    string repName;
    string repPhone;

    [DataMember]
    public String DisListID { get; set; }

    [DataMember]
    public String DeptName { get; set; }

    [DataMember]
    public String CollectionPoint { get; set; }

    [DataMember]
    public String DeliveryDate { get; set; }

    [DataMember]
    public String DeliveryTime { get; set; }

    [DataMember]
    public String RepName { get; set; }

    [DataMember]
    public String RepPhone { get; set; }
}

public class wcfDisbursementListDetail
{
    string itemName;
    string preQty;
    string disbQty;
    string remarks;

    [DataMember]
    public String ItemName { get; set; }

    [DataMember]
    public String PreQty { get; set; }

    [DataMember]
    public String DisbQty { get; set; }

    [DataMember]
    public String Remarks { get; set; }
}

public class wcfStockReorder
{
    string itemName;
    string actualQty;
    string reorderLevel;
    string reorderQty;
    string supplier1;
    string s1Phone;
    string s1Price;
    string supplier2;
    string s2Phone;
    string s2Price;
    string supplier3;
    string s3Phone;
    string s3Price;


    [DataMember]
    public String ItemName { get; set; }

    [DataMember]
    public String ActualQty { get; set; }

    [DataMember]
    public String ReorderLevel { get; set; }

    [DataMember]
    public String ReorderQty { get; set; }

    [DataMember]
    public String Supplier1 { get; set; }

    [DataMember]
    public String S1Phone { get; set; }

    [DataMember]
    public String S1Price { get; set; }

    [DataMember]
    public String Supplier2 { get; set; }

    [DataMember]
    public String S2Phone { get; set; }

    [DataMember]
    public String S2Price { get; set; }

    [DataMember]
    public String Supplier3 { get; set; }

    [DataMember]
    public String S3Phone { get; set; }
    [DataMember]
    public String S3Price { get; set; }
}

public class wcfRetrivalList
{
    string itemNo;
    string itemName;
    string requestedQty;
    string retrievedQty;
    string status;

    [DataMember]
    public String ItemNo { get; set; }

    [DataMember]
    public String ItemName { get; set; }

    [DataMember]
    public String RequestedQty { get; set; }

    [DataMember]
    public String RetrievedQty { get; set; }

    [DataMember]
    public String Status { get; set; }

   }

public class wcfallocate
{
    string itemName;
    string preQty;
    string disbQty;


    [DataMember]
    public String ItemName { get; set; }

    [DataMember]
    public String PreQty { get; set; }

    [DataMember]
    public String DisbQty { get; set; }


}


public class wcflogin
{
    string authenticate;
    string userid;
    string role;
    string deptid;


    [DataMember]
    public String Userid { get; set; }

    [DataMember]
    public String Role { get; set; }

    [DataMember]
    public String Deptid { get; set; }


    [DataMember]
    public string Authenticate { get; set; }


}
