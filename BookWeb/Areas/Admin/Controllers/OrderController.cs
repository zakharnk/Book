using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Diagnostics;
using System.Security.Claims;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.OrderHeaderId == orderId, includeProperties: "AppUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };

            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles = Constants.Role_Admin + "," + Constants.Role_Employee)]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.OrderHeaderId == OrderVM.OrderHeader.OrderHeaderId);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.State = OrderVM.OrderHeader.State;
            orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;

            if (string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Order Details Updated Successfully.";

            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.OrderHeaderId });
        }

        [HttpPost]
        [Authorize(Roles = Constants.Role_Admin + "," + Constants.Role_Employee)]
        public IActionResult StartProcessing()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.OrderHeaderId, Constants.StatusInProcess);
            _unitOfWork.Save();
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.OrderHeaderId });
        }

        [HttpPost]
        [Authorize(Roles = Constants.Role_Admin + "," + Constants.Role_Employee)]
        public IActionResult ShipOrder()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.OrderHeaderId == OrderVM.OrderHeader.OrderHeaderId);
            orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            orderHeaderFromDb.OrderStatus = Constants.StatusShipped;
            orderHeaderFromDb.OderShipped = DateTime.Now;
            if (orderHeaderFromDb.PaymentStatus == Constants.PaymentStatusDelayedPayment)
            {
                orderHeaderFromDb.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }
            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Order Shipped Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.OrderHeaderId });
        }

        [HttpPost]
        [Authorize(Roles = Constants.Role_Admin + "," + Constants.Role_Employee)]
        public IActionResult CancelOrder()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.OrderHeaderId == OrderVM.OrderHeader.OrderHeaderId);
            
            if(orderHeaderFromDb.PaymentStatus== Constants.PaymentStatusApproved)
            {
                //refund
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeaderFromDb.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);

                _unitOfWork.OrderHeader.UpdateStatus(orderHeaderFromDb.OrderHeaderId, Constants.StatusCancelled, Constants.StatusRefunded);
            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeaderFromDb.OrderHeaderId, Constants.StatusCancelled, Constants.StatusCancelled);
            }
            _unitOfWork.Save();
            TempData["Success"] = "Order Canceled Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.OrderHeaderId });
        }
        [ActionName("Details")]
        [HttpPost]
        public IActionResult DetailsPayNow()
        {
            OrderVM.OrderDetails = _unitOfWork.OrderDetail
                .GetAll(u => u.OrderHeaderId == OrderVM.OrderHeader.OrderHeaderId, includeProperties: "Product");
            OrderVM.OrderHeader = _unitOfWork.OrderHeader
                .Get(u => u.OrderHeaderId == OrderVM.OrderHeader.OrderHeaderId, includeProperties: "AppUser");

            //stripe logic

            var domain = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"admin/order/PaymentConfirmation?orderHeaderId={OrderVM.OrderHeader.OrderHeaderId}",
                CancelUrl = domain + $"admin/order/details?orderId={OrderVM.OrderHeader.OrderHeaderId}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in OrderVM.OrderDetails)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title,

                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentId(OrderVM.OrderHeader.OrderHeaderId, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        public IActionResult PaymentConfirmation(int orderHeaderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.OrderHeaderId == orderHeaderId);
            if (orderHeader.PaymentStatus == Constants.PaymentStatusDelayedPayment)
            {
                //company payment
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentId(orderHeaderId, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(orderHeaderId, orderHeader.OrderStatus, Constants.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
            }
            
            return View(orderHeaderId);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> orderHeaders;

            if (User.IsInRole(Constants.Role_Admin) || User.IsInRole(Constants.Role_Employee))
            {
                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "AppUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.AppUserId == userId, includeProperties: "AppUser");
            }
            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == Constants.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == Constants.StatusInProcess);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == Constants.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == Constants.StatusApproved);
                    break;
                default:
                    break;
            }

            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}
