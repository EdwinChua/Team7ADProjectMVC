using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Team7ADProjectMVC
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
	public class Service : IService
	{
        ProjectEntities db = new ProjectEntities();
        public List<WCFMsg> DoWork()
        {
            List<WCFMsg> l = new List<WCFMsg>();
            l.Add(new WCFMsg("ok"));
            l.Add(new WCFMsg("ok2"));
            l.Add(new WCFMsg("ok3"));
            Console.Write(l.ToString());
            return l;

        }

        public List<wcfRequisitionList> RequisitionList()
        {
            List<wcfRequisitionList> making = new List<wcfRequisitionList>();
           
         List<Requisition> r = db.Requisitions.ToList();

         foreach(Requisition rr in r)
         {
             wcfRequisitionList rl = new wcfRequisitionList();
             rl.Employeename = rr.Employee.EmployeeName;
             rl.Status = rr.RequisitionStatus;
             rl.Id = rr.RequisitionId.ToString();
             making.Add(rl);
         }
         return making;
        }
    }
}
