using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ChatCore;
/*
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
public interface IService
{
    [OperationContract(IsInitiating = true, IsOneWay = false, IsTerminating = false)]
    List<String> Join(string name);

    [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = false)]
    void Send(string msg);

    [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = false)]
    void SendPrivate(string name, string msg);

    [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = true)]
    void Leave();
}

public interface IChatCallback
{
    [OperationContract(IsOneWay = true)]
    void Receive(string name, string msg);

    [OperationContract(IsOneWay = true)]
    void ReceivePrivate(string name, string msg);

    [OperationContract(IsOneWay = true)]
    void UserEnter(string name);

    [OperationContract(IsOneWay = true)]
    void UserLeave(string name);
}

*/


namespace ChatClient
{
    class Chat
    {

    }
}
