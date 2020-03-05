using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClassLibrary;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    //User
    [OperationContract]
    List<users> GetUsers();

    [OperationContract]
    bool InsertUsers(users obj);

    [OperationContract]
    bool DeleteUsers(int uID);

    //Product
    [OperationContract]
    List<product> GetProduct();

    [OperationContract]
    List<product> GetProductFilter(string query);
    
    [OperationContract]
    bool InsertProduct(product obj);

    [OperationContract]
    bool DeleteProduct(int pID);

    //Order
    [OperationContract]
    List<order> GetOrder(int uID);

    [OperationContract]
    bool InsertOrder(order obj);

    [OperationContract]
    List<OrderDetailContract> GetOrderDetail(int uID, int oID);

    [OperationContract]
    bool InsertOrderDetail(orderdetail obj);

    //Hampers
    [OperationContract]
    List<HamperContract> GetHampers(int uID);
    
    [OperationContract]
    bool UpdateHampers(hampers obj);

    [OperationContract]
    bool UpdateQuantityHampers(int hID, int count);

    [OperationContract]
    bool DeleteHampers(int hID, int uID);

    [OperationContract]
    bool WriteDebugLogError(string error);

    [OperationContract]
    bool WriteDebugLogInfo(string info);

    [OperationContract]
    users Login(string x, string y);

    [OperationContract]
    string MD5(string Text);
}
