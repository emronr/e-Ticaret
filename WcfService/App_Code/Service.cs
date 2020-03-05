using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClassLibrary;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    ServiceHelper svc = new ServiceHelper();
    
    public List<users> GetUsers()
    {
        return svc.GetUsers();
    }
    public bool InsertUsers(users obj)
    {
        return svc.InsertUsers(obj);
    }
    public bool DeleteUsers(int uID)
    {
        return svc.DeleteUsers(uID);
    }


    public List<product> GetProduct()
    {
        return svc.GetProduct();
    }
    public List<product> GetProductFilter(string query)
    {
        return svc.GetProductFilter(query);
    }
    public bool InsertProduct(product obj)
    {
        return svc.InsertProduct(obj);
    }
    public bool DeleteProduct(int pID)
    {
        return svc.DeleteProduct(pID);
    }

    public  List<order> GetOrder(int uID)
    {
        return svc.GetOrder(uID);
    }
    public bool InsertOrder(order obj)
    {
        return svc.InsertOrder(obj);
    }
    public List<OrderDetailContract> GetOrderDetail(int uID,int oID)
    {
        return svc.GetOrderDetail(uID, oID);
    }
    public bool InsertOrderDetail(orderdetail obj)
    {
        return svc.InsertOrderDetail(obj);
    }

    
    public List<HamperContract> GetHampers(int uID)
    {
        return svc.GetHampers(uID);
    }
    public bool UpdateHampers(hampers obj)
    {
        return svc.UpdateHampers(obj);
    }
    public bool UpdateQuantityHampers(int hID,int count)
    {
        return svc.UpdateQuantityHampers(hID, count);
    }
    public bool DeleteHampers(int hID,int uID)
    {
        return svc.DeleteHampers(hID,uID);
    }

    public bool WriteDebugLogError(string error)
    {
        return svc.WriteDebugLogError(error);
    }
    public bool WriteDebugLogInfo(string info)
    {
        return svc.WriteDebugLogInfo(info);
    }
    public users Login(string x, string y)
    {
        return svc.Login(x, y);
    }
    public string MD5(string Text)
    {
        return svc.MD5(Text);
    }

}
