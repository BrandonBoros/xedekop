using Xedekop.Server.Controllers.Base;
using Xedekop.Server.Data;
using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Xedekop.Server.Controllers
{
    [Route("api/[controller]")]
    public class OrderItemController : BaseController<OrderItem>
    {
        private IUnitOfWork _unitOfWork;

        public OrderItemController(ILogger<OrderItemController> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork.GetRepository<IPokeRepository<OrderItem>>())
        {
            _unitOfWork = unitOfWork;
        }
    }
}
