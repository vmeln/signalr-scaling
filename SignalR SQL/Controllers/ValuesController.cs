using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SignalR_SQL.Models;

namespace SignalR_SQL.Controllers
{
    public class ValuesController : ApiController
    {

        MessageRepository objRepo = new MessageRepository();

        public IEnumerable<Message> Get()
        {
            return objRepo.GetData();
        }
    }
}
