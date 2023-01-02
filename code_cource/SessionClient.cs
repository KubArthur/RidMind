using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programmation_systeme
{
    class SessionClient
    {
        Client runInv = new Client();
        public void ClientAnwser()
        {
            // élément à envoyer au serv pour déduire score (button + guess)
            string packet = Values.Instance.UserName + "," + ValuesSession.Instance.Choice + "," + ValuesSession.Instance.Guess;
            ValuesSessionClient.Instance.Packet = packet;
            runInv.SendString("P-" + packet);
        }

        public void ClientSending()
        {
            // élément à envoyer au serv pour déduire score (button + guess)
            runInv.SendString("P-" + ValuesSessionClient.Instance.Packet);
        }
    }

    

    
    class ValuesSessionClient
    {
       
        public string Packet { get; set; }
        
        public ValuesSessionClient() { }
        public static readonly ValuesSessionClient Instance = new ValuesSessionClient();
    }
}
