using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xedekop.Server.Data;
using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Xedekop.Server.Controllers.Base;

namespace Xedekop.Server.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : BaseController<Order>
    {
        private IUnitOfWork _unitOfWork;

        public OrdersController(ILogger<OrdersController> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork.GetRepository<IPokeRepository<Order>>())
        {
            _unitOfWork = unitOfWork;
        }
    }
}
